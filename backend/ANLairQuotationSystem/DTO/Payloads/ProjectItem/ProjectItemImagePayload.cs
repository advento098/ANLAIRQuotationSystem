namespace ANLairQuotationSystem.DTO.Payloads.ProjectItem;

public class ProjectItemImagePayload
{
    public required byte[] Image { get; set; }
    public required string ContentType { get; set; }
    public string Caption { get; set; } = null!;
}
