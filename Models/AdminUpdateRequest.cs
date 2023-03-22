using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    public class AdminUpdateRequest
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string AdminName { get; set; } = string.Empty;

        [Required]
        public string? Email { get; set; }

        [StringLength(50)]
        [Required]
        public string LoginName { get; set; } = string.Empty;

        [Required]
        // public string? Password { get; set; }
        // public string? Salt { get; set; }

        public bool IsActive { get; set; }

        public int? AdminLevelId { get; set; }

        public string? AdminPhoto { get; set; } = string.Empty;

    }
}