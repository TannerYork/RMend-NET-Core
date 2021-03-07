using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace RMendAPI.Models
{
    public class Report
    {
        public int ReportId { get; set; }
        public string Name { get; set; }
        public bool IsPriority { get; set; }


        [ForeignKey("Authority")]
        public int AuthorityId { get; set; }
        public Authority Authority { get; set; }
    }

    public class UserReport
    {
        public int ReportId { get; set; }
        public string Name { get; set; }
    }
}
