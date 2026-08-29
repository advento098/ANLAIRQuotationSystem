using static ANLairQuotationSystem.Entities.Project;

namespace ANLairQuotationSystem.DTO.Payloads;

public class NewProjectPayload
{
    public required string UserPublicId { get; set; }
    public required string ClientPublicId { get; set; }
    public required string Name { get; set; }
    public DateTime? DateRequested { get; set; } = null;
    public required string RequestorFirstname { get; set; }
    public string? RequestorMiddlename { get; set; } = null;
    public required string RequestorSurname { get; set; }
    public string? RequestorExtensionName { get; set; } = null;
    public required string RequestorPosition { get; set; }
    public required string HospitalName { get; set; }
    public ProjectStatus Status { get; set; }
    public List<string>? ItemTemplateUniqueId { get; set; } = null;

}
