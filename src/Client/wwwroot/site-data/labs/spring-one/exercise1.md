[vs-new-proj]: /site-data/labs/spring-one/images/vs-new-proj.png "New visual studio web project"
[vs-name-proj]: /site-data/labs/spring-one/images/vs-configure-project.png "Name project"
[vs-create-proj]: /site-data/labs/spring-one/images/vs-create-project.png "Create an api project"
[vs-add-endpointcore]: /site-data/labs/spring-one/images/vs-add-endpointcore.png "Endpointcode nuget dependency"
[vs-add-dynamiclogger]: /site-data/labs/spring-one/images/vs-add-dynamiclogger.png "Dynamiclogger nuget dependency"
[vs-add-tracingcore]: /site-data/labs/spring-one/images/vs-add-tracingcore.png "TracingCode nuget dependency"
[vs-run-application]: /site-data/labs/spring-one/images/vs-run-application.png "Run the project"
[run-weatherforecast]: /site-data/labs/spring-one/images/weatherforecast-endpoint.png "Weatherforecast endpoint"
[health-endpoint]: /site-data/labs/spring-one/images/health-endpoint.png "Health endpoint"
[info-endpoint]: /site-data/labs/spring-one/images/info-endpoint.png "Info endpoint"
[trace-log]: /site-data/labs/spring-one/images/trace-log.png "Trace logs"

[home-page-link]: /labs/spring-one
[exercise-1-link]: /labs/spring-one/exercise1
[exercise-2-link]: /labs/spring-one/exercise2
[exercise-3-link]: /labs/spring-one/exercise3
[exercise-4-link]: /labs/spring-one/exercise4
[exercise-5-link]: /labs/spring-one/exercise5

## Getting to know Steeltoe

### Goal

Understand how Steeltoe is distributed (Nuget) and how one adds components into an existing application.

### Expected Results

Begin building an API that will be enhanced with more components in the next exercise(s).

### Get Started

1. Create a new WebAPI project

	|![vs-new-proj] |![vs-name-proj]|![vs-create-proj]|
	|--|
	```powershell
	dotnet new webapi
	```
	<br/><br/>

1. Bring in the dependency for `Steeltoe.Management.Endpointcore`
	
	![vs-add-endpointcore]

	```powershell
	dotnet add package Steeltoe.Management.Endpointcore
	```
	<br/><br/>

1. Bring in the dependency for `Steeltoe.Extensions.Logging.DynamicLogger`

	![vs-add-dynamiclogger]

	```powershell
	dotnet add package Steeltoe.Extensions.Logging.DynamicLogger
	```
	<br/><br/>

1. Bring in the dependency for `Steeltoe.Management.TracingCore`

	![vs-add-tracingcore]

	```powershell
	dotnet add package Steeltoe.Extensions.Management.TracingCore
	```
	<br/><br/>

1. Add the health, info, and logger actuators to the host builder `Program.cs`
	```csharp
	using Steeltoe.Management.Endpoint;
	...

	public static IHostBuilder CreateHostBuilder(string[] args) =>
		Host.CreateDefaultBuilder(args)
			.ConfigureWebHostDefaults(webBuilder => {
				webBuilder.UseStartup<Startup>();
			})
			.AddHealthActuator()
			.AddInfoActuator()
			.AddLoggersActuator()
			;
	```
	<br/><br/>

1. Add the distributed tracing service in the application `Startup.cs`
	```csharp
	using Steeltoe.Management.Tracing;
	...

	public void ConfigureServices(IServiceCollection services) {
		...
		services.AddDistributedTracing(Configuration);
		...
	}
	```
	<br/><br/>

1. Write a log message in the default controller `Controllers\WeatherForecastController.cs`
	```csharp
	[HttpGet]
	public IEnumerable<WeatherForecast> Get() {
		...
		_logger.LogInformation("Hi there");
		
		return ...
	}
	```
	<br/><br/>

1. Run the application `Start Debugging`

	![vs-run-application]
	```powershell
	dotnet run
	```
	<br/><br/>

1. The default weather forecast endpoint will show in your browser `http://localhost/weatherforecast`

	![run-weatherforecast]
	<br/><br/>

1. Navigate to the automatically generated health endpoint and note a reporting a status of "UP" `https://localhost/actuators/health`

	![health-endpoint]
	<br/><br/>

1. Navigate to the automatically generated info endpoint and note the vitals it provides `https://localhost/actuators/info`

	![info-endpoint]
	<br/><br/>

1. With the application still running refer to the logs and note the appended trace information `[app name, trace id, span id, trace flags]`
	![trace-log]
	<br/><br/>


|[<< Back to Introduction][home-page-link]|[Next Exercise >>][exercise-2-link]|
|:--|--:|
