using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SocialMedia.Core.Exeptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SocialMedia.Infrastucture.Filters
{
    public class GlobalExeptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception.GetType()== typeof(BusinessExeption))
            {
                var exeption = (BusinessExeption)context.Exception;
                var validation = new
                {
                    Status = 400,
                    Title = "Bad Request",
                    Detail = exeption.Message
                };
                var Json = new
                {
                    error= new[] {validation}
                };
                context.Result = new BadRequestObjectResult(Json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.ExceptionHandled = true;
            }
        }
    }
}
