using Entidad.Dto.Correo;
using Entidad.Dto.Global;
using System.Collections.Generic;
using System.ComponentModel;

namespace Entidad.Dto.Response.Correo
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
