using CarRental.Dto;
using CarRental.Dto.ServiceDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CarRental.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ServiceController(IHttpClientFactory httpClientFactory) : BaseAdminController
	{
		public async Task<IActionResult> Index()
		{
			var client = httpClientFactory.CreateClient("CarRentalApi");
			var responseMessage = await client.GetAsync("Services");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var apiResponse = JsonConvert.DeserializeObject<ResultApiResponseDto<List<ResultServiceDto>>>(jsonData);
				if (apiResponse != null && apiResponse.Success)
				{
					return View(apiResponse.Data);
				}
			}
			return View(new List<ResultServiceDto>());
		}

		[HttpGet]
		public async Task<IActionResult> CreateService()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateService(CreateServiceDto createServiceDto)
		{
			var client = httpClientFactory.CreateClient("CarRentalApi");
			var jsonData = JsonConvert.SerializeObject(createServiceDto);
			var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PostAsync("Services", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index", "Service", new { area = "Admin" });
			}
			return View(createServiceDto);
		}
		public async Task<IActionResult> DeleteService(int id)
		{
			var client = httpClientFactory.CreateClient("CarRentalApi");
			var responseMessage = await client.DeleteAsync("Services/" + id);

			return RedirectToAction("Index", "Service", new { area = "Admin" });
		}

		[HttpGet]
		public async Task<IActionResult> UpdateService(int id)
		{
			var client = httpClientFactory.CreateClient("CarRentalApi");
			var responseMessage = await client.GetAsync("Services/" + id);
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var apiResponse = JsonConvert.DeserializeObject<ResultApiResponseDto<UpdateServiceDto>>(jsonData);
				if (apiResponse != null && apiResponse.Success)
				{
					return View(apiResponse.Data);
				}
			}
			return RedirectToAction("Index", "Service", new { area = "Admin" });
		}

		[HttpPost]
		public async Task<IActionResult> UpdateService(UpdateServiceDto updateServiceDto)
		{
			var client = httpClientFactory.CreateClient("CarRentalApi");
			var jsonData = JsonConvert.SerializeObject(updateServiceDto);
			var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PutAsync("Services", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index", "Service", new { area = "Admin" });
			}
			return View(updateServiceDto);
		}
	}
}
