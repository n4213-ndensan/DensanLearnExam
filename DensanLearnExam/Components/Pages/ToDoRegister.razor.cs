using DensanLearnExam.Entities;
using DensanLearnExam.Services;
using Microsoft.AspNetCore.Components;

namespace DensanLearnExam.Components.Pages;

public partial class ToDoRegister
{
    private readonly MyToDo _myToDo = new("", DateTime.Now, ToDoState.未着手, "");

    [CascadingParameter]
    private IStateProvider<MyToDo> ToDoService { get; set; } = default!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    private async Task RegisterAsync(MyToDo item)
    {
        await ToDoService.AddItemAsync(item);
        NavigateToHome();
    }

    private void NavigateToHome()
    {
        NavigationManager.NavigateTo("/");
    }
}
