using System;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers.Maestro
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PruebaController : ControllerBase
    {

        // GET: api/Prueba/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            string resultado = string.Empty;
            switch (id)
            {
                case 1:
                    {
                        resultado = string.Format("Direccion url directorio: {0}", AppDomain.CurrentDomain.BaseDirectory);
                        break;
                    }
                case 2:
                    {
                        resultado = string.Format("Direccion url directorio: {0}", AppDomain.CurrentDomain.BaseDirectory);
                        break;
                    }
                default:
                    break;
            }

            return resultado;
        }

 
    }
}
