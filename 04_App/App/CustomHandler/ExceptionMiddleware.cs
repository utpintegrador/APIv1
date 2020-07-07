using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace App.CustomHandler
{
    public class ExceptionMiddleware
    {
        //private readonly RequestDelegate _next;
        //public ExceptionMiddleware(RequestDelegate next)
        //{
        //    _next = next;
        //}

        //public async Task InvokeAsync(HttpContext httpContext)
        //{
        //    try
        //    {
        //        await _next.Invoke(httpContext);
        //    }
        //    catch
        //    {
        //        throw;
        //        //if (!httpContext.Response.HasStarted)
        //        //{
        //        //    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        //        //    httpContext.Response.ContentType = "application/json";
        //        //}
        //        ////httpContext.Response.StatusCode = 400;
        //        //return;
        //        //    //HandleExceptionAsync(httpContext);
        //    }
        //}

        ////private Task HandleExceptionAsync(HttpContext context)
        ////{
        ////    try
        ////    {
        ////        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        ////    }
        ////    catch (Exception ex)
        ////    {

        ////    }
        ////    return context.Response.WriteAsync(new ErrorDetails
        ////     {
        ////         StatusCode = context.Response.StatusCode,
        ////         Message = "Internal server error from middleware"
        ////     }.ToString());
        ////}

    }
}
