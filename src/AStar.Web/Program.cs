using AStar.FilesApi.Client.SDK.FilesApi;
using AStar.ImagesApi.Client.SDK.ImagesApi;
using AStar.Web.Components;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

        _ = builder.Services.AddOptions<FilesApiConfiguration>()
                    .Bind(builder.Configuration.GetSection(FilesApiConfiguration.SectionLocation))
                    .ValidateDataAnnotations()
                    .ValidateOnStart();
        _ = builder.Services.AddOptions<ImagesApiConfiguration>()
                    .Bind(builder.Configuration.GetSection(ImagesApiConfiguration.SectionLocation))
                    .ValidateDataAnnotations()
                    .ValidateOnStart();

        _ = builder.Services.AddHttpClient<FilesApiClient>().ConfigureHttpClient((serviceProvider, client) =>
        {
            client.BaseAddress = serviceProvider.GetRequiredService<IOptions<FilesApiConfiguration>>().Value.BaseUrl;
            client.DefaultRequestHeaders.Accept.Add(new("application/json"));
        });
        _ = builder.Services.AddHttpClient<ImagesApiClient>().ConfigureHttpClient((serviceProvider, client) =>
        {
            client.BaseAddress = serviceProvider.GetRequiredService<IOptions<ImagesApiConfiguration>>().Value.BaseUrl;
            client.DefaultRequestHeaders.Accept.Add(new("application/json"));
        });
        
var app = builder.Build();

_ = app.UseExceptionHandler("/Error", createScopeForErrors: true);

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

public partial class Program{}