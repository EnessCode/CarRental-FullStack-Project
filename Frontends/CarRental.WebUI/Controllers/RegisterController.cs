using CarRental.Dto.RegistersDto;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace CarRental.WebUI.Controllers
{
	public class RegisterController(IHttpClientFactory httpClientFactory) : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(RegisterDto registerDto)
		{
			var client = httpClientFactory.CreateClient("CarRentalApi");
			var content = new StringContent(JsonSerializer.Serialize(registerDto), Encoding.UTF8, "application/json");
			var response = await client.PostAsync("Register", content);

			if (response.IsSuccessStatusCode)
			{
				return RedirectToAction("Index", "Login");
			}
			return View();
		}
	}
}
