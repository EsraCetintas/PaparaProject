using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace PaparaProject.WebAPI.Filters.Validation
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ValidationAttribute : ActionFilterAttribute
    {
        private Type validatorType;

        public ValidationAttribute(Type ValidatorType)
        {
            validatorType = ValidatorType;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ValidationHelper.Validate(validatorType, context.ActionArguments.Values.ToArray());

            base.OnActionExecuting(context);
        }
    }
}
