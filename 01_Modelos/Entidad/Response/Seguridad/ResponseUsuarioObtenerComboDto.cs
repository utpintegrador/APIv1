using Entidad.Dto.Seguridad;
using System.Collections.Generic;

namespace Entidad.Response.Seguridad
{
    public class ResponseUsuarioObtenerComboDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public List<UsuarioObtenerComboDto> Cuerpo { get; set; }
        public ResponseUsuarioObtenerComboDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
