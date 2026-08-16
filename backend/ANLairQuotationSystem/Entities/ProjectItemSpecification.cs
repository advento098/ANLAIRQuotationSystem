namespace ANLairQuotationSystem.Entities;

public class ProjectItemSpecification
{
    public uint Id { get; set; }
    public uint ProjectItemId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Value { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public DateTime DateModified { get; set; }

    public ProjectItem ProjectItem { get; set; } = null!;
}
