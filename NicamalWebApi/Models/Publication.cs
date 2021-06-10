using System;

namespace NicamalWebApi.Models
{
    public class Publication
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Species { get; set; }
        public double Weight { get; set; }
        public string Gender { get; set; }
        public string Personality { get; set; }
        public string History { get; set; }
        public string Observations { get; set; }
        public bool IsUrgent { get; set; }
        public string Age { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        
        public string UserId { get; set; }
        public User User { get; set; }
        
        public Report Report { get; set; }
        
    }
}