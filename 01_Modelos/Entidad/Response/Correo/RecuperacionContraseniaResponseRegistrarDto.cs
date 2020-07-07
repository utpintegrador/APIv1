using Entidad.Dto.Correo;
using System.Collections.Generic;

namespace Entidad.Response.Correo
{
    public class RecuperacionContraseniaResponseRegistrarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public RecuperacionContraseniaObtenerRegistradoDto Cuerpo { get; set; }

        public RecuperacionContraseniaResponseRegistrarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
