using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Entidad.Response.Maestro
{
    public class NegocioResponseEliminarDto
    {
        public int ProcesadoOk { get; set; }
        [DisplayName("ListaErrores")]
        public List<ErrorDto> ListaError { get; set; }
        public NegocioResponseEliminarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
