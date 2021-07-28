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
        public string Address { get; set; }
        public string Province { get; set; }
        public string Text { get; set; }
    }

    public class ShelterFilters
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 6;

        public Pagination Pagination
        {
            get => new Pagination() {Page = Page, PageSize = PageSize}; 
        }

        public string Address { get; set; }
        public string Province { get; set; }
        public string Text { get; set; }
    }

    public class ShelterPublicationsFilters
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 6;

        public Pagination Pagination
        {
            get => new Pagination() {Page = Page, PageSize = PageSize}; 
        }
        
        public string Text { get; set; }
    }
}