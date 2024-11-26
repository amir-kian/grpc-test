using Microsoft.AspNetCore.Mvc;
using GrpcSDK;

namespace grpc_test.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly ILogger<WeatherForecastController> _logger;
		private readonly IGreatGrpcClientService _greatGrpcClientService;

		public WeatherForecastController(ILogger<WeatherForecastController> logger, IGreatGrpcClientService greatGrpcClientService)
		{
			_logger = logger;
            _greatGrpcClientService= greatGrpcClientService;

        }

		[HttpGet(Name = "GetWeatherForecast")]
		public   IEnumerable<WeatherForecast> Get()
		{

            return  Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
				TemperatureC = Random.Shared.Next(-20, 55),
				Summary = Summaries[Random.Shared.Next(Summaries.Length)]
			})
			.ToArray();
		}


        [HttpPost(Name = "GetGrpcTest")]
        public async Task<string> GrpcTest()
        {
            var name = await _greatGrpcClientService.SayHelloAsync("soheila tanha eshghe amirhosein");
			return name;
        }
    }
}
