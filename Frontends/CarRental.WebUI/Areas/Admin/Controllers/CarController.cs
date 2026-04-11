using CarRental.Dto;
using CarRental.Dto.BrandDtos;
using CarRental.Dto.CarDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace CarRental.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CarController(IHttpClientFactory httpClientFactory) : BaseAdminController
	{
		public async Task<IActionResult> Index()
		{
			var client = httpClientFactory.CreateClient("CarRentalApi");
			var responseMessage = await client.GetAsync("Cars/GetCarWithBrand");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var apiResponse = JsonConvert.DeserializeObject<ResultApiResponseDto<List<ResultCarWithBrandDto>>>(jsonData);
				if (apiResponse != null && apiResponse.Success)
				{
					return View(apiResponse.Data);
				}
			}
			return View(new List<ResultCarWithBrandDto>());
		}

		[HttpGet]
		public async Task<IActionResult> CreateCar()
		{
			await LoadBrandsToViewBag();
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateCar(CreateCarDto createCarDto)
		{
			var client = httpClientFactory.CreateClient("CarRentalApi");
			var jsonData = JsonConvert.SerializeObject(createCarDto);
			var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PostAsync("Cars", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index", "Car", new { area = "Admin" });
			}
			await LoadBrandsToViewBag();
			return View(createCarDto);
		}

		public async Task<IActionResult> DeleteCar(int id)
		{
			var client = httpClientFactory.CreateClient("CarRentalApi");
			var responseMessage = await client.DeleteAsync("Cars/" + id);

			return RedirectToAction("Index", "Car", new { area = "Admin" });
		}

		[HttpGet]
		public async Task<IActionResult> UpdateCar(int id)
		{
			await LoadBrandsToViewBag();
			var client = httpClientFactory.CreateClient("CarRentalApi");
			var responseMessage = await client.GetAsync("Cars/" + id);
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var apiResponse = JsonConvert.DeserializeObject<ResultApiResponseDto<UpdateCarDto>>(jsonData);
				if (apiResponse != null && apiResponse.Success)
				{
					return View(apiResponse.Data);
				}
			}
			return RedirectToAction("Index", "Car", new { area = "Admin" });
		}

		[HttpPost]
		public async Task<IActionResult> UpdateCar(UpdateCarDto updateCarDto)
		{
			var client = httpClientFactory.CreateClient("CarRentalApi");
			var jsonData = JsonConvert.SerializeObject(updateCarDto);
			var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PutAsync("Cars", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index", "Car", new { area = "Admin" });
			}
			await LoadBrandsToViewBag();
			return View(updateCarDto);
		}

		private async Task LoadBrandsToViewBag()
		{
			var client = httpClientFactory.CreateClient("CarRentalApi");
			var responseMessage = await client.GetAsync("Brands");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var apiResponse = JsonConvert.DeserializeObject<ResultApiResponseDto<List<ResultBrandDto>>>(jsonData);
				if (apiResponse?.Data != null)
				{
					ViewBag.BrandList = apiResponse.Data.Select(x => new SelectListItem
					{
						Text = x.Name,
						Value = x.Id.ToString()
					}).ToList();
				}
			}
		}
	}
}
