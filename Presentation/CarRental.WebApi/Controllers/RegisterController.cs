using CarRental.Application.Common;
using CarRental.Application.Features.Mediator.Commands.AppUserCommands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RegisterController(IMediator mediator) : ControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> Index(CreateAppUserCommand command)
		{
			await mediator.Send(command);
			return StatusCode(201, ApiResponse<CreateAppUserCommand>.SuccessResponse(command, "Kullanıcı kaydı başarıyla gerçekleştirildi."));
		}
	}
}