using Entidad.Response;
using Entidad.Entidad.Perfil;
using System.Collections.Generic;
using System.ComponentModel;

namespace Entidad.Response.Perfil
{
    public class ImagenResponseObtenerPorIdDto
    {
        public int ProcesadoOk { get; set; }
        [DisplayName("ListaErrores")]
        public List<ErrorDto> ListaError { get; set; }
        public Imagen Cuerpo { get; set; }
        public ImagenResponseObtenerPorIdDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
