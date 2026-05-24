using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using GNEZDO.Data;
using GNEZDO.Models;
using GNEZDO.Repositories;
using GNEZDO.Services;
using GNEZDO.Validation;
using GNEZDO.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Database (SQLite)
builder.Services.AddDbContext<GnezdoContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false; // ✅ Добавлено
    options.Password.RequireNonAlphanumeric = false; // ✅ Добавлено
    options.User.RequireUniqueEmail = true; // ✅ Добавлено
    options.Lockout.AllowedForNewUsers = true;
})
.AddEntityFrameworkStores<GnezdoContext>()
.AddDefaultTokenProviders();

// Настройка Cookie для входа
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
    options.LogoutPath = "/logout";
    options.AccessDeniedPath = "/access-denied";
});

// Repositories (DI)
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<IForumRepository, ForumRepository>();
builder.Services.AddScoped<ISpecialistRepository, SpecialistRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Services (DI)
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IForumService, ForumService>();
builder.Services.AddScoped<ISpecialistService, SpecialistService>();
builder.Services.AddScoped<IUserService, UserService>();

// Validation (FluentValidation)
builder.Services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

// ✅ MapRazorComponents ПОСЛЕ аутентификации
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapGet("/logout", async (SignInManager<User> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.Redirect("/login");
});

// === Применение миграций при запуске ===
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<GnezdoContext>();

    try
    {
        var pending = dbContext.Database.GetPendingMigrations();
        if (pending.Any())
        {
            Console.WriteLine($"[DB] Applying {pending.Count()} migrations: {string.Join(", ", pending)}");
        }

        dbContext.Database.Migrate();
        Console.WriteLine("[DB] Database migration completed successfully");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[DB ERROR] Migration failed: {ex.Message}");
        // Не останавливаем приложение — пусть работает без БД для демонстрации
    }
}
// ===============================================

app.Run();

// Делаем класс Program доступным для интеграционных тестов
public partial class Program { }