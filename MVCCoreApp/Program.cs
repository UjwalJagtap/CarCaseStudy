using Microsoft.EntityFrameworkCore;
using MVCCoreApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Connection string setup
string conString = builder.Configuration.GetConnectionString("sqlcon");

// Register DbContext
builder.Services.AddDbContext<JourneyDbContext>(options => options.UseSqlServer(conString));

// Add MVC services
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Default routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Journey}/{action=Welcome}/{id?}");

app.Run();
