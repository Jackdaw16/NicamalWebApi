using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace NicamalWebApi.Models.ViewModels
{
    public class UserForPublication
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        
    }

    public class UserResponseWhenLoggedIn
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SurNames { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string TelephoneContact { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string Address { get; set; }
        public bool IsShelter { get; set; }
        public DateTime CreatedAt { get; set; }

    }

    public class UserResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SurNames { get; set; }
        public string Email { get; set; }
        public string TelephoneContact { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string Address { get; set; }
        public bool IsShelter { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class UserForPublicationDetail
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Image { get; set; }
        public string Province { get; set; }
        public string Address { get; set; }
        public bool IsShelter { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class UserRegister
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string SurNames { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string TelephoneContact { get; set; }
        public string Country { get; set; }
        [Required]
        public string Province { get; set; }
        [Required]
        public string Address { get; set; }
        public bool IsShelter { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class UserLogIn
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class UserLoggedIn
    {
        public UserResponseWhenLoggedIn UserResponse { get; set; }
        public string Token { get; set; }
    }
    
    public class UserShelter
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        
        public List<PublicationCount> Publications { get; set; }
        public int PublicationCount { get; set; }
        public int UrgentCount { get; set; }
    }

    public class UserShelterList
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public int PublicationCount { get; set; }
    }

    public class UserShelterDetail
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string UrlDonation { get; set; }
        public string History { get; set; }
        public string IsShelter { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        
        public int PublicationCount { get; set; }
        public int UrgentCount { get; set; }
    }

    public class UserShelterRegister
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string TelephoneContact { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        public string UrlDonation { get; set; }
        [Required]
        public string History { get; set; }
        public string IsShelter { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Province { get; set; }
        public string Country { get; set; }
    }

    public class UserShelterUpdate
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string TelephoneContact { get; set; }
        public IFormFile Image { get; set; }
        public string UrlDonation { get; set; }
        [Required]
        public string History { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Province { get; set; }
        public string Country { get; set; }
    }

    public class UserShelterPatch
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserShelterLoggedIn
    {
        public UserShelterDetail Shelter { get; set; }
        public string Token { get; set; }
    }
}