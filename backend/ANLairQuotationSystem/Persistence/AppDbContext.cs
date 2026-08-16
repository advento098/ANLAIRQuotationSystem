using ANLairQuotationSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace ANLairQuotationSystem.Persistence;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    // TODO: Continue checking the correctness of the entities
    public DbSet<Client> Clients { get; set; }
    public DbSet<ComputationConstant> ComputationConstants { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<ItemType> ItemTypes { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<ItemExpense> ItemExpenses { get; set; }
    public DbSet<ItemSpecification> ItemSpecifications { get; set; }
    public DbSet<ItemImage> ItemImages { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectItem> ProjectItems { get; set; }
    public DbSet<ProjectItemExpense> ProjectItemExpenses { get; set; }
    public DbSet<ProjectItemSpecification> ProjectItemSpecifications { get; set; }
    public DbSet<ProjectItemImage> ProjectItemImages { get; set; }
    public DbSet<ProjectRequestProof> ProjectRequestProofs { get; set; }
    public DbSet<Quotation> Quotations { get; set; }
    public DbSet<QuotationAdditional> QuotationAdditionals { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserSession> UserSessions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Client>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.Property(c => c.Id)
            .ValueGeneratedOnAdd();

            entity
            .Property(c => c.CompanyName)
            .IsRequired(false)
            .HasMaxLength(150);

            entity
            .Property(c => c.Firstname)
            .IsRequired()
            .HasMaxLength(50);

            entity
            .Property(c => c.Middlename)
            .IsRequired(false)
            .HasMaxLength(50);

            entity
            .Property(c => c.Surname)
            .IsRequired()
            .HasMaxLength(50);

            entity
            .Property(c => c.ExtensionName)
            .IsRequired(false)
            .HasMaxLength(20);

            entity
            .Property(c => c.Position)
            .IsRequired(false)
            .HasMaxLength(50);

            entity
            .Property(c => c.ContactNumber)
            .IsRequired(false)
            .HasMaxLength(20);

            entity
            .Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(50);

            entity
            .Property(c => c.Address)
            .IsRequired()
            .HasMaxLength(100);

            entity
            .Property(c => c.DateCreated)
            .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity
            .Property(c => c.DateModified)
            .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity
            .HasOne(c => c.CreatorUser)
            .WithMany(c => c.CreatedClients)
            .HasForeignKey(c => c.CreatorId);
        }); // DONE

        builder.Entity<ComputationConstant>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsRequired();

            entity.HasIndex(e => e.Name)
                .IsUnique();

            entity.Property(e => e.Value)
                .HasPrecision(13, 2)
                .IsRequired();

            entity.Property(e => e.Description)
                .HasMaxLength(200);

            entity.Property(e => e.DateCreated)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.Property(e => e.DateModified)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        }); // DONE

        builder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsRequired();

            entity.HasIndex(e => e.Name)
                .IsUnique();

            entity.Property(e => e.DateCreated)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.Property(e => e.DateModified)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            //entity.HasMany(e => e.RolePermissions)
            //    .WithOne(e => e.Permission)
            //    .HasForeignKey(e => e.PermissionId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }); // DONE

        builder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.UniqueId)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasIndex(e => e.UniqueId)
                .IsUnique();

            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(e => e.RequestorFirstname)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.RequestorMiddlename)
                .HasMaxLength(100)
                .IsRequired(false);

            entity.Property(e => e.RequestorSurname)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.RequestorExtensionName)
                .HasMaxLength(20)
                .IsRequired(false);

            entity.Property(e => e.RequestorPosition)
                .HasMaxLength(100)
                .IsRequired(false);

            entity.Property(e => e.HospitalName)
                .HasMaxLength(200)
                .IsRequired();

            //entity.Property(e => e.Status)
            //    .HasConversion<string>()
            //    .HasMaxLength(20)
            //    .IsRequired();

            entity.Property(e => e.DateRequested)
                .IsRequired();

            entity.Property(e => e.DateCreated)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");


            entity.Property(e => e.DateModified)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.HasOne(e => e.Creator)
                .WithMany(e => e.CreatedProjects)
                .HasForeignKey(e => e.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Client)
                .WithMany(e => e.ClientProjects)
                .HasForeignKey(e => e.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
        });// DONE

        builder.Entity<ProjectRequestProof>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            entity.Property(p => p.PhotoConfirmation)
                .HasColumnType("MEDIUMBLOB");

            entity.Property(p => p.DateCreated)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.HasOne(p => p.Creator)
                .WithMany(p => p.CreatedProjectRequestProofs)
                .HasForeignKey(p => p.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.Project)
                .WithMany(p => p.ProjectRequestProofs)
                .HasForeignKey(p => p.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }); // DONE

        builder.Entity<Quotation>(entity =>
        {
            entity.HasKey(e => e.ProjectId);

            entity.Property(e => e.ItemsFinalCost)
                .HasPrecision(13, 2)
                .IsRequired();

            entity.Property(e => e.FinalCost)
                .HasPrecision(14, 2)
                .IsRequired();

            entity.HasOne(e => e.Project)
                .WithOne(e => e.Quotation)
                .HasForeignKey<Quotation>(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<QuotationAdditional>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsRequired(false);

            entity.Property(e => e.Cost)
                .HasPrecision(13, 2)
                .IsRequired();

            entity.Property(e => e.DateCreated)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.Property(e => e.DateModified)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.HasOne(e => e.Quotation)
                .WithMany(e => e.QuotationAdditionals)
                .HasForeignKey(e => e.QuotationId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsRequired();

            entity.HasIndex(e => e.Name)
                .IsUnique();

            entity.Property(e => e.Description)
                .HasMaxLength(200);

            entity.Property(e => e.DateCreated)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.Property(e => e.DateModified)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            //entity.HasMany(e => e.Users)
            //    .WithOne(e => e.Role)
            //    .HasForeignKey(e => e.RoleId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //entity.HasMany(e => e.RolePermissions)
            //    .WithOne(e => e.Role)
            //    .HasForeignKey(e => e.RoleId)
            //    .OnDelete(DeleteBehavior.Cascade);
        });  // DONE

        builder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => new { e.RoleId, e.PermissionId });

            entity.HasOne(e => e.Role)
                .WithMany(e => e.RolePermissions)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Permission)
                .WithMany(e => e.RolePermissions)
                .HasForeignKey(e => e.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);
        }); // DONE

        builder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.PublicId)
                .HasMaxLength(15)
                .IsRequired();

            entity.HasIndex(e => e.PublicId)
                .IsUnique();

            entity.Property(e => e.Firstname)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.Middlename)
                .IsRequired(false)
                .HasMaxLength(100);

            entity.Property(e => e.Surname)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.ExtensionName)
                .IsRequired(false)
                .HasMaxLength(20);

            entity.Property(e => e.ContactNumber)
                .HasMaxLength(20);

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsRequired();

            entity.HasIndex(e => e.Email)
                .IsUnique();

            // Let entity framework map it to int
            //entity.Property(e => e.Status)
            //    .HasConversion<string>()
            //    .HasMaxLength(20)
            //    .IsRequired();

            entity.Property(e => e.DateCreated)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.Property(e => e.DateModified)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.HasOne(e => e.Role)
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            //entity.HasMany(e => e.UserSessions)
            //    .WithOne(e => e.User)
            //    .HasForeignKey(e => e.UserId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //entity.HasMany(e => e.CreatedClients)
            //    .WithOne(e => e.CreatorUser)
            //    .HasForeignKey(e => e.CreatorId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //entity.HasMany(e => e.CreatedProjects)
            //    .WithOne(e => e.CreatorUser)
            //    .HasForeignKey(e => e.CreatorId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //entity.HasMany(e => e.ClientProjects)
            //    .WithOne(e => e.ClientUser)
            //    .HasForeignKey(e => e.ClientId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }); // DONE

        builder.Entity<UserSession>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.RefreshToken)
                .HasMaxLength(150)
                .IsRequired();

            entity.HasIndex(e => e.RefreshToken)
                .IsUnique();

            entity.Property(e => e.DateCreated)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.Property(e => e.DateExpiring)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.HasOne(e => e.User)
                .WithMany(e => e.UserSessions)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }); // DONE

        // New item schema
        builder.Entity<ItemType>(entity =>
        {
            entity.HasKey(it => it.Id);

            entity.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(i => i.DateCreated)
               .IsRequired()
               .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.Property(i => i.DateModified)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        });

        builder.Entity<Item>(entity =>
        {
            entity.HasKey(i => i.Id);

            entity.Property(i => i.UniqueId)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasIndex(i => i.UniqueId)
                .IsUnique();

            entity.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(i => i.DistributorName)
               .IsRequired()
               .HasMaxLength(150);

            entity.Property(i => i.ContactNumber)
               .IsRequired(false)
               .HasMaxLength(20);

            entity.Property(i => i.FinalCost)
                .IsRequired()
                .HasPrecision(12, 2);

            entity.Property(i => i.DateCreated)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.Property(i => i.DateModified)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.HasOne(i => i.Type)
                .WithMany(t => t.Items)
                .HasForeignKey(i => i.TypeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<ItemExpense>(entity =>
        {
            entity.HasKey(i => i.Id);

            entity.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(i => i.Description)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(i => i.Cost)
                .IsRequired()
                .HasPrecision(12, 2);

            entity.Property(i => i.DateCreated)
               .IsRequired()
               .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.Property(i => i.DateModified)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.HasOne(i => i.Item)
                .WithMany(t => t.ItemExpenses)
                .HasForeignKey(i => i.ItemId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<ItemSpecification>(entity =>
        {
            entity.HasKey(i => i.Id);

            entity.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(i => i.Description)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(i => i.Value)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(i => i.DateCreated)
               .IsRequired()
               .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.Property(i => i.DateModified)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.HasOne(i => i.Item)
               .WithMany(t => t.ItemSpecifications)
               .HasForeignKey(i => i.ItemId)
               .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<ItemImage>(entity =>
        {
            entity.HasKey(i => i.Id);

            entity.Property(i => i.Image)
                .IsRequired()
                .HasColumnType("MEDIUMBLOB");

            entity.Property(i => i.DateCreated)
               .IsRequired()
               .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.Property(i => i.DateModified)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.HasOne(i => i.Item)
              .WithMany(t => t.ItemImages)
              .HasForeignKey(i => i.ItemId)
              .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<ProjectItem>(entity =>
        {
            entity.HasKey(i => i.Id);

            entity.Property(i => i.UniqueId)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasIndex(i => i.UniqueId)
                .IsUnique();

            entity.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(i => i.DistributorName)
               .IsRequired()
               .HasMaxLength(150);

            entity.Property(i => i.ContactNumber)
               .IsRequired(false)
               .HasMaxLength(20);

            entity.Property(i => i.FinalCost)
                .IsRequired()
                .HasPrecision(12, 2);

            entity.Property(i => i.DateCreated)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.Property(i => i.DateModified)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.HasOne(i => i.Project)
                .WithMany(t => t.ProjectItems)
                .HasForeignKey(i => i.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(i => i.Type)
                .WithMany(t => t.ProjectItems)
                .HasForeignKey(i => i.TypeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<ProjectItemExpense>(entity =>
        {
            entity.HasKey(i => i.Id);

            entity.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(i => i.Description)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(i => i.Cost)
                .IsRequired()
                .HasPrecision(12, 2);

            entity.Property(i => i.DateCreated)
               .IsRequired()
               .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.Property(i => i.DateModified)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.HasOne(i => i.ProjectItem)
                .WithMany(t => t.ProjectItemExpenses)
                .HasForeignKey(i => i.ProjectItemId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<ProjectItemSpecification>(entity =>
        {
            entity.HasKey(i => i.Id);

            entity.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(i => i.Description)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(i => i.Value)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(i => i.DateCreated)
               .IsRequired()
               .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.Property(i => i.DateModified)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.HasOne(i => i.ProjectItem)
                .WithMany(t => t.ProjectItemSpecifications)
                .HasForeignKey(i => i.ProjectItemId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<ProjectItemImage>(entity =>
        {
            entity.HasKey(i => i.Id);

            entity.Property(i => i.Image)
                .IsRequired()
                .HasColumnType("MEDIUMBLOB");

            entity.Property(i => i.DateCreated)
               .IsRequired()
               .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.Property(i => i.DateModified)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.HasOne(i => i.ProjectItem)
                 .WithMany(t => t.ProjectItemImages)
                 .HasForeignKey(i => i.ProjectItemId)
                 .OnDelete(DeleteBehavior.Cascade);
        });
        base.OnModelCreating(builder);
    }
}
