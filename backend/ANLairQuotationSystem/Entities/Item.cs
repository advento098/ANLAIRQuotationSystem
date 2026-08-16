namespace ANLairQuotationSystem.Entities;

public class Item
{
    public uint Id { get; set; }
    public uint TypeId { get; set; }
    public required string UniqueId { get; set; }
    public required string Name { get; set; }
    public required string DistributorName { get; set; }
    public string? ContactNumber { get; set; }
    public string? Email { get; set; }
    public decimal FinalCost { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public DateTime DateModified { get; set; }

    public ItemType Type { get; set; } = null!;
    public ICollection<ItemExpense> ItemExpenses { get; set; } = [];
    public ICollection<ItemSpecification> ItemSpecifications { get; set; } = [];
    public ICollection<ItemImage> ItemImages { get; set; } = [];
}
