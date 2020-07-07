using System.Collections.Generic;

namespace Entidad.Response.Correo
{
    public class ResponseRecuperacionContraseniaModificarContraseniaDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }

        public ResponseRecuperacionContraseniaModificarContraseniaDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
