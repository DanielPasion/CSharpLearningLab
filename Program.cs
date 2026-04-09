using CSharpLearningLab;
using CSharpLearningLab.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Root component: a tiny placeholder Blazor mounts at #app. The existing vanilla-JS UI
// still renders everything — Blazor is only booted here so the .NET runtime is available
// and our [JSInvokable] methods can be called from app.js.
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient is needed by Blazor's default services — point it at the app's base address.
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// CodeExecutionService is stateless and expensive to construct (it holds the Roslyn
// MetadataReference list), so register it as a singleton.
builder.Services.AddSingleton<CodeExecutionService>();

var host = builder.Build();

// Give the static JSInvokable bridge a handle to the singleton runner before we start.
// The bridge itself can't use DI because [JSInvokable] methods must be static.
JsBridge.Initialize(host.Services.GetRequiredService<CodeExecutionService>());

await host.RunAsync();
