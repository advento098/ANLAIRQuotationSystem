using ANLairQuotationSystem.Persistence;

namespace ANLairQuotationSystem.Services;

public class TemplatingService(
        AppDbContext db
    )
{
    private readonly AppDbContext _db = db;
}