using CarDealershipAPI.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace CarDealershipAPI.Data
{
    public class CarDealershipContext : DbContext
    {
        public CarDealershipContext(DbContextOptions<CarDealershipContext> options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<TestDrive> TestDrives { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Make).IsRequired().HasMaxLength(50);
                entity.Property(c => c.Model).IsRequired().HasMaxLength(50);
                entity.Property(c => c.Year).IsRequired();
                entity.Property(c => c.Price).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(c => c.VIN).HasMaxLength(17);
                entity.Property(c => c.Color).HasMaxLength(50).HasDefaultValue("أبيض");
                entity.Property(c => c.FuelType).HasMaxLength(20).HasDefaultValue("Gasoline");
                entity.Property(c => c.Transmission).HasMaxLength(20).HasDefaultValue("Manual");
                entity.Property(c => c.Condition).HasMaxLength(20).HasDefaultValue("Used");
                entity.Property(c => c.Status).IsRequired().HasMaxLength(20).HasDefaultValue("Available");
                entity.Property(c => c.CreatedDate).HasDefaultValueSql("GETDATE()");

                entity.HasIndex(c => c.Make);
                entity.HasIndex(c => c.Model);
                entity.HasIndex(c => c.Year);
                entity.HasIndex(c => c.Price);
                entity.HasIndex(c => c.VIN).IsUnique();
                entity.HasIndex(c => c.Status);
                entity.HasIndex(c => new { c.Make, c.Model, c.Year });
            });

    
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Phone).IsRequired().HasMaxLength(20);
                entity.Property(c => c.Email).HasMaxLength(100);
                entity.Property(c => c.Address).HasMaxLength(200);
                entity.Property(c => c.Budget).HasColumnType("decimal(18,2)");
                entity.Property(c => c.MonthlyIncome).HasColumnType("decimal(18,2)");
                entity.Property(c => c.Status).IsRequired().HasMaxLength(20).HasDefaultValue("Lead");
                entity.Property(c => c.PreferredLanguage).HasMaxLength(10).HasDefaultValue("Arabic");
                entity.Property(c => c.PreferredContactMethod).HasMaxLength(20).HasDefaultValue("Phone");
                entity.Property(c => c.CreatedDate).HasDefaultValueSql("GETDATE()");

                entity.HasIndex(c => c.Phone).IsUnique();
                entity.HasIndex(c => c.Email).IsUnique();
                entity.HasIndex(c => c.Status);
                entity.HasIndex(c => c.Budget);
                entity.HasIndex(c => new { c.Name, c.Phone });
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Phone).IsRequired().HasMaxLength(20);
                entity.Property(e => e.IdentityNumber).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Role).IsRequired().HasMaxLength(50).HasDefaultValue("SalesRep");
                entity.Property(e => e.Department).HasMaxLength(50).HasDefaultValue("Sales");
                entity.Property(e => e.BaseSalary).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.CommissionRate).HasColumnType("decimal(5,4)").HasDefaultValue(0.05m);
                entity.Property(e => e.Status).IsRequired().HasMaxLength(20).HasDefaultValue("Active");
                entity.Property(e => e.EmploymentType).HasMaxLength(20).HasDefaultValue("FullTime");
                entity.Property(e => e.TotalSalesValue).HasColumnType("decimal(18,2)").HasDefaultValue(0);
                entity.Property(e => e.TotalCommissionEarned).HasColumnType("decimal(18,2)").HasDefaultValue(0);
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.Supervisor)
                    .WithMany(e => e.Subordinates)
                    .HasForeignKey(e => e.SupervisorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.IdentityNumber).IsUnique();
                entity.HasIndex(e => e.Role);
                entity.HasIndex(e => e.Status);
                entity.HasIndex(e => e.Department);
            });


            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.SalePrice).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(s => s.OriginalCarPrice).HasColumnType("decimal(18,2)");
                entity.Property(s => s.Discount).HasColumnType("decimal(18,2)").HasDefaultValue(0);
                entity.Property(s => s.Tax).HasColumnType("decimal(18,2)").HasDefaultValue(0);
                entity.Property(s => s.TotalAmount).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(s => s.PaymentMethod).IsRequired().HasMaxLength(20).HasDefaultValue("Cash");
                entity.Property(s => s.DownPayment).HasColumnType("decimal(18,2)").HasDefaultValue(0);
                entity.Property(s => s.RemainingAmount).HasColumnType("decimal(18,2)").HasDefaultValue(0);
                entity.Property(s => s.Status).IsRequired().HasMaxLength(20).HasDefaultValue("Pending");
                entity.Property(s => s.EmployeeCommission).HasColumnType("decimal(18,2)");
                entity.Property(s => s.ProfitMargin).HasColumnType("decimal(18,2)");
                entity.Property(s => s.TradeInValue).HasColumnType("decimal(18,2)");
                entity.Property(s => s.CreatedDate).HasDefaultValueSql("GETDATE()");

                entity.HasOne(s => s.Car)
                    .WithMany(c => c.Sales)
                    .HasForeignKey(s => s.CarId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(s => s.Customer)
                    .WithMany(c => c.Sales)
                    .HasForeignKey(s => s.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(s => s.Employee)
                    .WithMany(e => e.Sales)
                    .HasForeignKey(s => s.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(s => s.TestDrive)
                    .WithOne(td => td.Sale)
                    .HasForeignKey<Sale>(s => s.TestDriveId)
                    .OnDelete(DeleteBehavior.SetNull);

         
                entity.HasIndex(s => s.SaleDate);
                entity.HasIndex(s => s.Status);
                entity.HasIndex(s => s.PaymentMethod);
                entity.HasIndex(s => s.CarId);
                entity.HasIndex(s => s.CustomerId);
                entity.HasIndex(s => s.EmployeeId);
                entity.HasIndex(s => new { s.SaleDate, s.Status });
            });

            modelBuilder.Entity<TestDrive>(entity =>
            {
                entity.HasKey(td => td.Id);
                entity.Property(td => td.ScheduledDateTime).IsRequired();
                entity.Property(td => td.DurationMinutes).IsRequired().HasDefaultValue(30);
                entity.Property(td => td.Status).IsRequired().HasMaxLength(20).HasDefaultValue("Scheduled");
                entity.Property(td => td.StartLocation).HasMaxLength(200).HasDefaultValue("المعرض");
                entity.Property(td => td.HasValidLicense).HasDefaultValue(true);
                entity.Property(td => td.IsInsured).HasDefaultValue(true);
                entity.Property(td => td.RequiresFollowUp).HasDefaultValue(true);
                entity.Property(td => td.FollowUpPriority).HasMaxLength(20).HasDefaultValue("Medium");
                entity.Property(td => td.DriveType).HasMaxLength(50).HasDefaultValue("Mixed");
                entity.Property(td => td.FuelCost).HasColumnType("decimal(18,2)");
                entity.Property(td => td.MaintenanceCost).HasColumnType("decimal(18,2)");
                entity.Property(td => td.RepairCost).HasColumnType("decimal(18,2)");
                entity.Property(td => td.ProposedPrice).HasColumnType("decimal(18,2)");
                entity.Property(td => td.CreatedDate).HasDefaultValueSql("GETDATE()");

  
                entity.HasOne(td => td.Car)
                    .WithMany(c => c.TestDrives)
                    .HasForeignKey(td => td.CarId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(td => td.Customer)
                    .WithMany(c => c.TestDrives)
                    .HasForeignKey(td => td.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(td => td.Employee)
                    .WithMany(e => e.TestDrives)
                    .HasForeignKey(td => td.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(td => td.Sale)
                    .WithOne(s => s.TestDrive)
                    .HasForeignKey<TestDrive>(td => td.SaleId)
                    .OnDelete(DeleteBehavior.SetNull);

            
                entity.HasIndex(td => td.ScheduledDateTime);
                entity.HasIndex(td => td.Status);
                entity.HasIndex(td => td.CarId);
                entity.HasIndex(td => td.CustomerId);
                entity.HasIndex(td => td.EmployeeId);
                entity.HasIndex(td => td.IsConvertedToSale);
                entity.HasIndex(td => new { td.ScheduledDateTime, td.Status });
                entity.HasIndex(td => new { td.CarId, td.ScheduledDateTime });
            });

         
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal) || p.PropertyType == typeof(decimal?));
                foreach (var property in properties)
                {
                    if (!modelBuilder.Entity(entityType.Name).Property(property.Name).Metadata.GetAnnotations().Any(a => a.Name == "Relational:ColumnType"))
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name).HasColumnType("decimal(18,2)");
                    }
                }
            }

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var dateProperties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(DateTime?));
                foreach (var property in dateProperties)
                {
                    modelBuilder.Entity(entityType.Name).Property(property.Name).HasColumnType("datetime2");
                }
            }

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
        
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "أحمد محمد",
                    Email = "admin@cardealership.com",
                    Phone = "01234567890",
                    IdentityNumber = "12345678901234",
                    Role = "Manager",
                    Department = "Management",
                    BaseSalary = 15000,
                    CommissionRate = 0.02m,
                    Status = "Active",
                    CreatedDate = DateTime.Now
                },
                new Employee
                {
                    Id = 2,
                    Name = "فاطمة علي",
                    Email = "sales1@cardealership.com",
                    Phone = "01234567891",
                    IdentityNumber = "12345678901235",
                    Role = "SalesRep",
                    Department = "Sales",
                    BaseSalary = 8000,
                    CommissionRate = 0.05m,
                    Status = "Active",
                    SupervisorId = 1,
                    CreatedDate = DateTime.Now
                }
            );


            modelBuilder.Entity<Car>().HasData(
                new Car
                {
                    Id = 1,
                    Make = "Toyota",
                    Model = "Corolla",
                    Year = 2022,
                    Price = 450000,
                    Color = "أبيض",
                    Mileage = 15000,
                    FuelType = "Gasoline",
                    Transmission = "Automatic",
                    Status = "Available",
                    CreatedDate = DateTime.Now
                },
                new Car
                {
                    Id = 2,
                    Make = "Hyundai",
                    Model = "Elantra",
                    Year = 2021,
                    Price = 380000,
                    Color = "أسود",
                    Mileage = 25000,
                    FuelType = "Gasoline",
                    Transmission = "Manual",
                    Status = "Available",
                    CreatedDate = DateTime.Now
                }
            );


            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1,
                    Name = "محمد أحمد",
                    Phone = "01098765432",
                    Email = "customer1@example.com",
                    Budget = 500000,
                    Status = "Lead",
                    PreferredMake = "Toyota",
                    CreatedDate = DateTime.Now
                }
            );
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.Property("UpdatedDate") != null)
                {
                    entry.Property("UpdatedDate").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Added && entry.Property("CreatedDate") != null)
                {
                    entry.Property("CreatedDate").CurrentValue = DateTime.Now;
                }
            }
        }
    }
}

