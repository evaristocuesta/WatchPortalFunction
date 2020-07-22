using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using WatchPortalFunction.Repository;

[assembly: FunctionsStartup(typeof(WatchPortalFunction.Startup))]

namespace WatchPortalFunction
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //builder.Services.AddHttpClient();

            builder.Services.AddSingleton<IWatchRepository>((s) =>
            {
                return new WatchRepository();
            });
        }
    }
}
