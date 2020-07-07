using Entidad.Dto.Correo;
using System.Collections.Generic;

namespace Entidad.Response.Correo
{
    public class RecuperacionContraseniaResponseObtenerPorCodigoDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public RecuperacionContraseniaObtenerPorCodigoDto Cuerpo { get; set; }

        public RecuperacionContraseniaResponseObtenerPorCodigoDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
