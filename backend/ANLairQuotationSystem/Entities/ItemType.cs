namespace ANLairQuotationSystem.Entities;

public class ItemType
{
    public uint Id { get; set; }
    public required string Name { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public DateTime DateModified { get; set; }

    public ICollection<Item> Items { get; set; } = [];
    public ICollection<ProjectItem> ProjectItems { get; set; } = [];
}
