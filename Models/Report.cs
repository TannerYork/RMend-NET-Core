using System;
namespace RMendAPI.Models
{
    public class Report
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsPriority { get; set; }
        public string Secret { get; set; }
    }

    public class ReportDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsPriority { get; set; }
    }
}
