using CarRental.Dto;
using CarRental.Dto.BrandDtos;
using CarRental.Dto.CarDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Versioning;
using System.Text;

namespace CarRental.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class BrandController(IHttpClientFactory httpClientFactory) : BaseAdminController
	{
		public async Task<IActionResult> Index()
		{
			var client = httpClientFactory.CreateClient("CarRentalApi");
			var responseMessage = await client.GetAsync("Brands");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var apiResponse = JsonConvert.DeserializeObject<ResultApiResponseDto<List<ResultBrandDto>>>(jsonData);
				if (apiResponse != null && apiResponse.Success)
				{
					return View(apiResponse.Data);
				}
			}
			return View(new List<ResultBrandDto>());
		}

		[HttpGet]
		public async Task<IActionResult> CreateBrand()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateBrand(CreateBrandDto createBrandDto)
		{
			var client = httpClientFactory.CreateClient("CarRentalApi");
			var jsonData = JsonConvert.SerializeObject(createBrandDto);
			var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PostAsync("Brands", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index", "Brand", new { area = "Admin" });
			}
			return View(createBrandDto);
		}

		public async Task<IActionResult> DeleteBrand(int id)
		{
			var client = httpClientFactory.CreateClient("CarRentalApi");
			var responseMessage = await client.DeleteAsync("Brands/" + id);

			return RedirectToAction("Index", "Brand", new { area = "Admin" });
		}

		[HttpGet]
		public async Task<IActionResult> UpdateBrand(int id)
		{
			var client = httpClientFactory.CreateClient("CarRentalApi");
			var responseMessage = await client.GetAsync("Brands/" + id);
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var apiResponse = JsonConvert.DeserializeObject<ResultApiResponseDto<UpdateBrandDto>>(jsonData);
				if (apiResponse != null && apiResponse.Success)
				{
					return View(apiResponse.Data);
				}
			}
			return RedirectToAction("Index", "Brand", new { area = "Admin" });
		}

		[HttpPost]
		public async Task<IActionResult> UpdateBrand(UpdateBrandDto updateBrandDto)
		{
			var client = httpClientFactory.CreateClient("CarRentalApi");
			var jsonData = JsonConvert.SerializeObject(updateBrandDto);
			var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PutAsync("Brands", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index", "Brand", new { area = "Admin" });
			}
			return View(updateBrandDto);
		}
	}
}
