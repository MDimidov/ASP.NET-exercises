using ForumApp.Core.Contracts;
using ForumApp.Core.Services;
using ForumApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext to IoC container
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
    throw new ArgumentException("Failed to load connection string");
builder.Services.AddDbContext<ForumAppDbContext>(opt =>
{
    opt.UseSqlServer(connectionString);
});

// Add application services to IoC conteiner
builder.Services.AddScoped<IPostService, PostService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
