namespace DensanLearnExam.Services;

public interface IStateProvider<T>
{
    public ICollection<T> GetState();
    public ValueTask<ICollection<T>> GetStateAsync();
    public void AddItem(T state);
    public ValueTask AddItemAsync(T state);
    public bool HasState();
}
