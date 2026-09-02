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

        #region "Quotation Computations"

        var orderedQuotationAdditionals = QuotationAdditionals.OrderBy(qa => qa.Operator);
        decimal runningQuotationAdditionals = 0;

        foreach (var item in orderedQuotationAdditionals)
        {
            runningQuotationAdditionals += item.CalculateNewValueUsingOperator(runningQuotationAdditionals);
        }

        decimal quotationAdditionalsTotal = runningQuotationAdditionals;

        #endregion

        #region "Constant Computations"

        var orderedConstantComputations = QuotationComputationConstants.OrderBy(qc => qc.Operator);
        decimal runningConstantComputationTotal = itemExpenseTotal + quotationAdditionalsTotal;

        foreach (var qc in orderedConstantComputations)
        {
            runningConstantComputationTotal += qc.CalculateNewValueUsingOperator(runningConstantComputationTotal);
        }

        #endregion

        ItemsFinalCost = itemExpenseTotal;
        FinalCost = runningConstantComputationTotal;
    }
}
