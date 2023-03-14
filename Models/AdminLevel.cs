using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    [Table("tbl_admin_level")]
    public partial class AdminLevel
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        [Column("level_name")]
        public string LevelName { get; set; } = string.Empty;
    }
}