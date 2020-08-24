# Steeltoe 3.0 packs a mighty punch with many new features

As you make your way through cloud computing and (inevitably) microservices, there will be a never ending list of configuration choices and technical debt. Containerizing legacy code, best practices in new code, developing locally, debugging production apps. That's just a hint of the kinds of questions to be answered.

Fortunately, you’re not the first to embark on this journey. Others have gone ahead and discovered realistic ways to do things. And don’t worry you’re also not the last, so don’t feel like you're playing catch up. 

Running in the cloud means different things to different folks but at its heart, it’s containerization. You may run a container on your desktop, on an enterprise network, or on a public cloud. For Steeltoe, they are all the same.

If you’re not familiar with the Steeltoe project, it is here to help your legacy .NET framework and new .NET Core run extremely well in the cloud. Steeltoe was started in 2015 as a means for .NET Framework applications to enjoy all that Spring Cloud has to offer. Service discovery, external configuration sources, message brokers, service connectors, management endpoints, and more. Over the years Steeltoe has followed Microsoft to .NET Core, but has arranged the project in a way that Framework apps can still be supported.

How does Steeltoe make your .NET microservices better? It takes away the burden of managing all the “stuff” that makes up your service. And seasoned developers will tell you, most of the code in their applications has nothing to do with what the application was meant to do. Most of the code is supporting connections to other services, handling logging, abstracting 3rd party package lock-in, and generally conforming to the environment it will run within. It’s a necessary evil - without it the app is buggy and useless, but creating it doubles the time to production.

Steeltoe makes things like production ready database connections a single line of code and some configuration values. It uses all the powerful abstractions like IConfiguration, ILogging, IHealthProvider, and others to deliver libraries that developers use to spend more time writing business logic and less time with boilerplate-type stuff. Just getting started with ASP.NET? Steeltoe will help you ease into all those possible configuration values. Looking to get in-depth and extend libraries to your needs? Steeltoe is based on common libraries that all .NET uses.

Steeltoe 3.0 is a product from years of learning. Small businesses and large enterprises all have been benefiting and contributing to this new release. It’s a fresh take on cloud opinions that seemingly never get answered. It’s the next evolution for your microservices to go to cloud ninja status.

---
#### If you’re moving from Steeltoe 2 and would likea quick comparision with version 3, refer to [this page](https://steeltoe.io/docs/3/welcome/whats-new).
---

Let's take a look at the new things included in Steeltoe 3. If you’re still wondering how the project fits in your new or existing .NET applications, [learn how here](https://steeltoe.io).

## RabbitMQ messaging in less than 5 lines of code

You might know it as a few different things - eventing, event architecture, event message bus, pub/sub, etc. All of these patterns have one thing in common. They have a heavy dependency on a message broker and queue. That dependency adds extra emphasis on how your app handles its connection & communication to the queue. It needs to be resilient to change and not fail if the queue isn’t there, and it needs to conform to how the message container will be built - but still keep the intended data in-tact.

To help developers use message based systems but not have to manage all the debt that typically comes along with such a thing, Steeltoe 3 introduces the new Messaging component. This component does all the work of resiliency, portability, and fault-tolerance. To implement it you provide the location of the message server and start reading & writing messages. Want to include a custom object in the message? Steeltoe Messaging will take care of converting the bytes. Should the queue be “ensured” when the application is starting up? Configure Steeltoe Messaging to create it, if nothing is present. Do you have potentially many instances of an application that all share the same queue? Steeltoe Messaging will handle everything appropriately for you.

