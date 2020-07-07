using Entidad.Dto.Seguridad;
using System.Collections.Generic;

namespace Entidad.Response.Seguridad
{
    public class ResponseUsuarioObtenerDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public List<UsuarioObtenerDto> Cuerpo { get; set; }
        public int CantidadTotalRegistros { get; set; }
        public ResponseUsuarioObtenerDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
            CantidadTotalRegistros = 0;
        }
    }
}
