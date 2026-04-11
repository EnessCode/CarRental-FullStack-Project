using CarRental.Dto;
using CarRental.Dto.StatisticsDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarRental.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class StatisticsController(IHttpClientFactory httpClientFactory) : BaseAdminController
	{
		public async Task<IActionResult> Index()
		{
			var client = httpClientFactory.CreateClient("CarRentalApi");

			var responseMessage1 = await client.GetAsync("Statistics/GetCarCount");
			if (responseMessage1.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage1.Content.ReadAsStringAsync();
				var apiResponse = JsonConvert.DeserializeObject<ResultApiResponseDto<ResultStatisticsDto>>(jsonData);
				ViewBag.carCount = apiResponse.Data.carCount;
			}

			var responseMessage2 = await client.GetAsync("Statistics/GetLocationCount");
			if (responseMessage2.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage2.Content.ReadAsStringAsync();
				var apiResponse = JsonConvert.DeserializeObject<ResultApiResponseDto<ResultStatisticsDto>>(jsonData);
				ViewBag.locationCount = apiResponse.Data.locationCount;
			}

			var responseMessage3 = await client.GetAsync("Statistics/GetAuthorCount");
			if (responseMessage3.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage3.Content.ReadAsStringAsync();
				var apiResponse = JsonConvert.DeserializeObject<ResultApiResponseDto<ResultStatisticsDto>>(jsonData);
				ViewBag.authorCount = apiResponse.Data.authorCount;
			}

			var responseMessage4 = await client.GetAsync("Statistics/GetBlogCount");
			if (responseMessage4.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage4.Content.ReadAsStringAsync();
				var apiResponse = JsonConvert.DeserializeObject<ResultApiResponseDto<ResultStatisticsDto>>(jsonData);
				ViewBag.blogCount = apiResponse.Data.blogCount;
			}

			var responseMessage5 = await client.GetAsync("Statistics/GetBrandCount");
			if (responseMessage5.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage5.Content.ReadAsStringAsync();
				var apiResponse = JsonConvert.DeserializeObject<ResultApiResponseDto<ResultStatisticsDto>>(jsonData);
				ViewBag.brandCount = apiResponse.Data.brandCount;
			}

			var responseMessage6 = await client.GetAsync("Statistics/GetAvgRentPriceForDaily");
			if (responseMessage6.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage6.Content.ReadAsStringAsync();
				var apiResponse = JsonConvert.DeserializeObject<ResultApiResponseDto<ResultStatisticsDto>>(jsonData);
				ViewBag.avgPriceDaily = apiResponse.Data.avgRentPriceForDaily.ToString("0.00");
			}

			var responseMessage7 = await client.GetAsync("Statistics/GetAvgRentPriceForWeekly");
			if (responseMessage7.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage7.Content.ReadAsStringAsync();
				var apiResponse = JsonConvert.DeserializeObject<ResultApiResponseDto<ResultStatisticsDto>>(jsonData);
				ViewBag.avgPriceWeekly = apiResponse.Data.avgRentPriceForWeekly.ToString("0.00");
			}

			var responseMessage8 = await client.GetAsync("Statistics/GetAvgRentPriceForMonthly");
			if (responseMessage8.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage8.Content.ReadAsStringAsync();
				var apiResponse = JsonConvert.DeserializeObject<ResultApiResponseDto<ResultStatisticsDto>>(jsonData);
				ViewBag.avgPriceMonthly = apiResponse.Data.avgRentPriceForMonthly.ToString("0.00");
			}

			var responseMessage9 = await client.GetAsync("Statistics/GetAutomaticCarCount");
			if (responseMessage9.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage9.Content.ReadAsStringAsync();
				var apiResponse = JsonConvert.DeserializeObject<ResultApiResponseDto<ResultStatisticsDto>>(jsonData);
				ViewBag.automaticCarCount = apiResponse.Data.automaticCarCount;
			}

			var responseMessage10 = await client.GetAsync("Statistics/GetBrandNameByMaxCarCount");
			if (responseMessage10.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage10.Content.ReadAsStringAsync();
				var apiResponse = JsonConvert.DeserializeObject<ResultApiResponseDto<ResultStatisticsDto>>(jsonData);
				ViewBag.maxBrandName = apiResponse.Data.brandNameByMaxCarCount;
			}

			var responseMessage11 = await client.GetAsync("Statistics/GetCarBrandAndModelByMaxRentPrice");
			if (responseMessage11.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage11.Content.ReadAsStringAsync();
				var apiResponse = JsonConvert.DeserializeObject<ResultApiResponseDto<ResultStatisticsDto>>(jsonData);
				ViewBag.maxPriceCar = apiResponse.Data.carBrandAndModelByMaxRentPrice;
			}

			var responseMessage12 = await client.GetAsync("Statistics/GetCarBrandAndModelByMinRentPrice");
			if (responseMessage12.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage12.Content.ReadAsStringAsync();
				var apiResponse = JsonConvert.DeserializeObject<ResultApiResponseDto<ResultStatisticsDto>>(jsonData);
				ViewBag.minPriceCar = apiResponse.Data.carBrandAndModelByMinRentPrice;
			}

			var responseMessage13 = await client.GetAsync("Statistics/GetCarCountByFuelGasoline");
			if (responseMessage13.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage13.Content.ReadAsStringAsync();
				var apiResponse = JsonConvert.DeserializeObject<ResultApiResponseDto<ResultStatisticsDto>>(jsonData);
				ViewBag.gasolineCount = apiResponse.Data.carCountByFuelGasoline;
			}

			var responseMessage14 = await client.GetAsync("Statistics/GetCarCountByFuelDiesel");
			if (responseMessage14.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage14.Content.ReadAsStringAsync();
				var apiResponse = JsonConvert.DeserializeObject<ResultApiResponseDto<ResultStatisticsDto>>(jsonData);
				ViewBag.dieselCount = apiResponse.Data.carCountByFuelDiesel;
			}

			var responseMessage15 = await client.GetAsync("Statistics/GetCarCountByFuelElectric");
			if (responseMessage15.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage15.Content.ReadAsStringAsync();
				var apiResponse = JsonConvert.DeserializeObject<ResultApiResponseDto<ResultStatisticsDto>>(jsonData);
				ViewBag.electricCount = apiResponse.Data.carCountByFuelElectric;
			}

			var responseMessage16 = await client.GetAsync("Statistics/GetManualCarCount");
			if (responseMessage16.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage16.Content.ReadAsStringAsync();
				var apiResponse = JsonConvert.DeserializeObject<ResultApiResponseDto<ResultStatisticsDto>>(jsonData);
				ViewBag.manualCarCount = apiResponse.Data.manualCarCount;
			}
			return View();
		}
	}
}