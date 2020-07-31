## Getting to know Steeltoe


### Goal

Understand how Steeltoe is distributed (Nuget) and how one adds components into an existing application.

### Expected Results

Begin building an API that will be enhanced with more components in the next exercise(s).


### Get Started

1. Create a new WebAPI project in Visual Studio: File > New > Project

	```bash
	dotnet new webapi
	```

1. Bring in the dependency for `Steeltoe.Management.Endpointcore`
1. Bring in the dependency for `Steeltoe.Extensions.Logging.DynamicLogger`
1. Bring in the dependency for `Steeltoe.Management.TracingCore`

1. Add the health, info, and logger actuators to the host builder

	```c#
	public static IHostBuilder CreateHostBuilder(string[] args) =>
		Host.CreateDefaultBuilder(args)
			.ConfigureWebHostDefaults(webBuilder => {
				webBuilder.UseStartup<Startup>();
			})
			.AddHealthActuator()
			.AddInfoActuator()
			.AddLoggersActuator();
	```

1. Add the distributed tracing service in the application

	```c#
	public void ConfigureServices(IServiceCollection services) {
		services.AddDistributedTracing(Configuration);
		services.AddControllers();
	}
	```

1. Write a log message in the default controller

	```c#
	[HttpGet]
	public IEnumerable<WeatherForecast> Get() {
		var rng = new Random();
		_logger.LogInformation("Hi there");
		return Enumerable.Range(1, 5).Select(index => new WeatherForecast {
				Date = DateTime.Now.AddDays(index),
			TemperatureC = rng.Next(-20, 55),
			Summary = Summaries[rng.Next(Summaries.Length)]
		})
		.ToArray();
	}
	```

1. Run the application: `F5`

	```bash
	dotnet run
	```

1. The default weather forecast endpoint will show

1. The health endpoint is reporting a status of “UP”

1. The info endpoint is reporting vital information about the app

1. With the application still running go back to Visual Studio, find the **Output** window, and choose to show output from **WebApplicationX - ASP.NET Core Web Server**. The log messages will be output and their tracing contexts will be automatically appended.    [app name, trace id, span id, trace flags]

<div class="table-responsive">
<table class="table table-striped">
<tbody>
<tr><td class="text-center"><a href="/labs/SpringOne"><button class="btn btn-dark">Back to Introduction</button></a>
</td><td class="text-center"><a href="/labs/SpringOne-Ex2"><button class="btn btn-dark">Next Exercise</button></a>
</td></tr></tbody></table></div>