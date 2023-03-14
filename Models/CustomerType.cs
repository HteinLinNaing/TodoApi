using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    [Table("tbl_customer_type")]
    public partial class CustomerType
    {
        [Column("customer_type_id")]
        [Key]
        public int CustomerTypeId { get; set; }

        [MaxLength(50)]
        [Required]
        [Column("customer_type_name")]
        public string CustomerTypeName { get; set; } = string.Empty;

        [MaxLength(100)]
        [Required]
        [Column("customer_type_description")]
        public string CustomerTypeDescription { get; set; } = string.Empty;
    }
}