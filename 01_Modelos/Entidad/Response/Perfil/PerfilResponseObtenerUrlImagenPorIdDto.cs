using Entidad.Response;
using Entidad.Dto.Seguridad;
using System.Collections.Generic;
using System.ComponentModel;

namespace Entidad.Response.Perfil
{
    public class PerfilResponseObtenerUrlImagenPorIdDto
    {
        public int ProcesadoOk { get; set; }
        [DisplayName("ListaErrores")]
        public List<ErrorDto> ListaError { get; set; }
        public UsuarioObtenerUrlImagenDto Cuerpo { get; set; }
        public PerfilResponseObtenerUrlImagenPorIdDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
