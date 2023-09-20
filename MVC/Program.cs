using AppCore.DataAccess.EntityFramework.Bases;
using Business.Services;
using DataAccess;
using DataAccess.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//DbContext için baðýmlýlýk yönetimi
var connectionString = builder.Configuration.GetConnectionString("Context");
builder.Services.AddDbContext<DContext>( options => options.UseSqlServer(connectionString));
//bagýmlýlýklar
builder.Services.AddScoped(typeof(RepoBase<>),typeof(Repo<>));
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(config =>
    {
        config.LoginPath = "/Account/Users/Login";
        config.AccessDeniedPath = "/Account/Users/AccessDenied";
        config.ExpireTimeSpan = TimeSpan.FromHours(1);
        config.SlidingExpiration = true;
    });

List<CultureInfo> cultures = new List<CultureInfo>()
{
    new CultureInfo("en-US") 
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture(cultures.FirstOrDefault().Name);
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});

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

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
