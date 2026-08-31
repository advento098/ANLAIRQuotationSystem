using ANLairQuotationSystem.Common;
using ANLairQuotationSystem.DTO.Payloads;
using ANLairQuotationSystem.Entities;
using ANLairQuotationSystem.Factories;
using ANLairQuotationSystem.Persistence;
using ANLairQuotationSystem.Utilities;
using Microsoft.EntityFrameworkCore;

namespace ANLairQuotationSystem.Services;

public class ProjectService(
        AppDbContext db,
        ProjectItemFactory projectItemFactory
    )
{
    private readonly AppDbContext _db = db;
    private readonly ProjectItemFactory _projectItemFactory = projectItemFactory;

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
            await _projectItemFactory.CreateProjectItemsFromItemTemplateUniqueIds(payload.ItemTemplateUniqueId) :
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
    public async Task<Result<bool>> AssignProjectItems(AssignProjectItemPayload payload)
    {
        // Load project
        Project? existingProject = await _db.Projects.SingleOrDefaultAsync(p => p.UniqueId == payload.ProjectUniqueId);
        if (existingProject == null) return Result<bool>.Fail("Project does not exists");
        if (existingProject.Status == Project.ProjectStatus.QUOTED || existingProject.Status == Project.ProjectStatus.ARCHIVED)
            return Result<bool>.Fail("Cannot edit quoted and archived projects");

        // Load chosen items
        List<ProjectItem> projectItems = await _projectItemFactory.CreateProjectItemsFromItemTemplateUniqueIds(payload.AssignedUniqueItemIdList);
        existingProject.ProjectItems = projectItems;



        await _db.SaveChangesAsync();

        return Result<bool>.Ok(true, "Successfully assigned items");
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
}
