using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    public class EmployeeRequest
    {
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        public string EmployeeName { get; set; } = string.Empty;
        [StringLength(100)]
        public string EmployeeAddress { get; set; } = string.Empty;
        public int? EmpDepartmentId { get; set; }
    }
}