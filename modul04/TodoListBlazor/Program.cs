using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using TodoListBlazor;
using TodoListBlazor.Data;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient { 
  BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) 
});
builder.Services.AddSingleton<TodoListService>();

await builder.Build().RunAsync();
