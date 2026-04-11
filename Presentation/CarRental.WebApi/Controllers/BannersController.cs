using CarRental.Application.Common;
using CarRental.Application.Features.CQRS.Commands.BannerCommands;
using CarRental.Application.Features.CQRS.Handlers.BannerHandlers;
using CarRental.Application.Features.CQRS.Queries.BannerQueries;
using CarRental.Application.Features.CQRS.Results.BannerResults;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebApi.Controllers
{
	[Area("Admin")]
	[Route("api/[controller]")]
	[ApiController]
	public class BannersController(
		CreateBannerCommandHandler createBannerCommandHandler,
		GetBannerQueryHandler getBannerQueryHandler,
		GetBannerByIdQueryHandler getBannerByIdQueryHandler,
		UpdateBannerCommandHandler updateBannerCommandHandler,
		RemoveBannerCommandHandler removeBannerCommandHandler
		) : ControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> BannerList()
		{
			var values = await getBannerQueryHandler.Handle();
			return Ok(ApiResponse<List<GetBannerQueryResult>>.SuccessResponse(values, "Site afiş listesi başarıyla getirildi"));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> BannerById(int id)
		{
			var value = await getBannerByIdQueryHandler.Handle(new GetBannerByIdQuery(id));
			return Ok(ApiResponse<GetBannerByIdQueryResult>.SuccessResponse(value, "İlgili afiş bilgisi başarıyla getirildi"));
		}

		[HttpPost]
		public async Task<IActionResult> CreateBanner(CreateBannerCommand command)
		{
			var createdData = await createBannerCommandHandler.Handle(command);
			return StatusCode(201, ApiResponse<CreateBannerCommand>.SuccessResponse(createdData, "Yeni afiş başarıyla oluşturuldu"));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> RemoveBanner(int id)
		{
			var removedData = await removeBannerCommandHandler.Handle(new RemoveBannerCommand(id));
			return Ok(ApiResponse<RemoveBannerCommand>.SuccessResponse(removedData, "Afiş kaydı sistemden silindi"));
		}

		[HttpPut]
		public async Task<IActionResult> UpdateBanner(UpdateBannerCommand command)
		{
			var updatedData = await updateBannerCommandHandler.Handle(command);
			return Ok(ApiResponse<UpdateBannerCommand>.SuccessResponse(updatedData, "Afiş bilgisi başarıyla güncellendi"));
		}
	}
}
