using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    public class CustomerRequest
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        public DateTime? RegisterDate { get; set; }

        [StringLength(100)]
        public string CustomerAddress { get; set; } = string.Empty;

        public int? CustomerTypeId { get; set; }

        public string? CustomerPhoto { get; set; } = string.Empty;

    }
}