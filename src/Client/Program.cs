using Steeltoe.Client.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "DocumentationPolicy",
           policy =>
           {
               policy.WithOrigins("https://docs.steeltoe.io")
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
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseCors("DocumentationPolicy");

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
