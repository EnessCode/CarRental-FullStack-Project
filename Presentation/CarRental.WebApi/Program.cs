using CarRental.Application.Common;
using CarRental.Application.Features.CQRS.Handlers.AboutHandlers;
using CarRental.Application.Interfaces;
using CarRental.Application.Interfaces.AppUserInterfaces;
using CarRental.Application.Interfaces.BlogInterfaces;
using CarRental.Application.Interfaces.CarDescriptionInterfaces;
using CarRental.Application.Interfaces.CarFeatureInterfaces;
using CarRental.Application.Interfaces.CarInterfaces;
using CarRental.Application.Interfaces.CarPricingInterfaces;
using CarRental.Application.Interfaces.CategoryInterfaces;
using CarRental.Application.Interfaces.CommentInterfaces;
using CarRental.Application.Interfaces.RentACarInterfaces;
using CarRental.Application.Interfaces.RentACarProcessInterfaces;
using CarRental.Application.Interfaces.ReservationInterfaces;
using CarRental.Application.Interfaces.StatisticsInterfaces;
using CarRental.Application.Interfaces.TagCloudInterfaces;
using CarRental.Application.Services;
using CarRental.Application.Tools;
using CarRental.Application.Validators.AppUserValidators;
using CarRental.Infrastructure.Services;
using CarRental.Persistence.Context;
using CarRental.Persistence.Repositories;
using CarRental.Persistence.Repositories.AppUserRepositories;
using CarRental.Persistence.Repositories.BlogRepositories;
using CarRental.Persistence.Repositories.CarDescriptionRepositories;
using CarRental.Persistence.Repositories.CarFeatureRepositories;
using CarRental.Persistence.Repositories.CarPricingRepositories;
using CarRental.Persistence.Repositories.CarRepositories;
using CarRental.Persistence.Repositories.CategoryRepositories;
using CarRental.Persistence.Repositories.CommentRepositories;
using CarRental.Persistence.Repositories.RentACarProcessRepositories;
using CarRental.Persistence.Repositories.RentACarRepositories;
using CarRental.Persistence.Repositories.ReservationRepositories;
using CarRental.Persistence.Repositories.StatisticsRepositories;
using CarRental.Persistence.Repositories.TagCloudRepositories;
using CarRental.WebApi.Middlewares;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
	opt.RequireHttpsMetadata = false;
	opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = builder.Configuration[JwtTokenDefaults.ValidIssuer],
		ValidAudience = builder.Configuration[JwtTokenDefaults.ValidAudience],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration[JwtTokenDefaults.SecretKey])),
	};
});

builder.Services.AddDbContext<CarRentalContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}); 
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<ICarPricingRepository, CarPricingRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ITagCloudRepository, TagCloudRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IStatisticsRepository, StatisticsRepository>();
builder.Services.AddScoped<IRentACarRepository, RentACarRepository>();
builder.Services.AddScoped<ICarFeatureRepository, CarFeatureRepository>();
builder.Services.AddScoped<ICarDescriptionRepository, CarDescriptionRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IRentACarProcessRepository, RentACarProcessRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();
builder.Services.AddScoped<JwtTokenGenerator>();

builder.Services.AddApplicationService(builder.Configuration);

builder.Services.Scan(scan => scan
	.FromAssemblyOf<CreateAboutCommandHandler>()
	.AddClasses(classes => classes.Where(c => c.Name.EndsWith("Handler")))
	.AsSelf()
	.WithScopedLifetime());

builder.Services.AddFluentValidationAutoValidation(); 
builder.Services.AddFluentValidationClientsideAdapters(); 
builder.Services.AddValidatorsFromAssemblyContaining<CreateAppUserValidator>();

builder.Services.AddControllers()
	.ConfigureApiBehaviorOptions(options =>
	{
		options.InvalidModelStateResponseFactory = context =>
		{
			var errors = context.ModelState.Values
				.SelectMany(x => x.Errors)
				.Select(x => x.ErrorMessage)
				.FirstOrDefault(); 

			return new BadRequestObjectResult(ApiResponse<NoContent>.FailResponse(errors));
		};
	}); builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
