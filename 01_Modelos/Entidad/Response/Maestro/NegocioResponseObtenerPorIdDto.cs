using Entidad.Entidad.Maestro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Entidad.Response.Maestro
{
    public class NegocioResponseObtenerPorIdDto
    {
        public int ProcesadoOk { get; set; }
        [DisplayName("ListaErrores")]
        public List<ErrorDto> ListaError { get; set; }
        public NegocioEnt Cuerpo { get; set; }
        public NegocioResponseObtenerPorIdDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
