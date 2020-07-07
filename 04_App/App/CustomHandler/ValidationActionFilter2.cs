using Entidad.Dto.Seguridad;
using Entidad.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;

namespace App.CustomHandler
{
    public class ValidationActionFilter2 : ActionFilterAttribute
    {
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

                        UsuarioTokenDto response = new UsuarioTokenDto
                        {
                            ListaError = ListaError
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
