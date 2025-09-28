using Microsoft.EntityFrameworkCore;
using CarDealershipAPI.Models;
using System.Reflection;

namespace CarDealershipAPI.Data
{
    public class CarDealershipDbContext : DbContext
    {
        public CarDealershipDbContext(DbContextOptions<CarDealershipDbContext> options) : base(options)
        {
        }

       
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Sale> Sales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            ConfigureCar(modelBuilder);

            ConfigureCustomer(modelBuilder);

            ConfigureEmployee(modelBuilder);

            ConfigureSale(modelBuilder);

            SeedData(modelBuilder);
        }

        private void ConfigureCar(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity =>
            {
                entity.ToTable("Cars");

                entity.HasKey(c => c.Id);

                entity.Property(c => c.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(c => c.Make)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(c => c.Model)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(c => c.Price)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(c => c.VIN)
                    .HasMaxLength(17);

                entity.Property(c => c.Color)
                    .HasMaxLength(50)
                    .HasDefaultValue("أبيض");

                entity.Property(c => c.CreatedDate)
                    .HasDefaultValueSql("GETDATE()");

   
                entity.HasIndex(c => c.VIN).IsUnique();
                entity.HasIndex(c => new { c.Make, c.Model, c.Year });
                entity.HasIndex(c => c.Status);
            });
        }

        private void ConfigureCustomer(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customers");

                entity.HasKey(c => c.Id);

                entity.Property(c => c.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(c => c.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(c => c.Phone)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(c => c.IdentityNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(c => c.IdentityType)
                    .HasMaxLength(20)
                    .HasDefaultValue("NationalID");

                entity.Property(c => c.Address)
                    .HasMaxLength(200);

                entity.Property(c => c.CreatedDate)
                    .HasDefaultValueSql("GETDATE()");

                // Indexes
                entity.HasIndex(c => c.Email).IsUnique();
                entity.HasIndex(c => c.IdentityNumber).IsUnique();
                entity.HasIndex(c => c.Phone);
            });
        }

        private void ConfigureEmployee(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employees");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.IdentityNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValue("SalesRep");

                entity.Property(e => e.Department)
                    .HasMaxLength(50)
                    .HasDefaultValue("Sales");

                entity.Property(e => e.BaseSalary)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(e => e.CommissionRate)
                    .HasColumnType("decimal(5,4)")
                    .HasDefaultValue(0.05m);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValue("Active");

                entity.Property(e => e.TotalSalesValue)
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0);

                entity.Property(e => e.TotalCommissionEarned)
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0);

                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("GETDATE()");


