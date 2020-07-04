using Entidad.Dto.Maestro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Entidad.Response.Maestro
{
    public class NegocioResponseObtenerDto
    {
        public int ProcesadoOk { get; set; }
        [DisplayName("ListaErrores")]
        public List<ErrorDto> ListaError { get; set; }
        public List<NegocioObtenerDto> Cuerpo { get; set; }
        public NegocioResponseObtenerDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
