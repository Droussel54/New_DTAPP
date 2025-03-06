using JT.AspNetBaseRoleProvider;

using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;

using New_DTAPP.Models;
using New_DTAPP.Data;
using New_DTAPP.Repository;
using New_DTAPP.Security;
using New_DTAPP.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddDbContextPool<NewDtappContext>(options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection",
//                o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

builder.Services.AddDbContext<NewDtappContext>(options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"), ServiceLifetime.Transient);

builder.Services.AddAuthentication(IISDefaults.AuthenticationScheme);
//builder.Services.AddJTRoleAuthorization<UserRoleProvider>();

builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddScoped<IOperationRepository, OperationRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ISystemRepository, SystemRepository>();

builder.Services.AddScoped<ITransferRepository, TransferRepository>();

builder.Services.AddScoped<IUnitRepository, UnitRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITransferTypeRepository, TransferTypeRepository>();

builder.Services.AddMvc().AddViewOptions(options =>
    options.HtmlHelperOptions.ClientValidationEnabled = false);

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
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();