using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    public class EmployeeResult
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string EmployeeAddress { get; set; } = string.Empty;
        public int? EmpDepartmentId { get; set; }
        public string? EmpDepartmentName { get; set; }
    }
}