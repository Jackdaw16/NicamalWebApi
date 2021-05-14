namespace NicamalWebApi.Models.ViewModels
{
    public class PublicationResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Gender { get; set; }
        public string Personality { get; set; }
        
        public UserForPublication User { get; set; }
    }
}