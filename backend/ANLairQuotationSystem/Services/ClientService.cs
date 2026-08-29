using ANLairQuotationSystem.Common;
using ANLairQuotationSystem.DTO.Payloads;
using ANLairQuotationSystem.Entities;
using ANLairQuotationSystem.Persistence;
using ANLairQuotationSystem.Utilities;
using Microsoft.EntityFrameworkCore;

namespace ANLairQuotationSystem.Services;

public class ClientService(
        AppDbContext db
    )
{
    private readonly AppDbContext _db = db;

    public async Task<Result<string>> CreateNewClient(NewClientPayload payload)
    {
        uint creatorId = await _db.Users
            .Where(u => u.PublicId == payload.CreatorPublicId)
            .Select(u => u.Id)
            .FirstAsync();

        Client newClient = new()
        {
            CreatorId = creatorId,
            PublicId = StringIdGenerator.Generate(),
            CompanyName = payload.CompanyName,
            Firstname = payload.Firstname,
            Middlename = payload.Middlename,
            Surname = payload.Surname,
            ExtensionName = payload.ExtensionName,
            Position = payload.Position,
            ContactNumber = payload.ContactNumber,
            Email = payload.Email,
            Address = payload.Address,
            DateCreated = DateTime.Now,
            DateModified = DateTime.Now
        };

        await _db.Clients.AddAsync(newClient);
        await _db.SaveChangesAsync();

        return Result<string>.Ok(newClient.PublicId);
    }
}
