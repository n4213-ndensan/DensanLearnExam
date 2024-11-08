using DensanLearnExam.Entities;
using DensanLearnExam.Library;
using System.Collections.Concurrent;

namespace DensanLearnExam.Services;
/// <summary>
/// ToDoリストの管理機能を提供するDIサービスです。<br />
/// ToDoを追加、ToDoリストの取得、ToDoリストのソートを行います。<br />
/// </summary>
public class ToDoService : IStateProvider<MyToDo>
{
    private readonly List<MyToDo> _state = [];
    private readonly MyToDoComparer _comparer = new();
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    private void SortState() => _state.Sort(_comparer);
    private void InternalAddItem(MyToDo state)
    {
        if (_state.Contains(state, _comparer as IEqualityComparer<MyToDo>)) return;

        _state.Add(state);
        SortState();
    }

    public ICollection<MyToDo> GetState() => _state;
    public ValueTask<ICollection<MyToDo>> GetStateAsync() => new(_state);

    public void AddItem(MyToDo state)
    {
        _semaphore.Wait();
        try
        {
            InternalAddItem(state);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async ValueTask AddItemAsync(MyToDo state)
    {
        await _semaphore.WaitAsync();
        try
        {
            InternalAddItem(state);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public bool HasState() => _state.Count != 0;
}
