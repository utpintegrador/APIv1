using Entidad.Response;
using Entidad.Dto.Perfil;
using System.Collections.Generic;
using System.ComponentModel;

namespace Entidad.Response.Perfil
{
    public class ImagenResponseObtenerDto
    {
        public int ProcesadoOk { get; set; }
        [DisplayName("ListaErrores")]
        public List<ErrorDto> ListaError { get; set; }
        public List<ImagenObtenerDto> Cuerpo { get; set; }
        public ImagenResponseObtenerDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
