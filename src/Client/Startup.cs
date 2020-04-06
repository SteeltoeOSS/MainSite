using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Steeltoe.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
          services.AddSingleton<Session>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            app.Services.GetService<Session>().LoadTOCAsync();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
    } 
    }
}
