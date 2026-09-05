using ANLairQuotationSystem.Entities;
using ANLairQuotationSystem.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ANLairQuotationSystem.Repositories;

public class ProjectRepository(
        AppDbContext db
    )
{
    private readonly AppDbContext _db = db;
    public async Task<List<ProjectItem>> CreateProjectItemsFromItemTemplateUniqueIds(List<string> itemUniqueIdList)
    {
        var result = await _db.Items
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
                    ContentType = img.ContentType,
                    Caption = img.Caption,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                }).ToList()
            })
            .ToListAsync();

        foreach (var item in result)
        {
            item.CalculateExpenses();
        }

        return result;
    }
}
