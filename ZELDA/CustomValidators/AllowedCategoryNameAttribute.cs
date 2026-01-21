using System.ComponentModel.DataAnnotations;

namespace ZELDA.PersonalizedValidator
{
    public class AllowedCategoryNameAttribute : ValidationAttribute
    {
        private readonly string[] _allowedNames;

        public AllowedCategoryNameAttribute(string[] allowedNames)
        {
            _allowedNames = allowedNames;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string str)
            {
                foreach (var name in _allowedNames)
                {
                    if (string.Equals(str, name, StringComparison.OrdinalIgnoreCase))
                    {
                        return ValidationResult.Success;
                    }
                }

                string allowedList = string.Join(", ", _allowedNames);
                return new ValidationResult($"Invalid category name. Allowed names: {allowedList}");
            }

            return new ValidationResult("Category name is required.");
        }
    }
}