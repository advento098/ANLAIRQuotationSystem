using ANLairQuotationSystem.Entities;

namespace ANLAIRQuotationSystem.Entities;

public class Project
{
    public uint Id { get; set; }
    public required string UniqueId { get; set; }
    public uint CreatorId { get; set; }
    public uint ClientId { get; set; }
    public required string Name { get; set; }
    public DateTime DateRequested { get; set; }
    public required string RequestorFirstname { get; set; }
    public string? RequestorMiddlename { get; set; }
    public required string RequestorSurname { get; set; }
    public string? RequestorExtensionName { get; set; }
    public required string RequestorPosition { get; set; }
    public required string HospitalName { get; set; }
    public ProjectStatus Status { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }


    public User Creator { get; set; } = null!;
    public Client Client { get; set; } = null!;
    public Quotation Quotation { get; set; } = null!;
    public ICollection<ProjectRequestProof> ProjectRequestProofs { get; set; } = [];
    public ICollection<ProjectItem> ProjectItems { get; set; } = [];

    public enum ProjectStatus
    {
        IDLE,
        ON_GOING,
        QUOTED
    }
}
