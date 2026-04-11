using CarRental.Application.Common;
using CarRental.Application.Features.Mediator.Commands.TagCloudCommands;
using CarRental.Application.Features.Mediator.Queries.TagCloudQueries;
using CarRental.Application.Features.Mediator.Results.TagCloudResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebApi.Controllers
{
	[Area("Admin")]
	[Route("api/[controller]")]
	[ApiController]
	public class TagCloudsController(IMediator mediator) : ControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> TagCloudList()
		{
			var values = await mediator.Send(new GetTagCloudQuery());
			return Ok(ApiResponse<List<GetTagCloudQueryResult>>.SuccessResponse(values, "Etiket bulutu listesi başarıyla getirildi"));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> TagCloudById(int id)
		{
			var value = await mediator.Send(new GetTagCloudByIdQuery(id));
			return Ok(ApiResponse<GetTagCloudByIdQueryResult>.SuccessResponse(value, "İlgili etiket bulutu kaydı başarıyla getirildi"));
		}

		[HttpPost]
		public async Task<IActionResult> CreateTagCloud(CreateTagCloudCommand command)
		{
			var createdData = await mediator.Send(command);
			return StatusCode(201, ApiResponse<CreateTagCloudCommand>.SuccessResponse(createdData, "Yeni etiket bulutu kaydı başarıyla oluşturuldu"));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> RemoveTagCloud(int id)
		{
			var removedData = await mediator.Send(new RemoveTagCloudCommand(id));
			return Ok(ApiResponse<RemoveTagCloudCommand>.SuccessResponse(removedData, "Etiket bulutu kaydı başarıyla silindi"));
		}

		[HttpPut]
		public async Task<IActionResult> UpdateTagCloud(UpdateTagCloudCommand command)
		{
			var updatedData = await mediator.Send(command);
			return Ok(ApiResponse<UpdateTagCloudCommand>.SuccessResponse(updatedData, "Etiket bulutu bilgisi başarıyla güncellendi"));
		}

		[HttpGet("GetTagCloudByBlogId/{id}")]
		public async Task<IActionResult> GetTagCloudByBlogId(int id)
		{
			var values = await mediator.Send(new GetTagCloudByBlogIdQuery(id));
			return Ok(ApiResponse<List<GetTagCloudByBlogIdQueryResult>>.SuccessResponse(values, "Etiketler getirildi"));
		}
	}
}