namespace ANLairQuotationSystem.Entities;

public class Client
{
    public uint Id { get; set; }
    public uint CreatorId { get; set; }
    public string? CompanyName { get; set; }
    public required string Firstname { get; set; }
    public string? Middlename { get; set; }
    public required string Surname { get; set; }
    public string? ExtensionName { get; set; }
    public string? Position { get; set; }
    public string? ContactNumber { get; set; }
    public required string Email { get; set; }
    public required string Address { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }

    public User CreatorUser { get; set; } = null!;
    public ICollection<Project> ClientProjects { get; set; } = [];
}
