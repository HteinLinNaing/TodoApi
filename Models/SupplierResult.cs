using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    public class SupplierResult
    {
        public int Id { get; set; }
        public string SupplierName { get; set; } = string.Empty;
        public DateTime? RegisterDate { get; set; }
        public string SupplierAddress { get; set; } = string.Empty;
        public int? SupplierTypeId { get; set; }
        public string? SupplierPhoto { get; set; } = string.Empty;
        public String? SupplierType { get; set; }
    }
}