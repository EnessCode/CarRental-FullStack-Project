using CarRental.Application.Common;
using CarRental.Application.Features.Mediator.Commands.TestimonialCommands;
using CarRental.Application.Features.Mediator.Queries.TestimonialQueries;
using CarRental.Application.Features.Mediator.Results.TestimonialResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebApi.Controllers
{
	[Area("Admin")]
	[Route("api/[controller]")]
	[ApiController]
	public class TestimonialsController(IMediator mediator) : ControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> TestimonialList()
		{
			var values = await mediator.Send(new GetTestimonialQuery());
			return Ok(ApiResponse<List<GetTestimonialQueryResult>>.SuccessResponse(values, "Referanslar başarıyla getirildi"));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> TestimonialById(int id)
		{
			var value = await mediator.Send(new GetTestimonialByIdQuery(id));
			return Ok(ApiResponse<GetTestimonialByIdQueryResult>.SuccessResponse(value, "İlgili referans başarıyla getirildi"));
		}

		[HttpPost]
		public async Task<IActionResult> CreateTestimonial(CreateTestimonialCommand command)
		{
			var createdData = await mediator.Send(command);
			return StatusCode(201, ApiResponse<CreateTestimonialCommand>.SuccessResponse(createdData, "Yeni referans başarıyla oluşturuldu"));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> RemoveTestimonial(int id)
		{
			var removedData = await mediator.Send(new RemoveTestimonialCommand(id));
			return Ok(ApiResponse<RemoveTestimonialCommand>.SuccessResponse(removedData, "Referans başarıyla silindi"));
		}

		[HttpPut]
		public async Task<IActionResult> UpdateTestimonial(UpdateTestimonialCommand command)
		{
			var updatedData = await mediator.Send(command);
			return Ok(ApiResponse<UpdateTestimonialCommand>.SuccessResponse(updatedData, "Referans bilgisi başarıyla güncellendi"));
		}
	}
}