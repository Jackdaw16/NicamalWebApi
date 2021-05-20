using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace NicamalWebApi.Validation
{
    public class TypeFileValidation: ValidationAttribute
    {
        private readonly string[] _tipesValid;
        public TypeFileValidation()
        {
            _tipesValid = new string[] {"image/jpeg", "image/png"};
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value == null)
                return ValidationResult.Success;
            
            IFormFile formFile = value as IFormFile;

            if (formFile == null)
                return ValidationResult.Success;

            if (!_tipesValid.Contains(formFile.ContentType))
                return new ValidationResult("The image format is not correct");
            
            return ValidationResult.Success;
        }
    }
}