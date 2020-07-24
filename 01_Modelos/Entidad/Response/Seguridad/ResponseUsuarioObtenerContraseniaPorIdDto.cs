using Entidad.Dto.Seguridad;
using System.Collections.Generic;

namespace Entidad.Response.Seguridad
{
    public class ResponseUsuarioObtenerContraseniaPorIdDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public UsuarioObtenerContraseniaPorIdDto Cuerpo { get; set; }
        public ResponseUsuarioObtenerContraseniaPorIdDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
