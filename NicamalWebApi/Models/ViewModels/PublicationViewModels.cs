using System;

namespace NicamalWebApi.Models.ViewModels
{
    public class PublicationsResponseForList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Gender { get; set; }
        public string Personality { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        
        public UserForPublication User { get; set; }
    }

    public class PublicationForReport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Species { get; set; }
        public double Weight { get; set; }
        public string Gender { get; set; }
        public string Personality { get; set; }
        public string History { get; set; }
        public string Observations { get; set; }
        public bool IsUrgent { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}