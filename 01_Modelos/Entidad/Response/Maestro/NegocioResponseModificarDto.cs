using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Entidad.Response.Maestro
{
    public class NegocioResponseModificarDto
    {
        public int ProcesadoOk { get; set; }
        [DisplayName("ListaErrores")]
        public List<ErrorDto> ListaError { get; set; }
        public NegocioResponseModificarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
