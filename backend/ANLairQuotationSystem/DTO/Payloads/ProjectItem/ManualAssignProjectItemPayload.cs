namespace ANLairQuotationSystem.DTO.Payloads.ProjectItem;

public class ManualAssignProjectItemPayload
{
    public required string ProjectUniqueId { get; set; }

    public IEnumerable<ProjectItemPayload> ProjectItems { get; set; } = [];
}
