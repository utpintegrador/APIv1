using System.Collections.Generic;

namespace Entidad.Response.Seguridad
{
    public class ResponseUsuarioModificarModoAdminDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public ResponseUsuarioModificarModoAdminDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
