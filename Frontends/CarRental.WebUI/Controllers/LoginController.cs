using CarRental.Dto.LoginDtos;
using CarRental.WebUI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace CarRental.WebUI.Controllers
{
	public class LoginController(IHttpClientFactory httpClientFactory) : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(LoginDto loginDto)
		{
			var client = httpClientFactory.CreateClient("CarRentalApi");
			var content = new StringContent(JsonSerializer.Serialize(loginDto), Encoding.UTF8, "application/json");

			var response = await client.PostAsync("Login", content);

			if (response.IsSuccessStatusCode)
			{
				var jsonData = await response.Content.ReadAsStringAsync();

				var tokenModel = JsonSerializer.Deserialize<JwtResponseModel>(jsonData, new JsonSerializerOptions
				{
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase
				});

				if (tokenModel != null)
				{
					JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
					var token = handler.ReadJwtToken(tokenModel.Token);
					var claims = token.Claims.ToList();

					claims.Add(new Claim("accessToken", tokenModel.Token));

					var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
					var authProperties = new AuthenticationProperties
					{
						ExpiresUtc = tokenModel.ExpireDate,
						IsPersistent = true
					};

					await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

					if (token.Claims.FirstOrDefault(x => x.Value == "Admin") != null)
					{
						return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
					}

					return RedirectToAction("Index", "Default");
				}
			}

			ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı!");
			return View();
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);
			return RedirectToAction("Index", "Login");
		}
	}
}