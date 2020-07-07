using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad.Response.Comun
{
    public class ResponseValidacionModelo2Dto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
    }
}
