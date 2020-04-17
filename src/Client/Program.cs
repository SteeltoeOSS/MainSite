using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Steeltoe.Client
{
	public class Program {
		public static async Task Main(string[] args) {
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("app");

			builder.Services.AddBaseAddressHttpClient();
			builder.Services.AddSingleton<INavMenu, NavMenu>();
			builder.Services.AddSingleton<ICalendarEvents, CalendarEvents>();

			await builder.Build().RunAsync();
		}
	}
}
