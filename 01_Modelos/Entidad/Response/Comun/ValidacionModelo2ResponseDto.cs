using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad.Response.Comun
{
    public class ValidacionModelo2ResponseDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
    }
}
