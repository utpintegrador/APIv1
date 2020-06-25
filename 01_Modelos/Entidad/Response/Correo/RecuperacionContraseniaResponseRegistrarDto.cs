using Entidad.Dto.Correo;
using Entidad.Response;
using System.Collections.Generic;
using System.ComponentModel;

namespace Entidad.Response.Correo
{
    public class RecuperacionContraseniaResponseRegistrarDto
    {
        public int ProcesadoOk { get; set; }
        [DisplayName("ListaErrores")]
        public List<ErrorDto> ListaError { get; set; }
        public RecuperacionContraseniaObtenerRegistradoDto Cuerpo { get; set; }

        public RecuperacionContraseniaResponseRegistrarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
