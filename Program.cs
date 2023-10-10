using AspNetCoreIdentityApp.Web.Extenisons;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Proje.Models;
using Proje.OptionsModels;
using Proje.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
});

builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
	options.ValidationInterval = TimeSpan.FromMinutes(30);
});

builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddIdentityWithExt();
builder.Services.AddScoped<IEmailService, EmailService>();
// 1 saat ara ile e-mail onay kontrol servisi 
builder.Services.AddHostedService<VerificationExpiryService>();

builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromSeconds(900);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});


builder.Services.ConfigureApplicationCookie(options =>
{
	options.Cookie.Name = "AppCookie";
	options.LoginPath = new PathString("/User/Signin");
	options.LogoutPath = new PathString("/Member/logout");
	options.AccessDeniedPath = new PathString("/Member/AccessDenied");
	options.ExpireTimeSpan = TimeSpan.FromDays(60);
	options.SlidingExpiration = true;

	options.Events.OnRedirectToAccessDenied = context =>
	{
		context.Response.Redirect("/Member/AccessDenied");
		return Task.CompletedTask;
	};

	options.Events.OnRedirectToLogin = context =>
	{
		if (context.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
		{
			context.Response.StatusCode = StatusCodes.Status401Unauthorized;
			context.Response.WriteAsync("Unauthorized");

		}
		else
		{
			context.Response.Redirect(context.RedirectUri);
		}
		return Task.CompletedTask;
	};
});

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
	options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
});



var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "areas",
	pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();