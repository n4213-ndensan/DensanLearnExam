using System.ComponentModel.DataAnnotations;
namespace DensanLearnExam.Entities;

public class MyToDo(string title, DateTime dueDate, ToDoState state, string description)
{
    [Required(ErrorMessage = "題名は必須項目です")]
    public string Title { get; set; } = title;
    [Required(ErrorMessage = "期限は必須項目です")]
    public DateTime DueDate { get; set; } = dueDate;
    [Required(ErrorMessage = "状態は必須項目です")]
    public ToDoState State { get; set; } = state;
    public string Description { get; set; } = description;
}
