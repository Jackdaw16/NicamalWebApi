namespace NicamalWebApi.Models
{
    public class PublicationsFilters
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 6;
        
        public Pagination Page
        {
            get => new Pagination() { PageNumber = PageNumber, PageSize = PageSize};
        }
        
        public string Specie { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string TextForSearch { get; set; }
    }
}