using Microsoft.AspNetCore.Authentication.JwtBearer;
using CarRental.WebUI.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<TokenHandler>();

builder.Services.AddHttpClient();

builder.Services.AddHttpClient("CarRentalApi", client =>
{
	client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]);
}).AddHttpMessageHandler<TokenHandler>(); ;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddCookie
	(JwtBearerDefaults.AuthenticationScheme, opt =>
	{
		opt.LoginPath = "/Login/Index";
		opt.LogoutPath = "/Login/Logout";
		opt.AccessDeniedPath = "/Login/AccessDenied";
		opt.Cookie.SameSite = SameSiteMode.Strict;
		opt.Cookie.HttpOnly = true;
		opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
		opt.Cookie.Name = "CarRentalAuthCookie";
	});

builder.Services.AddControllersWithViews();

var app = builder.Build();

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

app.MapControllerRoute(
	name: "areas",
	pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Default}/{action=Index}/{id?}");

app.Run();