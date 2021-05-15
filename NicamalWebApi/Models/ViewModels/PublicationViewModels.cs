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
}