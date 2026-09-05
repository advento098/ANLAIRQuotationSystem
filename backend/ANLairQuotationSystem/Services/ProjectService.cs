using ANLairQuotationSystem.Common;
using ANLairQuotationSystem.DTO.Payloads;
using ANLairQuotationSystem.DTO.Payloads.ProjectItem;
using ANLairQuotationSystem.DTO.Payloads.Quotation;
using ANLairQuotationSystem.Entities;
using ANLairQuotationSystem.Persistence;
using ANLairQuotationSystem.Repositories;
using ANLairQuotationSystem.Utilities;
using Microsoft.EntityFrameworkCore;

namespace ANLairQuotationSystem.Services;

public class ProjectService(
        AppDbContext db,
        ProjectRepository projectRepository
    )
{
    private readonly AppDbContext _db = db;
    private readonly ProjectRepository _projectRepository = projectRepository;

    #region "Project Root"

    public async Task<Result<string>> CreateNewProject(NewProjectPayload payload)
    {
        DateTime finalDateRequested = payload.DateRequested ?? DateTime.Now;
        string projectId = StringIdGenerator.GenerateUniqueId(payload.Name, finalDateRequested);

        // Fetch user
        uint userId = await _db.Users
            .Where(u => u.PublicId == payload.UserPublicId)
            .Select(u => u.Id)
            .FirstAsync();

        uint clientId = await _db.Clients
            .Where(c => c.PublicId == payload.ClientPublicId)
            .Select(c => c.Id)
            .FirstAsync();

        List<ProjectItem> items = payload.ItemTemplateUniqueId != null ?
            await _projectRepository.CreateProjectItemsFromItemTemplateUniqueIds(payload.ItemTemplateUniqueId) :
            [];

        Project newProject = new()
        {
            UniqueId = projectId,
            CreatorId = userId,
            ClientId = clientId,
            Name = payload.Name,
            DateRequested = finalDateRequested,
            RequestorFirstname = payload.RequestorFirstname,
            RequestorMiddlename = payload.RequestorMiddlename,
            RequestorSurname = payload.RequestorSurname,
            RequestorExtensionName = payload.RequestorExtensionName,
            RequestorPosition = payload.RequestorPosition,
            HospitalName = payload.HospitalName,
            Status = payload.Status,
            DateCreated = DateTime.Now,
            DateModified = DateTime.Now,
            ProjectItems = items
        };

        await _db.Projects.AddAsync(newProject);
        await _db.SaveChangesAsync();

        return Result<string>.Ok(newProject.UniqueId, "Successfully created new project");
    }
    public async Task<Result<string>> RenameProject(string projectUniqueId, string newProjectName)
    {
        Project project = await _db.Projects.SingleOrDefaultAsync(p => p.UniqueId == projectUniqueId)
            ?? throw new Exception("Project does not exist");

        project.Name = newProjectName;
        project.UniqueId = StringIdGenerator.GenerateUniqueId(newProjectName);

        await _db.SaveChangesAsync();

        return Result<string>.Ok(project.UniqueId, "Successfully changed project name");
    }
    public async Task<Result<bool>> ArchiveProject(string projectUniqueId)
    {
        Project project = await _db.Projects.SingleOrDefaultAsync(p => p.UniqueId == projectUniqueId)
            ?? throw new Exception("Project does not exist");

        project.Status = Project.ProjectStatus.ARCHIVED;
        project.DateModified = DateTime.Now;

        await _db.SaveChangesAsync();

        return Result<bool>.Ok(true, "Successfully archived project");
    }

    #endregion

    #region "Project Items"

    public async Task<Result<bool>> AssignProjectItemsFromItemTemplate(AssignProjectItemPayload payload)
    {
        // Load project
        Project? existingProject = await _db.Projects.SingleOrDefaultAsync(p => p.UniqueId == payload.ProjectUniqueId);
        if (existingProject == null) return Result<bool>.Fail("Project does not exists");
        if (existingProject.Status == Project.ProjectStatus.QUOTED)
            return Result<bool>.Fail("Cannot edit quoted and archived projects");

        // Load chosen items
        List<ProjectItem> projectItems =
            await _projectRepository.CreateProjectItemsFromItemTemplateUniqueIds(payload.AssignedUniqueItemIdList);
        existingProject.ProjectItems = [.. existingProject.ProjectItems, .. projectItems];
        // Compute every items
        foreach (var item in existingProject.ProjectItems)
        {
            item.CalculateExpenses();
        }

        existingProject.DateModified = DateTime.Now;

        await _db.SaveChangesAsync();

        return Result<bool>.Ok(true, "Successfully assigned items");
    }
    public async Task<Result<bool>> ManualAssignProjectItems(ManualAssignProjectItemPayload payload)
    {
        Project? existingProject = await _db.Projects.SingleOrDefaultAsync(p => p.UniqueId == payload.ProjectUniqueId);
        if (existingProject == null) return Result<bool>.Fail("Project does not exists");
        if (existingProject.Status == Project.ProjectStatus.QUOTED)
            return Result<bool>.Fail("Cannot edit quoted and archived projects");

        List<ProjectItem> projectItems = [..payload.ProjectItems.Select(item => new ProjectItem()
        {
            TypeId = item.TypeId,
            UniqueId = StringIdGenerator.GenerateUniqueId(item.Name),
            Name = item.Name,
            DistributorName = item.DistributorName,
            ContactNumber = item.ContactNumber,
            Email = item.Email,
            DateCreated = DateTime.Now,
            DateModified = DateTime.Now,
            ProjectItemExpenses = [..item.ProjectItemExpenses.Select(expense => new ProjectItemExpense()
            {
                Name = expense.Name,
                Description = expense.Description,
                Cost = expense.Cost,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            })],
            ProjectItemSpecifications = [..item.ProjectItemSpecifications.Select(spec => new ProjectItemSpecification()
            {
                Name = spec.Name,
                Description = spec.Description,
                Value = spec.Value,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
            })],
            ProjectItemImages = [..item.ProjectItemImages.Select(img => new ProjectItemImage()
            {
                Image = img.Image,
                ContentType = img.ContentType,
                Caption = img.Caption,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            })],
        })];
        existingProject.ProjectItems = [.. existingProject.ProjectItems, .. projectItems];

        foreach (var item in existingProject.ProjectItems)
        {
            item.CalculateExpenses();
        }

        existingProject.DateModified = DateTime.Now;

        await _db.SaveChangesAsync();

        return Result<bool>.Ok(true, "Successfully assigned manual project items");
    }

    #endregion

    #region "Project quotations"

    public async Task<Result<decimal>> CalculateFinalProjectQuotationCost(string userPublicId, string projectUniqueId)
    {
        Project? project = await _db.Projects
            .Include(p => p.Quotation)
                .ThenInclude(q => q.QuotationAdditionals)
            .Include(p => p.Quotation)
                .ThenInclude(q => q.QuotationComputationConstants)
            .Include(p => p.ProjectItems)
                .ThenInclude(pi => pi.ProjectItemExpenses)
            .Include(p => p.Creator)
            .SingleOrDefaultAsync(p => p.UniqueId == projectUniqueId);
        if (project is null) return Result<decimal>.Fail("Project does not exists");
        if (project.Creator.PublicId != userPublicId) return Result<decimal>.Fail("Invalid request: UNAUTHORIZED_CREATOR");

        project.Quotation.CalculateFinalCost();

        decimal finalCalculation = project.Quotation.FinalCost;

        await _db.SaveChangesAsync();

        return Result<decimal>.Ok(finalCalculation, "Successfully calculated and saved project final cost");
    }
    public async Task<Result<Quotation>> GenerateProjectQuotation(string userPublicId, AddEditQuotationPayload payload)
    {
        Project? loadedProject = await _db.Projects.SingleOrDefaultAsync(p => p.UniqueId == payload.ProjectUniqueId && p.Creator.PublicId == userPublicId);
        if (loadedProject is null) return Result<Quotation>.Fail("Project does not exists");

        Quotation newQuotation = new()
        {
            ProjectId = loadedProject.Id,
            Project = loadedProject
        };

        List<QuotationComputationConstant> quotationComputationConstants = [];
        // Add computation constants
        if (payload.ConstantComputationNames != null)
        {
            List<QuotationComputationConstant> computationConstants = await _db.ComputationConstants
                .Where(cc => payload.ConstantComputationNames.Contains(cc.Name))
                .Select(cc => new QuotationComputationConstant()
                {
                    Name = cc.Name,
                    Operator = cc.Operator,
                    Value = cc.Value,
                    Description = cc.Description,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                })
                .ToListAsync();

            quotationComputationConstants.AddRange(computationConstants);
        }

        if (payload.QuotationComputationConstantPayloads != null)
        {
            List<QuotationComputationConstant> computationConstants = [..payload.QuotationComputationConstantPayloads
                .Select(cc => new QuotationComputationConstant()
                {
                    Name = cc.Name,
                    Operator = cc.Operator,
                    Value = cc.Value,
                    Description = cc.Description,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                })];

            quotationComputationConstants.AddRange(computationConstants);
        }

        newQuotation.QuotationComputationConstants = quotationComputationConstants;

        // Add quotation additionals
        if (payload.Additionals != null)
        {
            List<QuotationAdditional> additionals = [..payload.Additionals.Select(qa => new QuotationAdditional() {
                Name = qa.Name,
                Description = qa.Description,
                Operator = qa.Operator,
                Cost = qa.Cost,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
            })];

            newQuotation.QuotationAdditionals = additionals;
        }

        loadedProject.DateModified = DateTime.Now;
        loadedProject.Status = Project.ProjectStatus.ON_GOING;
        newQuotation.CalculateFinalCost();

        await _db.Quotations.AddAsync(newQuotation);
        await _db.SaveChangesAsync();

        return Result<Quotation>.Ok(newQuotation);
    }
    public async Task<Result<bool>> EditProjectQuotation(string userPublicId, AddEditQuotationPayload payload)
    {
        Project? loadedProject = await _db.Projects
            .Include(p => p.Quotation)
                .ThenInclude(q => q.QuotationComputationConstants)
            .Include(p => p.Quotation)
                .ThenInclude(q => q.QuotationAdditionals)
            .SingleOrDefaultAsync(p => p.UniqueId == payload.ProjectUniqueId && p.Creator.PublicId == userPublicId);
        if (loadedProject is null) return Result<bool>.Fail("Project does not exists");

        if (payload.QuotationComputationConstantPayloads != null && payload.QuotationComputationConstantPayloads.Count > 0)
        {
            if (loadedProject.Quotation.QuotationComputationConstants.Count == 0)
                return Result<bool>.Fail("Quotation computation constants has no data to edit");

            if (payload.QuotationComputationConstantPayloads.Any(qc => !qc.Id.HasValue))
                return Result<bool>.Fail("Quotation computation constants contains data with no id");

            Dictionary<uint, QuotationComputationConstantPayload> computationConstantDictionary =
                payload.QuotationComputationConstantPayloads
                .ToDictionary(qc => qc.Id!.Value, qc => qc);

            foreach (var item in loadedProject.Quotation.QuotationComputationConstants)
            {
                if (computationConstantDictionary.TryGetValue(item.Id, out var matchedPayload))
                {
                    item.Name = matchedPayload.Name;
                    item.Operator = matchedPayload.Operator;
                    item.Value = matchedPayload.Value;
                    item.Description = matchedPayload.Description;
                    item.DateModified = DateTime.Now;
                }
            }
        }

        if (payload.Additionals != null && payload.Additionals.Count > 0)
        {
            if (loadedProject.Quotation.QuotationAdditionals.Count == 0)
                return Result<bool>.Fail("Quotation additionals has no data to edit");

            if (payload.Additionals.Any(qc => !qc.Id.HasValue))
                return Result<bool>.Fail("Quotation additionals contains data with no id");

            Dictionary<uint, QuotationAdditionalPayload> quotationAdditionalPayloads =
                payload.Additionals
                .ToDictionary(qc => qc.Id!.Value, qc => qc);

            foreach (var item in loadedProject.Quotation.QuotationAdditionals)
            {
                if (quotationAdditionalPayloads.TryGetValue(item.Id, out var matchedPayload))
                {
                    item.Name = matchedPayload.Name;
                    item.Operator = matchedPayload.Operator;
                    item.Cost = matchedPayload.Cost;
                    item.Description = matchedPayload.Description;
                    item.DateModified = DateTime.Now;
                }
            }
        }

        loadedProject.DateModified = DateTime.Now;
        loadedProject.Status = Project.ProjectStatus.ON_GOING;
        loadedProject.Quotation.CalculateFinalCost();

        await _db.SaveChangesAsync();

        return Result<bool>.Ok(true, "Successfully updated project quotation");
    }

    #endregion
}
