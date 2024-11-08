using DensanLearnExam.Entities;
namespace DensanLearnExam.Library;

public class MyToDoComparer : IComparer<MyToDo>, IEqualityComparer<MyToDo>
{
    public int Compare(MyToDo? x, MyToDo? y)
    {
        // nullチェック
        int result = (x, y) switch
        {
            (null, null) => 0,
            (null, not null) => -1,
            (not null, null) => 1,
            (not null, not null) => int.MaxValue
        };

        if (result != int.MaxValue)
        {
            return result;
        }
        // まず状態で比較
        if (x!.State != y!.State)
        {
            result = x.State.CompareTo(y.State);
        }
        // 状態が同じ場合は期限で比較
        // この時、期限が過ぎているものは最後にする
        else
        {
            DateTime now = DateTime.Now;
            DateTime xDueDate = x.DueDate;
            DateTime yDueDate = y.DueDate;

            result = (now > xDueDate, now > yDueDate) switch
            {
                (true, true) or (false, false) => xDueDate.ToShortDateString().CompareTo(yDueDate.ToShortDateString()),
                (true, false) => -1,
                (false, true) => 1
            };
        }

        return result;
    }

    public bool Equals(MyToDo? x, MyToDo? y)
    {
        bool result = Compare(x, y) == 0;
        if (x is not null && y is not null)
            result = x.Title == y.Title && result;
        return result;
    }
    public int GetHashCode(MyToDo obj)
    {
        return obj.GetHashCode();
    }
}
