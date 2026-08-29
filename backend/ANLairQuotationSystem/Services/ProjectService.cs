using ANLairQuotationSystem.Common;
using ANLairQuotationSystem.DTO.Payloads;
using ANLairQuotationSystem.Entities;
using ANLairQuotationSystem.Persistence;
using ANLairQuotationSystem.Utilities;
using Microsoft.EntityFrameworkCore;

namespace ANLairQuotationSystem.Services;

public class ProjectService(
        AppDbContext db
    )
{
    private readonly AppDbContext _db = db;

    public async Task<Result<string>> CreateNewProject(NewProjectPayload payload)
    {
        DateTime finalDateRequested = payload.DateRequested ?? DateTime.Now;
        string projectId = TextManager.GenerateProjectId(payload.Name, finalDateRequested);

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
            await _db.Items
            .Where(it => payload.ItemTemplateUniqueId.Contains(it.UniqueId))
            .Select(item => new ProjectItem()
            {
                Id = item.Id,
                TypeId = item.TypeId,
                UniqueId = item.UniqueId,
                Name = item.Name,
                DistributorName = item.DistributorName,
                ContactNumber = item.ContactNumber,
                Email = item.Email,
                FinalCost = item.FinalCost,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                Type = item.Type,
                ProjectItemExpenses = item.ItemExpenses.Select(expense => new ProjectItemExpense()
                {
                    Name = expense.Name,
                    Description = expense.Description,
                    Cost = expense.Cost,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                }).ToList(),
                ProjectItemSpecifications = item.ItemSpecifications.Select(spec => new ProjectItemSpecification()
                {
                    Name = spec.Name,
                    Description = spec.Description,
                    Value = spec.Value,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                }).ToList(),
                ProjectItemImages = item.ItemImages.Select(img => new ProjectItemImage()
                {
                    Image = img.Image,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                }).ToList()
            }).ToListAsync() : [];

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
        project.UniqueId = TextManager.GenerateProjectId(newProjectName);

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
    public async Task<Result<>>
}
