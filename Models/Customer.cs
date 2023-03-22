using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    [Table("tbl_customer")]
    public class Customer : BaseModel
    {
        [Column("customer_id")]
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("customer_name")]
        [StringLength(50)]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [Column("register_date")]
        public DateTime? RegisterDate { get; set; }

        [Column("customer_address")]
        [StringLength(100)]
        public string CustomerAddress { get; set; } = string.Empty;

        [Column("customer_type_id")]
        public int? CustomerTypeId { get; set; }

        [Column("customer_photo")]
        public string? CustomerPhoto { get; set; } = string.Empty;

        [ForeignKey("CustomerTypeId")]
        public CustomerType? CustomerType { get; set; }
    }
}