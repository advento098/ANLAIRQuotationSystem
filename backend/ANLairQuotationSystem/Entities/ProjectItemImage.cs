namespace ANLairQuotationSystem.Entities;

public class ProjectItemImage
{
    public uint Id { get; set; }
    public uint ProjectItemId { get; set; }
    public required byte[] Image { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public DateTime DateModified { get; set; }

    public ProjectItem ProjectItem { get; set; } = null!;
}
