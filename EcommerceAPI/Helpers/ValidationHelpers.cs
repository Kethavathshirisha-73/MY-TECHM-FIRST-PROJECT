using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Helpers
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string fileName)
            {
                var extension = Path.GetExtension(fileName);
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult($"File extension {extension} is not allowed. Allowed extensions: {string.Join(", ", _extensions)}");
                }
            }

            return ValidationResult.Success;
        }
    }

    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult($"File size cannot exceed {_maxFileSize} bytes");
                }
            }

            return ValidationResult.Success;
        }
    }
}
