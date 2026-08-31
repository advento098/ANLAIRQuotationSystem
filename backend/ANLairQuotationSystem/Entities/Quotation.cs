namespace ANLairQuotationSystem.Entities;

public class Quotation
{
    public uint ProjectId { get; set; }
    public decimal ItemsFinalCost { get; private set; }
    public decimal FinalCost { get; private set; }

    public Project Project { get; set; } = null!;
    public ICollection<QuotationAdditional> QuotationAdditionals { get; set; } = [];
    public ICollection<QuotationComputationConstant> QuotationComputationConstants { get; set; } = [];

    /// <summary>
    /// Sets the value of the final cost based on existing data
    /// </summary>
    public void CalculateFinalCost()
    {
        decimal itemExpenseTotal = Project.ProjectItems.Sum(pt => pt.CalculateExpenses());
        decimal quotationAdditionalsTotal = QuotationAdditionals.Sum(qa => qa.Cost);
        // TODO: Continue working on final quotation cost
        // Compute constants here before assigning to final cost

        ItemsFinalCost = itemExpenseTotal;
        //FinalCost = Math.Max(0, quotationAdditionalsTotal + itemExpenseTotal);
    }
}
