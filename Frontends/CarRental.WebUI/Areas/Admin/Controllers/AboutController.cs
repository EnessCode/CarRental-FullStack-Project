using CarRental.Dto;
using CarRental.Dto.AboutDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CarRental.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class AboutController(IHttpClientFactory httpClientFactory) : BaseAdminController
	{
		public async Task<IActionResult> Index()
		{
			var client = httpClientFactory.CreateClient("CarRentalApi");
			var responseMessage = await client.GetAsync("Abouts");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var apiResponse = JsonConvert.DeserializeObject<ResultApiResponseDto<List<ResultAboutDto>>>(jsonData);
				if (apiResponse != null && apiResponse.Success)
				{
					return View(apiResponse.Data);
				}
			}
			return View(new List<ResultAboutDto>());
		}

		[HttpGet]
		public async Task<IActionResult> CreateAbout()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
		{
			var client = httpClientFactory.CreateClient("CarRentalApi");
			var jsonData = JsonConvert.SerializeObject(createAboutDto);
			var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PostAsync("Abouts", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index", "About", new { area = "Admin" });
			}
			return View(createAboutDto);
		}

		public async Task<IActionResult> DeleteAbout(int id)
		{
			var client = httpClientFactory.CreateClient("CarRentalApi");
			var responseMessage = await client.DeleteAsync("Abouts/" + id);

			return RedirectToAction("Index", "About", new { area = "Admin" });
		}

		[HttpGet]
		public async Task<IActionResult> UpdateAbout(int id)
		{
			var client = httpClientFactory.CreateClient("CarRentalApi");
			var responseMessage = await client.GetAsync("Abouts/" + id);
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var apiResponse = JsonConvert.DeserializeObject<ResultApiResponseDto<UpdateAboutDto>>(jsonData);
				if (apiResponse != null && apiResponse.Success)
				{
					return View(apiResponse.Data);
				}
			}
			return RedirectToAction("Index", "About", new { area = "Admin" });
		}

		[HttpPost]
		public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
		{
			var client = httpClientFactory.CreateClient("CarRentalApi");
			var jsonData = JsonConvert.SerializeObject(updateAboutDto);
			var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PutAsync("Abouts", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index", "About", new { area = "Admin" });
			}
			return View(updateAboutDto);
		}
	}
}
