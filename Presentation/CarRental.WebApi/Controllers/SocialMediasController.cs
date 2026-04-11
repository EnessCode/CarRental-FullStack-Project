using CarRental.Application.Common;
using CarRental.Application.Features.Mediator.Commands.SocialMediaCommands;
using CarRental.Application.Features.Mediator.Queries.SocialMediaQueries;
using CarRental.Application.Features.Mediator.Results.SocialMediaResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebApi.Controllers
{
	[Area("Admin")]
	[Route("api/[controller]")]
	[ApiController]
	public class SocialMediasController(IMediator mediator) : ControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> SocialMediaList()
		{
			var values = await mediator.Send(new GetSocialMediaQuery());
			return Ok(ApiResponse<List<GetSocialMediaQueryResult>>.SuccessResponse(values, "Sosyal medya hesapları başarıyla getirildi"));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> SocialMediaById(int id)
		{
			var value = await mediator.Send(new GetSocialMediaByIdQuery(id));
			return Ok(ApiResponse<GetSocialMediaByIdQueryResult>.SuccessResponse(value, "İlgili sosyal medya hesabı başarıyla getirildi"));
		}

		[HttpPost]
		public async Task<IActionResult> CreateSocialMedia(CreateSocialMediaCommand command)
		{
			var createdData = await mediator.Send(command);
			return StatusCode(201, ApiResponse<CreateSocialMediaCommand>.SuccessResponse(createdData, "Yeni sosyal medya hesabı başarıyla oluşturuldu"));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> RemoveSocialMedia(int id)
		{
			var removedData = await mediator.Send(new RemoveSocialMediaCommand(id));
			return Ok(ApiResponse<RemoveSocialMediaCommand>.SuccessResponse(removedData, "Sosyal medya hesabı başarıyla silindi"));
		}

		[HttpPut]
		public async Task<IActionResult> UpdateSocialMedia(UpdateSocialMediaCommand command)
		{
			var updatedData = await mediator.Send(command);
			return Ok(ApiResponse<UpdateSocialMediaCommand>.SuccessResponse(updatedData, "Sosyal medya bilgisi başarıyla güncellendi"));
		}
	}
}