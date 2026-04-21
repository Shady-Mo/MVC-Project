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
                var region = new RegionInfo(culture.LCID);
                countries.Add(region.EnglishName);
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
