using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RealEstateAgency.Core.Entities;
using RealEstateAgency.Core.Interfaces;
using RealEstateAgency.Core.Resources;
using RealEstateAgency.Infrastructure.Data;
using RealEstateAgency.Infrastructure.Repositories;
using RealEstateAgency.Service.BuildingPlanService;
using RealEstateAgency.Service.BuildingTypeService;
using RealEstateAgency.Service.CategoryService;
using RealEstateAgency.Service.EstateConditionService;
using RealEstateAgency.Service.EstateOptionService;
using RealEstateAgency.Service.EstateService;
using RealEstateAgency.Service.ImageService;
using RealEstateAgency.Service.Mappers.UserRoleMapper;
using RealEstateAgency.Service.RoleService;
using RealEstateAgency.Service.UserService;
using RealEstateAgency.Service.ZoneService;
using System.Globalization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(options =>
    {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
        {
            var assemblyName = new AssemblyName(typeof(UILabel).GetTypeInfo().Assembly.FullName);
            return factory.Create(nameof(UILabel), assemblyName.Name);
        };
    });

builder.Services.Configure<RequestLocalizationOptions>(
    options =>
    {
        var supportedCultures = new List<CultureInfo>
        {
            new CultureInfo("en-US"),
            new CultureInfo("ro-RO"),
            new CultureInfo("ru-RU")
        };

        options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;
        foreach (var culture in options.SupportedUICultures)
        {
            culture.NumberFormat = NumberFormatInfo.InvariantInfo;
        }

        options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
    });

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<AgentUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<Role>()
    .AddUserStore<UserStore<AgentUser, Role, ApplicationDbContext, Guid>>()
    .AddRoleStore<RoleStore<Role, ApplicationDbContext, Guid>>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IBuildingPlanService, BuildingPlanService>();
builder.Services.AddScoped<IBuildingTypeService, BuildingTypeService>();
builder.Services.AddScoped<IEstateConditionService, EstateConditionService>();
builder.Services.AddScoped<IEstateOptionService, EstateOptionService>();
builder.Services.AddScoped<IZoneService, ZoneService>();
builder.Services.AddScoped<IEstateService, EstateService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IUserRoleMapper, UserRoleMapper>();

var app = builder.Build();

var locOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(locOptions.Value);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.MapRazorPages();

app.Run();
