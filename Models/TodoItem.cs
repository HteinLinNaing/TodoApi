using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    [Table("todo_items")]
    public class TodoItem
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("name")]
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        [Column("is_complete")]
        [Required]
        public bool IsComplete { get; set; }

        [Column("secret")]
        public string? Secret { get; set; }
    }
}