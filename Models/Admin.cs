using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    [Table("tbl_admins")]
    public class Admin : BaseModel
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("admin_name")]
        [StringLength(50)]
        public string AdminName { get; set; } = string.Empty;

        [Required]
        [Column("email")]
        [StringLength(50)]
        public string? Email { get; set; }

        [Column("login_name")]
        [StringLength(50)]
        public string LoginName { get; set; } = string.Empty;

        [Required]
        [Column("password")]
        public string? Password { get; set; }

        [Required]
        [Column("salt")]
        public string? Salt { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        [Column("admin_level_id")]
        public int? AdminLevelId { get; set; }

        [Column("admin_photo")]
        public string? AdminPhoto { get; set; } = string.Empty;

        [ForeignKey("AdminLevelId")]
        public AdminLevel? AdminLevel { get; set; }
    }
}