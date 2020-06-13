using Entidad.Dto.Correo;
using Entidad.Dto.Global;
using System.Collections.Generic;
using System.ComponentModel;

namespace Entidad.Dto.Response.Correo
{
    public class RecuperacionContraseniaResponseObtenerPorCodigoDto
    {
        public int ProcesadoOk { get; set; }
        [DisplayName("ListaErrores")]
        public List<ErrorDto> ListaError { get; set; }
        public RecuperacionContraseniaObtenerPorCodigoDto Cuerpo { get; set; }

        public RecuperacionContraseniaResponseObtenerPorCodigoDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
