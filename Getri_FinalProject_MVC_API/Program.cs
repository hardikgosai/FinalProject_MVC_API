using DAL.EntityFramework;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Services.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/Access/Login";    
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    
});

builder.Services.AddDbContext<ApplicationDbContext>
    (options => options.UseSqlServer
    (builder.Configuration.GetConnectionString("ProjectConnection"),
    b => b.MigrationsAssembly("Getri_FinalProject_MVC_API")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();


builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserProfileRepository, UserProfileRepository>();

builder.Services.AddTransient<ILoginRepository, LoginRepository>();

builder.Services.AddSession(Services =>
{
    Services.IdleTimeout = TimeSpan.FromDays(2);
    Services.Cookie.HttpOnly = true;
    Services.Cookie.IsEssential = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=Login}/{id?}");

app.Run();
