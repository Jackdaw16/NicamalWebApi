using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using NicamalWebApi.Models;

namespace NicamalWebApi.Validation
{
    public class ImageSizeValidation: ValidationAttribute
    {
        private readonly int _maxSizeImage;

        public ImageSizeValidation(int maxSizeImage)
        {
            _maxSizeImage = maxSizeImage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value == null)
                return ValidationResult.Success;
            
            IFormFile formFile = value as IFormFile;

            if (formFile == null)
                return ValidationResult.Success;

            if (formFile.Length > _maxSizeImage * 1024 * 1024)
                return new ValidationResult("The weight of the file cannot be greater than " + _maxSizeImage + "mb");
            
            return ValidationResult.Success;
        }
    }
}