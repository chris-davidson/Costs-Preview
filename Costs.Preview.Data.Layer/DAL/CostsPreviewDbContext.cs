using Costs.Preview.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Costs.Preview.DAL
{
    public class CostsPreviewDbContext : DbContext
	{
		public CostsPreviewDbContext() : base("CostsPreviewDb")
		{
		}

		public DbSet<DependentModel> Dependents { get; set; }
		public DbSet<EmployeeModel> Employees { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
            base.OnModelCreating(modelBuilder);
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new DependentConfiguration());
            modelBuilder.Configurations.Add(new EmployeeConfiguration());

			modelBuilder.Entity<EmployeeModel>()
				.HasMany<DependentModel>(e => e.Dependents);
		}
	}

    // Fluent API
    public class DependentConfiguration:EntityTypeConfiguration<DependentModel>
    {
        public DependentConfiguration()
        {
            Property(d => d.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            Property(d => d.LastName)
                .IsRequired()
                .HasMaxLength(50);
        }
    }

    public class EmployeeConfiguration : EntityTypeConfiguration<EmployeeModel>
    {
        public EmployeeConfiguration()
        {
            Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);

			Property(e => e.PayPerPeriod)
				.IsRequired();
        }
    }
}
