using Steeltoe.Client.Components;
using Steeltoe.Client.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "DocumentationPolicy",
           policy =>
           {
               policy.WithOrigins("http://localhost:8082", "https://docs-dev.steeltoe.io", "https://docs.steeltoe.io")
                       .AllowAnyHeader()
                       .WithMethods("GET");
           });
});

builder.Services.Configure<CalendarEventOptions>(builder.Configuration.GetSection("CalendarEvents"));
builder.Services.AddHttpContextAccessor();
builder.Services.AddOptions<DocsSiteOptions>()
    .Bind(builder.Configuration.GetSection("DocsSite"))
    .ValidateDataAnnotations()
    .PostConfigure(docsSiteOptions =>
    {
        docsSiteOptions.SetUrls();
    });

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAntiforgery();
app.UseCors("DocumentationPolicy");
app.MapRazorComponents<App>();

app.Run();
