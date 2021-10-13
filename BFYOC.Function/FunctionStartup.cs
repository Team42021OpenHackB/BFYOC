using BFYOC.Function.Extensions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(BFYOC.Function.Startup))]

namespace BFYOC.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();

            builder.Services.AddSingleton<CosmosDBRatingStore>();

            builder.Services.AddSingleton<ApiManager>();
        }
    }
}