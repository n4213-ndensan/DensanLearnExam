using DensanLearnExam.Entities;
using DensanLearnExam.Services;
using Microsoft.AspNetCore.Components;

namespace DensanLearnExam.Components.Pages;

public partial class Home
{
    // 初期状態
    private ICollection<MyToDo> myToDoes = [
        new("ToDo1", DateTime.Now, ToDoState.未着手, "1行目\r\n2行目"),
        new("ToDo2", DateTime.Now, ToDoState.仕掛中, "1行目\r\n2行目"),
        new("ToDo3", DateTime.Now, ToDoState.完了, "1行目\r\n2行目"),
        ];

    // インターフェースを通じて状態を管理する
    // サービスインスタンスは親コンポーネントから受け取る
    [CascadingParameter]
    private IStateProvider<MyToDo> StateProvider { get; set; } = default!;

    private static int GetEndOfFirstLine(string description)
    {
        int index = description.IndexOfAny(['\r', '\n']);
        return index == -1 ? description.Length : index;
    }

    private async Task LoadToDoesAsync()
    {
        // 状態があれば読み込むが、なければ何もしない
        if (!StateProvider.HasState())
            return;
        
        myToDoes = await StateProvider.GetStateAsync();
        await InvokeAsync(StateHasChanged);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            // 初回レンダリング時に状態を設定する
            foreach (var todo in myToDoes)
            {
                await StateProvider.AddItemAsync(todo);
            }
        }
        // 状態を読み込む
        await LoadToDoesAsync();
    }
}
