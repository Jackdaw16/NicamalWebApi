using System;
using System.ComponentModel.DataAnnotations;

namespace NicamalWebApi.Models.ViewModels
{
    public class UserForPublication
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        
    }

    public class UserResponseWhenLoggedIn
    {
        public int Id { get; set; }
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

    public class UserRegister
    {
        public string Name { get; set; }
        public string SurNames { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string TelephoneContact { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
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
}