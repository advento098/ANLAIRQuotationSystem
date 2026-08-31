using ANLairQuotationSystem.Entities;
using ANLairQuotationSystem.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ANLairQuotationSystem.Factories;

public class ProjectItemFactory(
        AppDbContext db
    )
{
    private readonly AppDbContext _db = db;
    public async Task<List<ProjectItem>> CreateProjectItemsFromItemTemplateUniqueIds(List<string> itemUniqueIdList)
    {
        return await _db.Items
            .AsNoTracking()
            .Where(i => itemUniqueIdList.Contains(i.UniqueId))
            .Select(item => new ProjectItem()
            {
                TypeId = item.TypeId,
                UniqueId = item.UniqueId,
                Name = item.Name,
                DistributorName = item.DistributorName,
                ContactNumber = item.ContactNumber,
                Email = item.Email,
                FinalCost = item.FinalCost,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                Type = item.Type,
                ProjectItemExpenses = item.ItemExpenses.Select(expense => new ProjectItemExpense()
                {
                    Name = expense.Name,
                    Description = expense.Description,
                    Cost = expense.Cost,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                }).ToList(),
                ProjectItemSpecifications = item.ItemSpecifications.Select(spec => new ProjectItemSpecification()
                {
                    Name = spec.Name,
                    Description = spec.Description,
                    Value = spec.Value,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                }).ToList(),
                ProjectItemImages = item.ItemImages.Select(img => new ProjectItemImage()
                {
                    Image = img.Image,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                }).ToList()
            })
            .ToListAsync();
    }
}