To get started using Messaging ([here is a full example](https://github.com/SteeltoeOSS/Samples/tree/master/Messaging)), provide the server address in `appsettings.json`:

```json
"Spring": {
  "RabbitMq": {
    "Host": "localhost",
    "Port": 5672,
    "Username": "admin",
    "Password": "secret"
  }
}
```
Then using dependency injection, send a message to the queue:
```csharp
public MyService(RabbitTemplate amqpTemplate, MyCustomObject myObj) {
        amqpTemplate.ConvertAndSend("myqueue", myObj);
}
```
Receive messages in a registered callback, managed by Steeltoe and the Rabbit broker:
```csharp
[RabbitListener("myqueue")]
public void Listen(MyCustomObject myObj){
   logger.LogInformation($“Received something: {myObj.MyThing}”);
}
```

Once you’ve got the basics down, it’s time to extend and make the design better with things like custom factories. Steeltoe Messaging will take care of the conversion of a message into a structured type but sometimes you want to intercept that conversion and make decisions. You can create your own `DirectRabbitListenerContainer` and customize to your heart's content. Learn more about custom listener container factories [in the docs](https://dev.steeltoe.io/docs/3/messaging/rabbitmq-intro#basics).

## Legacy and Modern together in the cloud

While all the latest headlines may be around .NET Core, the reality is there is still quite a bit of .NET Framework in production. And the developers of those applications are probably getting pressure to “get that app in a container”. Or at least “reduce the app’s infrastructure”. Steeltoe’s roots are with .NET Framework and every feature in version 2 fully supports Framework apps.

Going forward the team may not bring every feature added in 3 back to 2 but they are actively maintaining the branch. Making sure things stay secure and have up to date patches. If the community shows a need for a certain feature to be back-ported, the Steeltoe team is ready and willing to listen.

## Kubernetes is the future

The current developer landscape is almost demanding that everyone learn something about Kubernetes. Granted you may only be learning why you don’t want it, it’s still a very very popular topic and a platform that many are embracing.

As a developer working with Kubernetes, the platform leaves quite a bit to be desired. The experience is raw and changing rapidly. We are seeing tools emerge that are aimed at helping the developers be more productive in a cluster, but there’s still a ways to go.

The Steeltoe team has begun the journey of supporting K8s specific things with the introduction of a new service discovery client and a new configuration provider.

Adding to the list of supported service registries, Steeltoe now offers developers the option of using native Kubernetes. The really amazing design of this is, there are no additional dependencies to be deployed. DNS is done automatically with the cluster.

Through the magic of reflection and the DNS capabilities in Kubernetes, when you want an application registered as discoverable simply include the `Steeltoe.Discovery.ClientCore` package along with implementing the discovery client in the `HostBuilder` ([here is a full example](https://github.com/SteeltoeOSS/Samples/tree/master/Discovery)).

```csharp
public static IHostBuilder CreateHostBuilder(string[] args) =>
  Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder => {
      webBuilder.UseStartup&lt;Startup>();
    })
    .AddDiscoveryClient();
```

Let's say the registered service was named “fortuneService”. To then discover it in a different application, add the same discovery client as above and also add a new http client factory to the services collection.

```csharp
public void ConfigureServices(IServiceCollection services) {
  services.AddHttpClient("fortunes", c =>
    {
        c.BaseAddress = new Uri("http://fortuneService/api/fortunes/");
    })
    .AddServiceDiscovery()
    .AddTypedClient&lt;IFortuneService, FortuneService>();
	  
    ...
}
```

Now when an new request is created using the http client `var result = await _httpClient.GetStringAsync(“random”);` the full url of http://fortuneService/api/fortunes/random will be used and Kubernetes will resolve it for you.

Kubernetes also has powerful built in features when it comes to getting configuration settings into an application. Combine that with the power of .NET Core’s configuration provider hierarchy and you have one heck of a cloud-native application.

Let's say we have the following ConfigMap ([here is a full example](https://github.com/SteeltoeOSS/Samples/tree/master/Configuration/src/Kubernetes)):

```yaml
apiVersion: v1
kind: ConfigMap
metadata:
  name: kubernetes
  namespace: default
data:
  someKey: someValue
```

We want to get the value of `someKey` into the application but don’t want to add anything platform specific. First add Kubernetes as a configuration provider in the HostBuilder:

```csharp
public static IHostBuilder CreateHostBuilder(string[] args) =>
  Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder => {
      webBuilder.UseStartup&lt;Startup>();
    })
    .AddKubernetes();
```
And then retrieve the configuration value through IConfiguration dependency injection:
```csharp
public HomeController(IConfiguration config) {
  string myKey = config.someKey;
}
```

If you'd like to see more visit the [samples repo](https://github.com/SteeltoeOSS/Samples), or have a look in the [docs](https://steeltoe.io/docs).

## Generate production ready projects and get going fast

Our friends in the Java world using Spring have been enjoying a project called Spring Initializr ([https://start.spring.io](https://start.spring.io)) for years. It’s a brilliant mix of creating ready to go microservices with all the best practices and boilerplate taken care of. The Steeltoe team though the .NET community should have the same kind of tool. So they created the Steeltoe Initializr (start.steeltoe.io).

The Initializr project aims to solve a very common challenge in every organization, of any size. When new projects are started, don’t use a copy of an old one and don’t spend the time writing everything from scratch. Instead do a little planning ahead and know what dependencies the new application will be taking on. Then build a call to Initializr or visit it’s web UI and generate the project.

Not only does this save the developer from writing code to manage config server connections or health endpoints or any other essential of microservices, it also keeps their projects on the latest patched libraries.

When you review the available dependencies in Steeltoe’s hosted version of Initializr you are probably going to have the need to add your own custom dependencies or want to bring the whole tool in-house. That is one of Initializ’s super powers!

Initializr is not an application that needs a managed installation and long term tender longing care. It’s a Nuget distributed package that can be added to a very simple .NET Core and hosted anywhere. When an update is published, just open the project in your IDE and update the package. If you’d like to add the Steeltoe UI on top of the Initializr service clone a copy of the public site. Then you can customize and run internally.

Learn more about Initilizr, [now](https://steeltoe.io/initializr)!

## Container observability

One of the most shocking things to a developer that is new to working with containerized applications is the loss of “visibility” into what the application is doing. Traditionally you had things like IIS logs and the event store, but in a containerized world those things are locked away in private container networks. So the traditional needs to get modern. And Steeltoe is here to get you there.

Observability is typically a sum of logs, traces, and metrics. Sometimes you have them all and sometimes not. Sometimes the data is all on one dashboard and (most of the time) it’s distributed between 2 or more dashboards. Regardless of where the data is being observed, a developer should not have to bring in all kinds of custom dependencies to conform to each dashboard used. They should offer the information in a simple, neutral way with little time spent getting it all wired up.

Steeltoe 3.0 continues the management endpoints in 2.0 but takes things to a deeper place. Along with the following examples, you can also get started using Tanzu Observability with Wavefront or create a Grafana dashboard in our [observability guides](https://dev.steeltoe.io/observability).

* Originally OpenCensus was used for creating standard trace data and metrics. That has been replaced with OpenTelemetry.
* Creating tracing spans has been simplified to a single line in `startup.cs` ([here is a full example](https://github.com/SteeltoeOSS/Samples/tree/master/Management/src/Tracing))

  ```csharp
  public void ConfigureServices(IServiceCollection services) {
    
    services.AddDistributedTracing(Configuration, builder => builder.UseZipkinWithTraceOptions(services));
    ...
  }
  ```

* Extending the [metrics endpoint](https://dev.steeltoe.io/docs/3/management/metrics-observers), there is a new prometheus endpoint that offers a simple way to scrape metrics. Below is an example prometheus.yml configuration ([here is a full example](https://github.com/SteeltoeOSS/Samples/tree/master/Management/src)):

  ```yaml
  scrape_configs:
    - job_name: 'steeltoe-prometheus'
      metrics_path: '/actuator/prometheus'
      scrape_interval: 5s
  ```

* [Dynamic logging](https://dev.steeltoe.io/docs/3/logging) can be implemented with a single statement in the `HostBuilder`

	```csharp
    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder => {
          webBuilder.UseStartup&lt;Startup>();
        })
        .AddDynamicLogging();
  ```

* To use Spring Boot Admin as your management dashboard add the following to `appsettings.json` ([here is a full example](https://github.com/SteeltoeOSS/Samples/tree/master/Management/src/SpringBootAdmin)):

  ```json
  "spring": {
      "boot": {
        "admin": {
          "client": {
            "url": "http://<MY_BOOT_ADMIN_SERVER>:8080"
          }
        }
      }
    }
  ```
  And implement the client in startup.cs
  ```csharp
  public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
    ...
    app.RegisterWithSpringBootAdmin(Configuration);
  }
  ```

## Not just for Cloud Foundry

If you’ve been using Steeltoe for any amount of time you’ll notice a strong hint in the documentation and samples, for Cloud Foundry. That's the application platform Steeltoe grew up on. But as things go the baby bird must leave the nest to discover bigger pastures, and Steeltoe is ready to fly.

In this new version, Steeltoe is spreading its cloud wings and showing strong support for all cloud platforms. In fact it doesn’t even need a cloud platform. You could run a .NET app with Steeltoe in IIS, or Visual Studio debugger, or as a container. The point is Steeltoe is a framework to help developers do what they love - coding. Not managing log messages or some database connection.

Steeltoe still has much love for Cloud Foundry but you’re going to see a lot more local and “get it in a container” kind of examples from the team. A statement like ‘UseCloudFoundryActuators’ is going to have a complementing ‘UseAllActuators’, for those not on CF. And while VCAP is a huge help in Cloud Foundry, you may want to provide your own connection information through `appsettings.json`. You’ll see a lot more flexibility in that direction with this new version.

Here are a few examples of new ways to implement things in the `HostBuilder`:

* Initialize only the health and dynamic logging
  ```csharp
  public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
    	.ConfigureWebHostDefaults(webBuilder => {
    		webBuilder.UseStartup&lt;Startup>();
    	})
    	.AddHealthActuator();
    }
  ```

* Initialize all actuators and optionally secure them behind a policy
  ```csharp
  public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
    	.ConfigureWebHostDefaults(webBuilder => {
    		webBuilder.UseStartup&lt;Startup>();
    	})
    	.AddAllActuators(/*endpoints => endpoints.RequireAuthorization("actuators.read")*/);
    }
  ```

* To instantly enable the enhanced features of app manager, in Tanzu Application Services
  ```csharp
  public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
    	.ConfigureWebHostDefaults(webBuilder => {
    		webBuilder.UseStartup&lt;Startup>();
    	})
    	.UseCloudHosting(5000)
    	.AddCloudFoundryActuators();
    }
  ```

## Learn more and get started today

To get started with any Steeltoe projects, head over to the [getting started guides](https://steeltoe.io/get-started). Combine this with the samples in the [Steeltoe Github repo](https://github.com/SteeltoeOSS/Samples), and you’ll have .NET microservices up and running before you know it! 

Want a complete runtime? Try [Pivotal Web Services](https://run.pivotal.io/) for free! This will give you access to a complete modern app runtime. 

Want to get deeper into creating cloud-native .NET apps? Attend the VMware Pivotal Labs’s [4 -day .NET developer course](https://pivotal.io/platform-acceleration-lab/pal-for-developers-net). You’ll get hands-on cloud-native .NET training learn best practices when creating microservices and become a Steeltoe ninja!
