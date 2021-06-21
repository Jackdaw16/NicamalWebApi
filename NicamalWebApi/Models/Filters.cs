namespace NicamalWebApi.Models
{
    public class PublicationsFilters
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 6;
        
        public Pagination Pagination
        {
            get => new Pagination() { Page = Page, PageSize = PageSize};
        }
        
        public string Specie { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string TextForSearch { get; set; }
    }
}