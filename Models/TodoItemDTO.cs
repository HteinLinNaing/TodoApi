using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    public class TodoItemDTO
    {
        public long Id { get; set; }

        public string? Name { get; set; }

        public bool IsComplete { get; set; }
    }
}