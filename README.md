# Steeltoe Main Site

	The site was built using [Blazor](https://docs.microsoft.com/aspnet/core/blazor/) in Visual Studio. 

## Install Dependencies

Requires .NET 8.0+ SDK

## Local testing of Steeltoe site

1. Change into the Client project folder
```powershell
PS> cd src/Client
```

1. Run the application with the `watch` command. So as changes are made, the site will refresh.

```powershell
PS> dotnet watch run

watch : Started
info: Microsoft.Hosting.Lifetime[0]
			Now listening on: https://localhost:8080
info: Microsoft.Hosting.Lifetime[0]
			Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
			Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
			Content root path: C:\source\Steeltoe\MainSite\src\Client
```

Visit site locally at [https://localhost:8080](https://localhost:8080)
