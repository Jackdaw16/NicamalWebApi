using System;

namespace NicamalWebApi.Models
{
    public class Disappearance
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string TelephoneContact { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string LastSeen { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        
    }
}