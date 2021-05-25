using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using NicamalWebApi.Validation;

namespace NicamalWebApi.Models.ViewModels
{
    public class PublicationsResponseForList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Gender { get; set; }
        public string Personality { get; set; }
        public string Species { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        
        public UserForPublication User { get; set; }
    }

    public class PublicationDetail
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
        
        public UserForPublicationDetail User { get; set; }
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

    public class PublicationCreate
    {
        [Required]
        public string Name { get; set; }
        [TypeFileValidation]
        [ImageSizeValidation(4)]
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public string Species { get; set; }
        [Required]
        public double Weight { get; set; }
        [Required]
        public string Gender { get; set; }
        public string Personality { get; set; }
        [Required]
        public string History { get; set; }
        public string Observations { get; set; }
        [Required]
        public bool IsUrgent { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int UserId { get; set; }
    }
}