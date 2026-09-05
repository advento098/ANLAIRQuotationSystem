namespace ANLairQuotationSystem.DTO.Payloads.ProjectItem;

public class ProjectItemSpecificationPayload
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Value { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public DateTime DateModified { get; set; }
}
