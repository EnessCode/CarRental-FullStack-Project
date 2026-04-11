using CarRental.Application.Enums;
using CarRental.Application.Features.Mediator.Commands.AppUserCommands;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Application.Features.Mediator.Handlers.AppUserHandlers
{
	public class CreateAppUserCommandHandler(IRepository<AppUser> repository) : IRequestHandler<CreateAppUserCommand>
	{
		public async Task Handle(CreateAppUserCommand request, CancellationToken cancellationToken)
		{
			await repository.CreateAsync(new AppUser
			{
				Username = request.Username,
				Password = request.Password,
				AppRoleId = (int)Roles.Member,
				Email = request.Email,
				Name = request.Name,
				Surname = request.Surname
			});
		}
	}
}
