using System.Collections.Generic;

namespace Entidad.Response.Seguridad
{
    public class UsuarioResponseSubirImagenDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public string UrlImagen { get; set; }
        public UsuarioResponseSubirImagenDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
            UrlImagen = string.Empty;
        }
    }
}
