using CarRental.Dto;
using CarRental.Dto.BlogDtos;
using CarRental.Dto.CommentDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarRental.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CommentController(IHttpClientFactory httpClientFactory) : BaseAdminController
	{
		public async Task<IActionResult> BlogCommentList(int id)
		{
			ViewBag.BlogId = id;

			var client = httpClientFactory.CreateClient("CarRentalApi");
			var responseMessage = await client.GetAsync("Comments/CommentListByBlog/" + id);
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var apiResponse = JsonConvert.DeserializeObject<ResultApiResponseDto<List<ResultCommentDto>>>(jsonData);
				if (apiResponse != null && apiResponse.Success && apiResponse.Data != null)
				{
					if (apiResponse.Data.Count > 0)
					{
						ViewBag.BlogTitle = apiResponse.Data.FirstOrDefault()?.BlogTitle;
					}
					else
					{
						ViewBag.BlogTitle = "Henüz Yorum Yapılmamış Blog";
					}
					return View(apiResponse.Data);
				}
			}
			return View(new List<ResultCommentDto>());
		}

		public async Task<IActionResult> DeleteComment(int id, int blogId)
		{
			var client = httpClientFactory.CreateClient("CarRentalApi");
			var responseMessage = await client.DeleteAsync("Comments/" + id);

			return RedirectToAction("BlogCommentList", new { area = "Admin", id = blogId });
		}
	}
}
