using DensanLearnExam.Components;
using DensanLearnExam.Entities;
using DensanLearnExam.Services;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddRadzenComponents();
// IStateProvider<MyToDo>�̎�����ToDoService�ɐݒ肵�AScoped�T�[�r�X�Ƃ��ēo�^
builder.Services.AddScoped<IStateProvider<MyToDo>, ToDoService>();
// Scoped�T�[�r�X�����[�U�[�X�R�[�v�ɂ��邽�߁ACascadingValue�Ƃ��ēo�^
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
