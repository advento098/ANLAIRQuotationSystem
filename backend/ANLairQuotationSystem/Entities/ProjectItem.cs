namespace ANLairQuotationSystem.Entities;

public class ProjectItem
{
    public uint Id { get; set; }
    public uint ProjectId { get; set; }
    public uint TypeId { get; set; }
    public required string UniqueId { get; set; }
    public required string Name { get; set; }
    public required string DistributorName { get; set; }
    public string? ContactNumber { get; set; }
    public string? Email { get; set; }
    public decimal FinalCost { get; private set; }
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public DateTime DateModified { get; set; }

    public ItemType Type { get; set; } = null!;
    public Project Project { get; set; } = null!;

    public ICollection<ProjectItemExpense> ProjectItemExpenses { get; set; } = [];
    public ICollection<ProjectItemSpecification> ProjectItemSpecifications { get; set; } = [];
    public ICollection<ProjectItemImage> ProjectItemImages { get; set; } = [];

    public void CalculateExpenses()
    {
        decimal total = ProjectItemExpenses.Sum(pie => pie.Cost);
        FinalCost = total;
    }
}
