using CarDealershipAPI.Data;
using CarDealershipAPI.Services;
using CarDealershipAPI.Services.Implementations;
using CarDealershipAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

namespace CarDealershipAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            await ConfigureMiddleware(app);

            await app.RunAsync();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Controllers Configuration
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // Handle enum as strings in JSON
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    // Handle null values properly
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    // Use camelCase naming policy
                    options.JsonSerializerOptions.PropertyNamingPolicy = null; // Keep original casing for Arabic support
                });

            // Database Configuration
            services.AddDbContext<CarDealershipDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                });

                // Enable sensitive data logging in development
                if (configuration.GetValue<bool>("Logging:EnableSensitiveDataLogging"))
                {
                    options.EnableSensitiveDataLogging();
                }

                options.EnableDetailedErrors();
            });

            // CORS Configuration
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });

                // Named policy for production
                options.AddPolicy("ProductionCorsPolicy", policy =>
                {
                    policy.WithOrigins("https://yourdomain.com")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });
            });

            // Swagger/OpenAPI Configuration
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Car Dealership API",
                    Version = "v1.0",
                    Description = "API áÅÏÇÑÉ ãÚÑÖ ÇáÓíÇÑÇÊ - Car Dealership Management System",
                    Contact = new OpenApiContact
                    {
                        Name = "Car Dealership Team",
                        Email = "info@cardealership.com"
                    }
                });

                // Include XML comments
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }

                // Add security definition for future JWT implementation
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
            });

            // Logging Configuration
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();

                if (configuration.GetValue<bool>("Logging:EnableFileLogging"))
                {
                    // Add file logging if needed (you can add Serilog here)
                }
            });

            // Memory Caching
            services.AddMemoryCache();

            // HTTP Client Factory
            services.AddHttpClient();

            // Register custom services
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ISaleService, SaleService>();
        }

        private static async Task ConfigureMiddleware(WebApplication app)
        {
            // Exception Handling
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Car Dealership API V1");
                    c.RoutePrefix = string.Empty; // Makes Swagger UI available at root
                    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            // Security Headers
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
                context.Response.Headers.Append("X-Frame-Options", "DENY");
                context.Response.Headers.Append("X-XSS-Protection", "1; mode=block");
                await next();
            });

            // Static files middleware
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            // CORS
            if (app.Environment.IsDevelopment())
            {
                app.UseCors(); // Uses default policy
            }
            else
            {
                app.UseCors("ProductionCorsPolicy");
            }

            // Authentication & Authorization (for future implementation)
            // app.UseAuthentication();
            app.UseAuthorization();

            // API Controllers
            app.MapControllers();

            // Database Migration and Seeding
            await EnsureDatabaseCreated(app);
        }

        private static async Task EnsureDatabaseCreated(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<CarDealershipDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            try
            {
                logger.LogInformation("Ensuring database is created...");

                // Apply pending migrations
                var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
                if (pendingMigrations.Any())
                {
                    logger.LogInformation($"Applying {pendingMigrations.Count()} pending migrations...");
                    await context.Database.MigrateAsync();
                    logger.LogInformation("Database migrations applied successfully.");
                }
                else
                {
                    logger.LogInformation("Database is up to date.");
                }

                // Ensure database is created (for development)
                await context.Database.EnsureCreatedAsync();

                logger.LogInformation("Database initialization completed successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while creating/migrating the database.");
                throw;
            }
        }
    }
}