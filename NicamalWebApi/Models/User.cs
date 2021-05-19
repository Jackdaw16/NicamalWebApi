using System;
using System.Collections.Generic;

namespace NicamalWebApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurNames { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string TelephoneContact { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string Address { get; set; }
        public bool Verify { get; set; }
        public bool IsShelter { get; set; }
        public bool IsBanned { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        public List<Publication> Publications { get; set; }

        public Report Reported { get; set; }
        public Report Report { get; set; }
        
    }
}