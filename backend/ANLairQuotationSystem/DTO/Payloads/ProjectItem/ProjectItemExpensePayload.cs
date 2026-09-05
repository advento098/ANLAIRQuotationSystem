namespace ANLairQuotationSystem.DTO.Payloads.ProjectItem;

public class ProjectItemExpensePayload
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Cost { get; set; }
}
