using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    [Table("heroes")]
    public class Hero : BaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        [Required]
        [StringLength(20)]
        public string? Name { get; set; }

        [Column("address")]
        [Required]
        [StringLength(100)]
        public string? Address { get; set; }

        [Column("secret")]
        public string? Secret { get; set; }
    }
}