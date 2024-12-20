using DensanLearnExam.Components;
using DensanLearnExam.Entities;
using DensanLearnExam.Services;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddRadzenComponents();
// IStateProvider<MyToDo>の実装をToDoServiceに設定し、Scopedサービスとして登録
builder.Services.AddScoped<IStateProvider<MyToDo>, ToDoService>();
// inject以外の方法で受け取るサンプルとして、CascadingValueに登録
builder.Services.AddCascadingValue(sp => sp.GetRequiredService<IStateProvider<MyToDo>>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
