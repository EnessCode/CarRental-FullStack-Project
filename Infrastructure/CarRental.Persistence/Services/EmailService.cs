using CarRental.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Infrastructure.Services
{
	public class EmailService(IConfiguration configuration) : IEmailService
	{
		public async Task SendReservationEmailAsync(string toEmail, string body)
		{
			var smtpServer = configuration["EmailSettings:SmtpServer"];
			var smtpPort = int.Parse(configuration["EmailSettings:SmtpPort"]);
			var senderEmail = configuration["EmailSettings:SenderEmail"];
			var senderPassword = configuration["EmailSettings:SenderPassword"];

			var client = new SmtpClient(smtpServer, smtpPort)
			{
				Credentials = new NetworkCredential(senderEmail, senderPassword),
				EnableSsl = true
			};

			var mailMessage = new MailMessage
			{
				From = new MailAddress(senderEmail, "CarRental Rezervasyon"),
				Subject = "Rezervasyon Onayı",
				Body = body,
				IsBodyHtml = true 
			};

			mailMessage.To.Add(toEmail);

			await client.SendMailAsync(mailMessage);
		}
	}
}