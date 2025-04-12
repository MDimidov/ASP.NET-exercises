using HouseRentingSystem.ModelBinders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationSerivces();
builder.Services.AddIdentityServiceExtension();
builder.Services.AddDbServiceExtension(builder.Configuration);

builder.Services.AddControllersWithViews(opt =>
{
    opt.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapDefaultControllerRoute();
app.MapRazorPages();

await app.RunAsync();
