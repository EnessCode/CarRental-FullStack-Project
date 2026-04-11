using CarRental.Application.Common;
using CarRental.Application.Features.Mediator.Commands.AuthorCommands;
using CarRental.Application.Features.Mediator.Queries.AuthorQueries;
using CarRental.Application.Features.Mediator.Results.AuthorResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebApi.Controllers
{
	[Area("Admin")]
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorsController(IMediator mediator) : ControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> AuthorList()
		{
			var values = await mediator.Send(new GetAuthorQuery());
			return Ok(ApiResponse<List<GetAuthorQueryResult>>.SuccessResponse(values, "Yazar listesi başarıyla getirildi"));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> AuthorById(int id)
		{
			var value = await mediator.Send(new GetAuthorByIdQuery(id));
			return Ok(ApiResponse<GetAuthorByIdQueryResult>.SuccessResponse(value, "İlgili yazar kaydı başarıyla getirildi"));
		}

		[HttpPost]
		public async Task<IActionResult> CreateAuthor(CreateAuthorCommand command)
		{
			var createdData = await mediator.Send(command);
			return StatusCode(201, ApiResponse<CreateAuthorCommand>.SuccessResponse(createdData, "Yeni yazar kaydı başarıyla oluşturuldu"));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> RemoveAuthor(int id)
		{
			var removedData = await mediator.Send(new RemoveAuthorCommand(id));
			return Ok(ApiResponse<RemoveAuthorCommand>.SuccessResponse(removedData, "Yazar kaydı başarıyla silindi"));
		}

		[HttpPut]
		public async Task<IActionResult> UpdateAuthor(UpdateAuthorCommand command)
		{
			var updatedData = await mediator.Send(command);
			return Ok(ApiResponse<UpdateAuthorCommand>.SuccessResponse(updatedData, "Yazar bilgisi başarıyla güncellendi"));
		}
	}
}