using Entidad.Response;
using Entidad.Dto.Perfil;
using System.Collections.Generic;
using System.ComponentModel;

namespace Entidad.Response.Perfil
{
    public class PerfilResponseObtenerPorIdUsuarioDto
    {
        public int ProcesadoOk { get; set; }
        [DisplayName("ListaErrores")]
        public List<ErrorDto> ListaError { get; set; }
        public PerfilObtenerDto Cuerpo { get; set; }
        public PerfilResponseObtenerPorIdUsuarioDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
