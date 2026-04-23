using CarRental.Application.Features.Mediator.Commands.ReservationCommands;
using CarRental.Application.Interfaces;
using CarRental.Application.Interfaces.CarPricingInterfaces;
using CarRental.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Application.Features.Mediator.Handlers.ReservationHandlers
{
	public class CreateReservationCommandHandler(
		IRepository<Reservation> repository,
		ICarPricingRepository carPricingRepository,
		IRepository<Car> carRepository,
		IEmailService emailService
	) : IRequestHandler<CreateReservationCommand>
	{
		public async Task Handle(CreateReservationCommand request, CancellationToken cancellationToken)
		{
			int totalDays = request.DropOffDate.DayNumber - request.PickUpDate.DayNumber;
			if (totalDays <= 0) totalDays = 1;

			var carPricings = await carPricingRepository.GetCarPricingsByCarId(request.CarId);
			var car = await carRepository.GetByIdAsync(request.CarId);

			var dailyPrice = carPricings.FirstOrDefault(x => x.PricingId == 1)?.Amount ?? 0;
			decimal calculatedTotalPrice = totalDays * dailyPrice;

			await repository.CreateAsync(new Reservation
			{
				Name = request.Name,
				Surname = request.Surname,
				Email = request.Email,
				Phone = request.Phone,
				CarId = request.CarId,
				AppUserId = request.AppUserId,
				PickUpLocationId = request.PickUpLocationId,
				DropOffLocationId = request.DropOffLocationId,
				Age = request.Age,
				DriverLicenseYear = request.DriverLicenseYear,
				Description = request.Description,
				PickUpDate = request.PickUpDate,
				DropOffDate = request.DropOffDate,
				PickUpTime = request.PickUpTime,
				DropOffTime = request.DropOffTime,
				Status = request.Status,
				TotalPrice = calculatedTotalPrice
			});

			string mailBody = CreateEmailBody(request, car, calculatedTotalPrice);
			await emailService.SendReservationEmailAsync(request.Email, mailBody);
		}

		private string CreateEmailBody(CreateReservationCommand request, Car car, decimal totalPrice)
		{
			return $@"
            <div style='font-family: Arial, sans-serif; max-width: 600px; margin: auto; border: 1px solid #ddd; border-radius: 15px; overflow: hidden;'>
                <div style='background-color: #0d6efd; color: white; padding: 20px; text-align: center;'>
                    <h2>Rezervasyonunuz Onaylandı!</h2>
                </div>
                <div style='padding: 20px;'>
                    <img src='{car.CoverImageUrl}' style='width: 100%; border-radius: 10px; margin-bottom: 20px;' alt='Araç Görseli'>
                    <h4>Sayın {request.Name} {request.Surname},</h4>
                    <p>Bizi tercih ettiğiniz için teşekkür ederiz. Rezervasyon detaylarınız aşağıdadır:</p>
                    <hr border='0' style='border-top: 1px solid #eee;'>
                    <table style='width: 100%; border-collapse: collapse;'>
                        <tr><td style='padding: 8px 0;'><strong>Araç:</strong></td><td style='text-align: right;'>{car.Model}</td></tr>
                        <tr><td style='padding: 8px 0;'><strong>Alış:</strong></td><td style='text-align: right;'>{request.PickUpDate:dd.MM.yyyy} - {request.PickUpTime}</td></tr>
                        <tr><td style='padding: 8px 0;'><strong>Teslim:</strong></td><td style='text-align: right;'>{request.DropOffDate:dd.MM.yyyy} - {request.DropOffTime}</td></tr>
                    </table>
                    <div style='background-color: #f8f9fa; padding: 15px; text-align: center; margin-top: 20px; border-radius: 10px;'>
                        <span style='font-size: 14px; color: #666;'>Toplam Ödenecek Tutar</span><br>
                        <strong style='font-size: 24px; color: #198754;'>{totalPrice:C2}</strong>
                    </div>
                </div>
                <div style='background-color: #f1f1f1; color: #888; padding: 15px; text-align: center; font-size: 12px;'>
                    Bu bir sistem mesajıdır, lütfen yanıtlamayınız. Keyifli yolculuklar dileriz!
                </div>
            </div>";
		}
	}
}