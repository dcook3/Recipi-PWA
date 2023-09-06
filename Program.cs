using Recipi_PWA;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Recipi_PWA.Models;
using Recipi_PWA.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<StateContainer>();
builder.Services.AddSingleton<PageHistoryState>();

var apiBA = new Uri("https://www.recipiapp.com");


builder.Services.AddHttpClient<IUserService, UserService>(client => client.BaseAddress = apiBA);
builder.Services.AddHttpClient<IRecipeService, RecipeService>(client => client.BaseAddress = apiBA);
builder.Services.AddHttpClient<IPostService, PostService>(client => client.BaseAddress = apiBA);
builder.Services.AddHttpClient<IMediaUploadService, MediaUploadService>(client => client.BaseAddress = apiBA);
builder.Services.AddHttpClient<IIngredientService, IngredientService>(client => client.BaseAddress = apiBA);
builder.Services.AddHttpClient<IHelperService, HelperService>(client => client.BaseAddress = apiBA);

var host = builder.Build();
using var scope = host.Services.CreateScope();

await host.RunAsync();

await builder.Build().RunAsync();