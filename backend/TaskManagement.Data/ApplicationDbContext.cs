using Microsoft.EntityFrameworkCore;
using TaskManagement.Primitives;


namespace TaskManagement.Data;

public class ApplicationDbContext : DbContext
{
	public DbSet<TaskEntity> Tasks {
		get; set;
	}

	public DbSet<User> Users {
		get; set;
	}

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		modelBuilder.Entity<TaskEntity>(builder => {
			builder.HasKey(t => t.Id);
			builder.Property(t => t.Id)
				.IsRequired();

			builder.Property(x => x.Name)
				.HasMaxLength(512)
				.IsRequired();
			builder.HasIndex(x => x.Name)
				.IsUnique();

			builder.HasOne(t => t.User)
				.WithMany(u => u.Tasks)
				.HasForeignKey(t => t.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Property(x => x.ModifiedAt)
				.IsRequired(false);
		});

		modelBuilder.Entity<User>(builder => {
			builder.HasKey(t => t.Id);
			builder.Property(x => x.Id)
				.UseIdentityAlwaysColumn()
				.ValueGeneratedOnAdd();

			builder.Property(x => x.Username)
				.HasMaxLength(512)
				.IsRequired();
			builder.HasIndex(x => x.Username)
				.IsUnique();
		});

		base.OnModelCreating(modelBuilder);
	}

}