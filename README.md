# Steeltoe Main Site

	The site was built using [Blazor](https://docs.microsoft.com/en-us/aspnet/core/blazor/) in Visual Studio 2019. 

## Install Dependencies
Requires .NET Core SDK 3.1

```powershell
PS> dotnet new -i Microsoft.AspNetCore.Blazor.Templates
```

## Local testing of Steeltoe site

1. Change into the Client project folder
```powershell
PS> cd src/Client
```

2. Run the application with the `watch` command. So as changes are made in Visual Studio, the site will refresh.
```powershell
PS> dotnet watch run

watch : Started
info: Microsoft.Hosting.Lifetime[0]
			Now listening on: http://localhost:8080
info: Microsoft.Hosting.Lifetime[0]
			Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
			Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
			Content root path: C:\source\Steeltoe\MainSite\src\Client
```

Visit site locally at [http://localhost:8080](http://localhost:8080)
