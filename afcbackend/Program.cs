using Afc.Core.Data;
using Afc.Repository.Implementations;
using Afc.Business.Interfaces;
using Afc.Business.Implementations;
using Afc.Business.Services;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// ============================================================================
// 1. ADD CONTROLLERS & API EXPLORER
// ============================================================================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "AFC Backend API",
        Version = "v1",
        Description = "API for AFC Food Court Management System"
    });
});

// ============================================================================
// 2. CONFIGURE CORS (Cross-Origin Resource Sharing)
// ============================================================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")  // Vite default port
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// ============================================================================
// 3. CONFIGURE DATABASE CONTEXT WITH SNAKE_CASE NAMING
// ============================================================================
builder.Services.AddDbContext<AfcDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsqlOptions =>
        {
            npgsqlOptions.EnableRetryOnFailure(
                maxRetryCount: 3,
                maxRetryDelay: TimeSpan.FromSeconds(5),
                errorCodesToAdd: null
            );
        }
    )
    .UseSnakeCaseNamingConvention()  // ✅ Auto-convert PascalCase to snake_case
    .EnableSensitiveDataLogging(builder.Environment.IsDevelopment())  // Only in dev
    .EnableDetailedErrors(builder.Environment.IsDevelopment());  // Only in dev
});

// ============================================================================
// 4. REGISTER APPLICATION SERVICES (Dependency Injection)
// ============================================================================

// Services (Business Logic Layer)
builder.Services.AddScoped<JwtService>();

// Repositories (Data Access Layer)
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Business Logic
builder.Services.AddScoped<IUserBusiness, UserBusiness>();

// ============================================================================
// 5. CONFIGURE LOGGING
// ============================================================================
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// ============================================================================
// 6. BUILD THE APPLICATION
// ============================================================================
var app = builder.Build();

// ============================================================================
// 7. CONFIGURE HTTP REQUEST PIPELINE (MIDDLEWARE)
// ============================================================================

// Development-specific middleware
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "AFC API V1");
        options.RoutePrefix = "swagger";  // Access at: http://localhost:7116/swagger
    });
    app.UseDeveloperExceptionPage();
//}
//else
//{
//    // Production error handling
//    app.UseExceptionHandler("/Error");
//    app.UseHsts();  // HTTP Strict Transport Security
//}

// Redirect HTTP to HTTPS
app.UseHttpsRedirection();

// Static files (if needed in future)
// app.UseStaticFiles();

// IMPORTANT: Middleware order matters!
app.UseRouting();

// CORS must be between UseRouting and UseAuthorization
app.UseCors("AllowFrontend");

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// Map API Controllers
app.MapControllers();

// ============================================================================
// 8. DATABASE MIGRATION & SEEDING (Optional - Auto-apply migrations)
// ============================================================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AfcDbContext>();

        // Remove the 'if IsDevelopment' check so it runs on Render too!
        await context.Database.MigrateAsync();
        Console.WriteLine("✅ Database migrations applied successfully.");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "❌ An error occurred while migrating the database.");
    }
}

// ============================================================================
// 9. RUN THE APPLICATION
// ============================================================================
Console.WriteLine(" AFC Backend API is running...");
Console.WriteLine($" Environment: {app.Environment.EnvironmentName}");
Console.WriteLine($" Swagger UI: https://localhost:7116/swagger");

app.Run();