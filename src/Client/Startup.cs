using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Steeltoe.Client
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddScoped<INavMenu, NavMenu>();
      services.AddScoped<ICalendarEvents, CalendarEvents>();
    }

    public void Configure(IComponentsApplicationBuilder app)
    {
      app.AddComponent<App>("app");
    } 
  }
}
