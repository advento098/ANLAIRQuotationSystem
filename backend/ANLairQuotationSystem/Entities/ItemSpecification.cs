namespace ANLairQuotationSystem.Entities;

public class ItemSpecification
{
    public uint Id { get; set; }
    public uint ItemId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Value { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public DateTime DateModified { get; set; }

    public Item Item { get; set; } = null!;
}
