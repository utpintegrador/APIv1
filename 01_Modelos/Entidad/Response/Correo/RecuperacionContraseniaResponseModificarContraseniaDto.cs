using System.Collections.Generic;

namespace Entidad.Response.Correo
{
    public class RecuperacionContraseniaResponseModificarContraseniaDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }

        public RecuperacionContraseniaResponseModificarContraseniaDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
