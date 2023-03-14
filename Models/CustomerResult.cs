using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    public class CustomerResult
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime? RegisterDate { get; set; }
        public string CustomerAddress { get; set; } = string.Empty;
        public int? CustomerTypeId { get; set; }
        public string? CustomerPhoto { get; set; } = string.Empty;
        public String? CustomerType { get; set; }
    }
}