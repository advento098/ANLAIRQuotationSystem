namespace ANLairQuotationSystem.DTO.Payloads.ProjectItem;

public class ProjectItemPayload
{
    public uint TypeId { get; set; }
    public bool ShouldAddToTemplates { get; set; } = false;
    public required string Name { get; set; }
    public required string DistributorName { get; set; }
    public string? ContactNumber { get; set; }
    public string? Email { get; set; }
    public ICollection<ProjectItemExpensePayload> ProjectItemExpenses { get; set; } = [];
    public ICollection<ProjectItemSpecificationPayload> ProjectItemSpecifications { get; set; } = [];
    public ICollection<ProjectItemImagePayload> ProjectItemImages { get; set; } = [];
}
