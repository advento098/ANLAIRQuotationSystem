namespace ANLairQuotationSystem.Entities;

public class ItemImage
{
    public uint Id { get; set; }
    public uint ItemId { get; set; }
    public required byte[] Image { get; set; }
    public required string ContentType { get; set; }
    public string Caption { get; set; } = null!;
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public DateTime DateModified { get; set; }

    public Item Item { get; set; } = null!;
}
