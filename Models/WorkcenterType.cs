using System;

namespace Azure.NETCoreAPI.Models
{
    public class WorkcenterType
    {
        public string WorkcenterTypeId { get; set; } = string.Empty; // maps to workcenter_type
        public string? AddUser { get; set; }
        public DateTime? AddDate { get; set; }
        public string? ModUser { get; set; }
        public DateTime? ModDate { get; set; }
    }
}