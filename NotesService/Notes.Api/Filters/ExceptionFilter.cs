using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Notes.Services.Model;
using System;

namespace Notes.Api.Filters
{
    /// <summary>
    ///  Filter /Hook to handle REST api exception
    /// </summary>
    public class ExceptionFilter: IActionFilter, IOrderedFilter
    {

        public int Order { get; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is IdpWebException idpWebException)
            {
                context.Result = new ObjectResult(new { Message = idpWebException.Message })
                {
                    StatusCode = (int)idpWebException.StatusCode,
                };
                context.ExceptionHandled = true;
                return;
            }

            if (context.Exception is ValidationException validationException)
            {
                context.Result = new ObjectResult(new { Message = validationException.Message })
                {
                    StatusCode = 400,
                };
                context.ExceptionHandled = true;
                return;
            }

            if (context.Exception is Exception exception)
            {
                context.Result = new ObjectResult(new { Message = exception.Message })
                {
                    StatusCode = 500,
                    
                    
                };
                context.ExceptionHandled = true;
                return;
            }
        }
    }
}
