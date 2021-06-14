using System;

namespace NicamalWebApi.Models
{
    public class Sanction
    {
        public string Id { get; set; }
        public string Reason { get; set; }
        public string Applied { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; }
    }
}