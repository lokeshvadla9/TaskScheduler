using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TaskScheduler.Entities
{
    [Table("tblTodoItems")]
    public class TodoItem 
    {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int TodoItemId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Status { get; set; }

            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign Key
            [ForeignKey("User")]
            public int UserId { get; set; }

            [JsonIgnore]
            public User User { get; set; }

        // Navigation Property for TodoItemHistories
        [JsonIgnore]
        public ICollection<TodoItemHistory> TodoItemHistories { get; set; } 

    }
}