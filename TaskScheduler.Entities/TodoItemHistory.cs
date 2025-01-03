using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskScheduler.Entities
{
    [Table("tblTodoItemHistory")]
    public class TodoItemHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HistoryId { get; set; }
        public string Status { get; set; }
        public DateTime ChangedAt { get; set; } = DateTime.Now;

        // Foreign Key
        [ForeignKey("TodoItemHistory")]
        public int TodoItemId { get; set; }
        public TodoItem TodoItem { get; set; }

    }
}
