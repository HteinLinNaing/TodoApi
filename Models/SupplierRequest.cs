using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    public class SupplierRequest
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string SupplierName { get; set; } = string.Empty;

        public DateTime? RegisterDate { get; set; }

        [StringLength(100)]
        public string SupplierAddress { get; set; } = string.Empty;

        public int? SupplierTypeId { get; set; }

        public string? SupplierPhoto { get; set; } = string.Empty;

    }
}
