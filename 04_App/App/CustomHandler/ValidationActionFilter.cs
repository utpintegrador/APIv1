using Entidad.Response;
using Entidad.Response.Comun;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace App.CustomHandler
{
    public class ValidationActionFilter: ActionFilterAttribute
    {
        //public override void OnResultExecuting(ResultExecutingContext context)
        //{
        //    //context.HttpContext.Response.StatusCode = 400;
        //    base.OnResultExecuting(context);
        //}

        //public override void OnResultExecuted(ResultExecutedContext context)
        //{
        //    //context.HttpContext.Response.StatusCode = 400;
        //    base.OnResultExecuted(context);
        //}

        //public override void OnActionExecuted(ActionExecutedContext context)
        //{
        //    try
        //    {
        //        PropertyInfo pi = context.Result.GetType().GetProperty("StatusCode");
        //        int httpCode = -1;
        //        if (pi != null && pi.CanRead && pi.PropertyType == typeof(int))
        //        {
        //            httpCode = (int)pi.GetValue(context.Result);
        //        }

        //        context.HttpContext.Response.StatusCode = httpCode;
        //    }
        //    catch (Exception ex) { }

        //    base.OnActionExecuted(context);
        //}

        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var modelState = actionContext.ModelState;
            if (!modelState.IsValid)
            {
                try
                {
                    var listaValores = modelState.Select(x => x.Value).ToList();
                    if (listaValores.Any())
                    {
                        List<ErrorDto> ListaError = new List<ErrorDto>();
                        foreach (var item in listaValores)
                        {
                            if (item != null)
                            {
                                foreach (var erro in item.Errors)
                                {
                                    ListaError.Add(new ErrorDto
                                    {
                                        Mensaje = erro.ErrorMessage
                                    });
                                }
                            }
                        }

                        ValidacionModeloResponseDto response = new ValidacionModeloResponseDto
                        {
                            ListaError = ListaError,
                            ProcesadoOk = 0,
                            Cuerpo = null,
                            IdGenerado = 0
                        };

                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(response);
                        actionContext.HttpContext.Response.StatusCode = 400;
                        actionContext.HttpContext.Response.ContentType = "application/json";
                        actionContext.HttpContext.Response.WriteAsync(json);
                    }
                }
                catch { }
            }

            base.OnActionExecuting(actionContext);
        }

    }

}
