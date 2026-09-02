using ANLairQuotationSystem.Common;
using ANLairQuotationSystem.DTO.Payloads.Quotation;
using ANLairQuotationSystem.Entities;
using ANLairQuotationSystem.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ANLairQuotationSystem.Services;

public class QuotationServices(
        AppDbContext db
    )
{
    private readonly AppDbContext _db = db;
    // TODO: (September 1, 2026) Add quotation creation?
    public async Task<Result<Quotation>> GenerateProjectQuotation(string userPublicId, AddEditQuotationPayload payload)
    {
        Project? loadedProject = await _db.Projects.SingleOrDefaultAsync(p => p.UniqueId == payload.ProjectUniqueId && p.Creator.PublicId == userPublicId);
        if (loadedProject is null) return Result<Quotation>.Fail("Project does not exists");

        Quotation newQuotation = new()
        {
            ProjectId = loadedProject.Id,
            Project = loadedProject
        };

        List<QuotationComputationConstant> quotationComputationConstants = [];
        // Add computation constants
        if (payload.ConstantComputationNames != null)
        {
            List<QuotationComputationConstant> computationConstants = await _db.ComputationConstants
                .Where(cc => payload.ConstantComputationNames.Contains(cc.Name))
                .Select(cc => new QuotationComputationConstant()
                {
                    Name = cc.Name,
                    Operator = cc.Operator,
                    Value = cc.Value,
                    Description = cc.Description,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                })
                .ToListAsync();

            quotationComputationConstants.AddRange(computationConstants);
        }

        if (payload.QuotationComputationConstantPayloads != null)
        {
            List<QuotationComputationConstant> computationConstants = [..payload.QuotationComputationConstantPayloads
                .Select(cc => new QuotationComputationConstant()
                {
                    Name = cc.Name,
                    Operator = cc.Operator,
                    Value = cc.Value,
                    Description = cc.Description,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                })];

            quotationComputationConstants.AddRange(computationConstants);
        }

        newQuotation.QuotationComputationConstants = quotationComputationConstants;

        // Add quotation additionals
        if (payload.Additionals != null)
        {
            List<QuotationAdditional> additionals = [..payload.Additionals.Select(qa => new QuotationAdditional() {
                Name = qa.Name,
                Description = qa.Description,
                Operator = qa.Operator,
                Cost = qa.Cost,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
            })];

            newQuotation.QuotationAdditionals = additionals;
        }

        loadedProject.DateModified = DateTime.Now;
        loadedProject.Status = Project.ProjectStatus.ON_GOING;
        newQuotation.CalculateFinalCost();

        await _db.Quotations.AddAsync(newQuotation);
        await _db.SaveChangesAsync();

        return Result<Quotation>.Ok(newQuotation);
    }

    // TODO: (September 1, 2026) Add quotation edit
    public async Task<Result<bool>> EditProjectQuotation(string userPublicId, AddEditQuotationPayload payload)
    {
        Project? loadedProject = await _db.Projects
            .Include(p => p.Quotation)
                .ThenInclude(q => q.QuotationComputationConstants)
            .Include(p => p.Quotation)
                .ThenInclude(q => q.QuotationAdditionals)
            .SingleOrDefaultAsync(p => p.UniqueId == payload.ProjectUniqueId && p.Creator.PublicId == userPublicId);
        if (loadedProject is null) return Result<bool>.Fail("Project does not exists");

        if (payload.QuotationComputationConstantPayloads != null && payload.QuotationComputationConstantPayloads.Count > 0)
        {
            if (loadedProject.Quotation.QuotationComputationConstants.Count == 0)
                return Result<bool>.Fail("Quotation computation constants has no data to edit");

            if (payload.QuotationComputationConstantPayloads.Any(qc => !qc.Id.HasValue))
                return Result<bool>.Fail("Quotation computation constants contains data with no id");

            Dictionary<uint, QuotationComputationConstantPayload> computationConstantDictionary =
                payload.QuotationComputationConstantPayloads
                .ToDictionary(qc => qc.Id!.Value, qc => qc);

            foreach (var item in loadedProject.Quotation.QuotationComputationConstants)
            {
                if (computationConstantDictionary.TryGetValue(item.Id, out var matchedPayload))
                {
                    item.Name = matchedPayload.Name;
                    item.Operator = matchedPayload.Operator;
                    item.Value = matchedPayload.Value;
                    item.Description = matchedPayload.Description;
                    item.DateModified = DateTime.Now;
                }
            }
        }

        if (payload.Additionals != null && payload.Additionals.Count > 0)
        {
            if (loadedProject.Quotation.QuotationAdditionals.Count == 0)
                return Result<bool>.Fail("Quotation additionals has no data to edit");

            if (payload.Additionals.Any(qc => !qc.Id.HasValue))
                return Result<bool>.Fail("Quotation additionals contains data with no id");

            Dictionary<uint, QuotationAdditionalPayload> quotationAdditionalPayloads =
                payload.Additionals
                .ToDictionary(qc => qc.Id!.Value, qc => qc);

            foreach (var item in loadedProject.Quotation.QuotationAdditionals)
            {
                if (quotationAdditionalPayloads.TryGetValue(item.Id, out var matchedPayload))
                {
                    item.Name = matchedPayload.Name;
                    item.Operator = matchedPayload.Operator;
                    item.Cost = matchedPayload.Cost;
                    item.Description = matchedPayload.Description;
                    item.DateModified = DateTime.Now;
                }
            }
        }

        loadedProject.DateModified = DateTime.Now;
        loadedProject.Status = Project.ProjectStatus.ON_GOING;
        loadedProject.Quotation.CalculateFinalCost();

        await _db.SaveChangesAsync();

        return Result<bool>.Ok(true, "Successfully updated project quotation");
    }
}
