namespace ANLairQuotationSystem.DTO.Payloads;

public class AssignProjectItemPayload
{
    public required string ProjectUniqueId { get; set; }
    public List<string> AssignedUniqueItemIdList { get; set; } = [];
}
