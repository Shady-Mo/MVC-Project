using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MVCProject.ValidationAttributes
{
    public class ValidCountryAttribute: ValidationAttribute
    {
        private static readonly HashSet<string> ValidCountries = GetValidCountries();

        private static HashSet<string> GetValidCountries()
        {
            var countries = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            var cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

            foreach (var culture in cultures)
            {
                try
                {
                    // التعديل هنا: استخدمنا Name بدل LCID
                    var region = new RegionInfo(culture.Name);
                    countries.Add(region.EnglishName);
                }
                catch (ArgumentException)
                {
                    // لو في أي Culture غريبة ملهاش Region، هنتجاهلها عشان السيرفر ميقعش
                    continue;
                }
            }

            return countries;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var countryName = value.ToString().Trim();

            if (ValidCountries.Contains(countryName))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage ?? "There is no country having this name");
        }
    }
}