                entity.HasOne(e => e.Supervisor)
                    .WithMany(e => e.Subordinates)
                    .HasForeignKey(e => e.SupervisorId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Indexes
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.IdentityNumber).IsUnique();
                entity.HasIndex(e => e.Role);
                entity.HasIndex(e => e.Status);
            });
        }

        private void ConfigureSale(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sale>(entity =>
            {
                entity.ToTable("Sales");

                entity.HasKey(s => s.Id);

                entity.Property(s => s.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(s => s.SalePrice)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(s => s.Discount)
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0);

                entity.Property(s => s.Tax)
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0);

                entity.Property(s => s.TaxRate)
                    .HasColumnType("decimal(5,2)")
                    .HasDefaultValue(14);

                entity.Property(s => s.TotalAmount)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(s => s.PaymentMethod)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValue("Cash");

                entity.Property(s => s.Status)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValue("Pending");

                entity.Property(s => s.InvoiceNumber)
                    .HasMaxLength(50);

                entity.Property(s => s.EmployeeCommission)
                    .HasColumnType("decimal(18,2)");

                entity.Property(s => s.SaleDate)
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(s => s.CreatedDate)
                    .HasDefaultValueSql("GETDATE()");

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

                entity.HasIndex(s => s.SaleDate);
                entity.HasIndex(s => s.Status);
                entity.HasIndex(s => s.InvoiceNumber).IsUnique();
                entity.HasIndex(s => new { s.EmployeeId, s.SaleDate });
                entity.HasIndex(s => new { s.CustomerId, s.SaleDate });
            });
        }

        private void SeedData(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "أحمد محمد علي",
                    Email = "ahmed.manager@dealership.com",
                    Phone = "+201234567890",
                    IdentityNumber = "28901234567890",
                    HireDate = new DateTime(2020, 1, 15),
                    Role = "Manager",
                    Department = "Sales",
                    BaseSalary = 15000.00m,
                    CommissionRate = 0.10m,
                    Status = "Active",
                    CreatedDate = DateTime.Now
                },
                new Employee
                {
                    Id = 2,
                    Name = "فاطمة أحمد حسن",
                    Email = "fatma.sales@dealership.com",
                    Phone = "+201234567891",
                    IdentityNumber = "29001234567891",
                    HireDate = new DateTime(2021, 3, 10),
                    Role = "SalesRep",
                    Department = "Sales",
                    BaseSalary = 8000.00m,
                    CommissionRate = 0.05m,
                    Status = "Active",
                    SupervisorId = 1,
                    CreatedDate = DateTime.Now
                },
                new Employee
                {
                    Id = 3,
                    Name = "محمد عبد الله سالم",
                    Email = "mohamed.sales@dealership.com",
                    Phone = "+201234567892",
                    IdentityNumber = "29101234567892",
                    HireDate = new DateTime(2021, 6, 1),
                    Role = "SalesRep",
                    Department = "Sales",
                    BaseSalary = 7500.00m,
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
                    Model = "Camry",
                    Year = 2023,
                    Price = 450000.00m,
                    VIN = "1HGBH41JXMN109186",
                    Color = "أبيض",
                    Mileage = 0,
                    FuelType = "Gasoline",
                    Transmission = "Automatic",
                    Condition = "New",
                    Status = "Available",
                    Doors = 4,
                    Seats = 5,
                    Description = "تويوتا كامري 2023 جديدة بالكامل",
                    CreatedDate = DateTime.Now
                },
                new Car
                {
                    Id = 2,
                    Make = "Honda",
                    Model = "Civic",
                    Year = 2022,
                    Price = 380000.00m,
                    VIN = "2HGFC2F59NH123456",
                    Color = "أسود",
                    Mileage = 15000,
                    FuelType = "Gasoline",
                    Transmission = "Manual",
                    Condition = "Used",
                    Status = "Available",
                    Doors = 4,
                    Seats = 5,
                    Description = "هوندا سيفيك 2022 مستعملة بحالة ممتازة",
                    CreatedDate = DateTime.Now
                },
                new Car
                {
                    Id = 3,
                    Make = "BMW",
                    Model = "X3",
                    Year = 2024,
                    Price = 850000.00m,
                    VIN = "WBXHT9C5XP5A12345",
                    Color = "أزرق",
                    Mileage = 0,
                    FuelType = "Gasoline",
                    Transmission = "Automatic",
                    Condition = "New",
                    Status = "Available",
                    Doors = 4,
                    Seats = 5,
                    Description = "BMW X3 2024 فئة فاخرة",
                    CreatedDate = DateTime.Now
                }
            );

            // Seed Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1,
                    Name = "سارة محمود إبراهيم",
                    Email = "sara.ibrahim@email.com",
                    Phone = "+201111111111",
                    Address = "شارع النيل، المعادي، القاهرة",
                    IdentityNumber = "29012345678901",
                    IdentityType = "NationalID",
                    DateOfBirth = new DateTime(1990, 5, 15),
                    Gender = "Female",
                    CreatedDate = DateTime.Now
                },
                new Customer
                {
                    Id = 2,
                    Name = "خالد أحمد عثمان",
                    Email = "khaled.osman@email.com",
                    Phone = "+201222222222",
                    Address = "شارع الهرم، الجيزة",
                    IdentityNumber = "28512345678902",
                    IdentityType = "NationalID",
                    DateOfBirth = new DateTime(1985, 10, 20),
                    Gender = "Male",
                    CreatedDate = DateTime.Now
                }
            );
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is Car || e.Entity is Customer || e.Entity is Employee || e.Entity is Sale)
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Property("CreatedDate").CurrentValue == null)
                        entry.Property("CreatedDate").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("UpdatedDate").CurrentValue = DateTime.Now;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}