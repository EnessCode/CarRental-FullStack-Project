using CarRental.Application.Common;
using CarRental.Application.Features.CQRS.Commands.ContactCommands;
using CarRental.Application.Features.CQRS.Handlers.ContactHandlers;
using CarRental.Application.Features.CQRS.Queries.ContactQueries;
using CarRental.Application.Features.CQRS.Results.ContactResults;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.WebApi.Controllers
{
	[Area("Admin")]
	[Route("api/[controller]")]
	[ApiController]
	public class ContactsController(
		CreateContactCommandHandler createContactCommandHandler,
		GetContactQueryHandler getContactQueryHandler,
		GetContactByIdQueryHandler getContactByIdHandler,
		UpdateContactCommandHandler updateContactCommandHandler,
		RemoveContactCommandHandler removeContactCommandHandler,
		MarkContactAsReadCommandHandler markContactAsReadCommandHandler
		) : ControllerBase
	{

		[HttpGet]
		public async Task<IActionResult> ContactList()
		{
			var values = await getContactQueryHandler.Handle();
			return Ok(ApiResponse<List<GetContactQueryResult>>.SuccessResponse(values, "Gelen mesajlar ve iletişim listesi başarıyla getirildi"));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> ContactById(int id)
		{
			var value = await getContactByIdHandler.Handle(new GetContactByIdQuery(id));
			return Ok(ApiResponse<GetContactByIdQueryResult>.SuccessResponse(value, "İlgili iletişim mesajı başarıyla getirildi"));
		}

		[HttpPost]
		public async Task<IActionResult> CreateContact(CreateContactCommand command)
		{
			var createdData = await createContactCommandHandler.Handle(command);
			return StatusCode(201, ApiResponse<CreateContactCommand>.SuccessResponse(createdData, "Yeni iletişim mesajı başarıyla oluşturuldu"));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> RemoveContact(int id)
		{
			var removedData = await removeContactCommandHandler.Handle(new RemoveContactCommand(id));
			return Ok(ApiResponse<RemoveContactCommand>.SuccessResponse(removedData, "İlgili iletişim mesajı sistemden silindi"));
		}

		[HttpPut]
		public async Task<IActionResult> UpdateContact(UpdateContactCommand command)
		{
			var updatedData = await updateContactCommandHandler.Handle(command);
			return Ok(ApiResponse<UpdateContactCommand>.SuccessResponse(updatedData, "İletişim bilgisi başarıyla güncellendi"));
		}

		[HttpGet("MarkContactAsRead/{id}")] 
		public async Task<IActionResult> MarkContactAsRead(int id)
		{
			await markContactAsReadCommandHandler.Handle(id);
			return Ok(ApiResponse<string>.SuccessResponse(null, "Mesaj okundu olarak işaretlendi."));
		}
	}
}
