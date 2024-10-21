using GrpcSDK;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace grpc_test
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddGrpcSdk();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.MapGet("/grpc", async ([FromServices] IGreeterGrpcService grpcService, CancellationToken cancellation) =>
			{
				var result = await grpcService.SayHelloAsync("hello grpc", cancellation);
				Console.WriteLine(result);
			});

			app.UseAuthorization();

			app.MapControllers();

			await app.RunAsync();
		}
	}
}