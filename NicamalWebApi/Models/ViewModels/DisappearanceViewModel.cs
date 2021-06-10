using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using NicamalWebApi.Validation;

namespace NicamalWebApi.Models.ViewModels
{
    public class DisappearanceListResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class DisappearanceDetail
    {
        public int Id { get; set; }
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

    public class DisappearanceCreate
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required] 
        public string TelephoneContact { get; set; }
        [Required]
        public string Description { get; set; }
        public string Country { get; set; }
        [Required]
        public string Province { get; set; }
        [Required]
        public string LastSeen { get; set; }
        [Required]
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}