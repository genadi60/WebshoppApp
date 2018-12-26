using System.ComponentModel.DataAnnotations;
using WebshopApp.Services.DataServices;
using WebshopApp.Services.DataServices.Contracts;

namespace WebshopApp.Web.Models.CustomAttributes
{
    public class ValidCategoryIdAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var service = (ICategoriesService)validationContext
                .GetService(typeof(ICategoriesService));

            if (service.IsCategoryIdValid((int)value))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Invalid category id!");
            }
        }
    }
}
