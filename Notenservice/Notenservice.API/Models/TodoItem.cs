using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    public class TodoItem
    {
        public long Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        [ForeignKey("TodoStatus")]
        public long StatusId { get; set; }
        public TodoStatus TodoStatus { get; set; }

        [ForeignKey("TodoPriority")]
        public long PriorityId { get; set; }
        public TodoPriority TodoPriority { get; set; }
    }
}