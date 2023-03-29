using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    [Table("tbl_supplier")]
    public class Supplier : BaseModel
    {
        [Column("supplier_id")]
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("supplier_name")]
        [StringLength(50)]
        public string SupplierName { get; set; } = string.Empty;

        [Column("register_date")]
        public DateTime? RegisterDate { get; set; }

        [Column("supplier_address")]
        [StringLength(100)]
        public string SupplierAddress { get; set; } = string.Empty;

        [Column("supplier_type_id")]
        public int? SupplierTypeId { get; set; }

        [Column("supplier_photo")]
        public string? SupplierPhoto { get; set; } = string.Empty;

        [ForeignKey("SupplierTypeId")]
        public SupplierType? SupplierType { get; set; }
    }
}
