﻿namespace NicamalWebApi.Models
{
    public class Pagination
    {
        public int CurrentPage { get; set; } = 1;

        private int countRegistryPerPage = 10;
        private readonly int maxCountRegistryPerPage = 50;

        public int CountRegistryPerPage
        {
            get => countRegistryPerPage;
            set { countRegistryPerPage = (value > maxCountRegistryPerPage) ? countRegistryPerPage : value; }
        }
    }
}