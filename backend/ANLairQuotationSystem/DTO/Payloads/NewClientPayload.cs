namespace ANLairQuotationSystem.DTO.Payloads;

public class NewClientPayload
{
    public required string CreatorPublicId { get; set; }
    public string? CompanyName { get; set; }
    public required string Firstname { get; set; }
    public string? Middlename { get; set; }
    public required string Surname { get; set; }
    public string? ExtensionName { get; set; }
    public string? Position { get; set; }
    public string? ContactNumber { get; set; }
    public required string Email { get; set; }
    public required string Address { get; set; }
}
