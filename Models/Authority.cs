using System;
using System.Collections.Generic;
namespace RMendAPI.Models
{
    public class Authority
    {
        public int AuthorityId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public string WebsiteUrl { get; set; }
        public string AuthCode { get; set; }
        public ICollection<Report> Reports { get; set; }
    }
}