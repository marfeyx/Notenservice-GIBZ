using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace TodoApi.Models
{
    public class TodoPriority
    {
        public long Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public virtual ICollection<TodoItem> TodoItems { get; set; }
    }
}