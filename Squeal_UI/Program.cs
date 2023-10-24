using Squeal_EL.EmailSenderProcess;
using Squeal_EL.IdentityModels;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Squeal_BL.ImplementationOfManagers;
using Squeal_BL.InterfacesOfManagers;
using Squeal_DL.ImplementationOfManagers;
using Squeal_DL.InterfaceofRepos;
using Squeal_EL.Mappings;
using Squeal_UI.CreateDefaultData;
using System.Globalization;
using Squeal_EL.ViewModels;
using Squeal_UI.Models;

var builder = WebApplication.CreateBuilder(args);


//culture info
var cultureInfo = new CultureInfo("tr-TR");

CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;


// serilog logger ayarlari

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


//contexti ayarliyoruz.
builder.Services.AddDbContext<MyContext>(options =>
{
    //klasik mvcde connection string web configte yer alir.
    //core mvcde connection string appsetting.json dosyasindan alinir.
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyLocal"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

});


//appuser ve approle identity ayari
builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    opt.Password.RequiredLength = 8;
    opt.Password.RequireNonAlphanumeric = true;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireUppercase = true;
    opt.Password.RequireDigit = true;
    opt.User.RequireUniqueEmail = true;
    //opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+&%";

}).AddDefaultTokenProviders().AddEntityFrameworkStores<MyContext>();



//automapper ayari 
builder.Services.AddAutoMapper(a =>
{
    a.AddExpressionMapping();
    a.AddProfile(typeof(Maps));
    a.CreateMap<TivitIndexViewModel, UserTivitDTO>().ReverseMap();
    a.CreateMap<AppUser, ProfileViewModel>().ReverseMap();

});

//interfacelerin DI yasam dongusu
builder.Services.AddScoped<IEmailManager, EmailManager>();

builder.Services.AddScoped<ITivitTagRepo, TivitTagRepo>();
builder.Services.AddScoped<ITivitTagManager, TivitTagManager>();

builder.Services.AddScoped<ITivitMediaRepo, TivitMediaRepo>();
builder.Services.AddScoped<ITivitMediaManager, TivitMediaManager>();

builder.Services.AddScoped<IUserTivitRepo, UserTivitRepo>();
builder.Services.AddScoped<IUserTivitManager, UserTivitManager>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

//Sistem ilk ayaga kalktiginnda rolleri ekleyelim
//ADMIN, MEMBER, WAITINGFORACTIVATION, PASSIVE

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

    CreateData c = new CreateData(logger);
    c.CreateRoles(serviceProvider);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); //login logout
app.UseAuthorization(); //yetki

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
