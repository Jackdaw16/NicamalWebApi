﻿namespace NicamalWebApi.Models
{
    public class Pagination
    {
        private const int maxPageSize = 50;
        
        public int Page { get; set; } = 1;

        private int _pageSize = 6;
        
        public int PageSize
        {
            get => _pageSize;
            set { _pageSize = (value > maxPageSize) ? maxPageSize : value; }
        }
    }
}