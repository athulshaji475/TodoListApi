using System.ComponentModel.DataAnnotations;

namespace ToDoList.TableModels
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? Status { get; set; }
        public Nullable<DateTime> TaskTime { get; set; }
        public Nullable<int> SlNo { get; set; }
    }

}
