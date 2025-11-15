using form_task_arda.Data;
using form_task_arda.DTOs;
using form_task_arda.MapperProfiles;
using form_task_arda.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



// 1- Database i�in gerekli importu yap //
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2- Identity i�in gerekli importu yap //
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
	// password kurallarını options (identity) üzerinden oluşturuyorum
	options.Password.RequireDigit = false;
	options.Password.RequireUppercase = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequiredLength = 6;

})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();




// AUTOMAPPER IMPORT  //
builder.Services.AddAutoMapper(typeof(MyAllMappers));

// WARNING :  don't forget import services and libraries at early lines, you CANNOT add services after "Build();" line. If you do, throw error "it's read-only". 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}



app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");


//  IDENTITY-ROLES IMPORT CODES  //
using (var scope = app.Services.CreateScope())
{
	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
	var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

	if (!await roleManager.RoleExistsAsync("Admin"))
	{
		await roleManager.CreateAsync(new IdentityRole("Admin"));
	}

	string adminEmail = "admin@example.com";
	string adminPassword = "Admin123!";

	var adminUser = await userManager.FindByEmailAsync(adminEmail);
	if (adminUser == null)
	{
		adminUser = new AppUser
		{
			UserName = adminEmail,
			Email = adminEmail,
			FullName = "System Administrator",
			EmailConfirmed = true
		};

		await userManager.CreateAsync(adminUser, adminPassword);
		await userManager.AddToRoleAsync(adminUser, "Admin");
	}
}


app.Run();
