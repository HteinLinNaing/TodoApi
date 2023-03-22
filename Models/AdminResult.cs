using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    public class AdminResult
    {
        public int Id { get; set; }
        public string AdminName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string LoginName { get; set; } = string.Empty;
        // public string? Password { get; set; }
        // public string? Salt { get; set; }
        public bool IsActive { get; set; }
        public int? AdminLevelId { get; set; }
        public string? AdminPhoto { get; set; } = string.Empty;

        public String? AdminLevel { get; set; }
    }
}