using GrpcService;
using Microsoft.Extensions.DependencyInjection;

namespace GrpcSDK;

public static class ServiceCollectionExtension
{
	public static void AddGrpcSdk(this IServiceCollection services) {
		services.AddGrpcClient<Greeter.GreeterClient>(grpcClient =>
		{
			grpcClient.Address = new Uri("https://localhost:7055");
		});
		services.AddScoped<IGreeterGrpcService,GreeterGrpcService>();
			}


}
