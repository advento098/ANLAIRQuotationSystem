namespace ANLairQuotationSystem.Entities;

public class ProjectRequestProof
{
    public uint Id { get; set; }
    public uint CreatorId { get; set; }
    public uint ProjectId { get; set; }
    public required byte[] PhotoConfirmation { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;

    public User Creator { get; set; } = null!;
    public Project Project { get; set; } = null!;
}
