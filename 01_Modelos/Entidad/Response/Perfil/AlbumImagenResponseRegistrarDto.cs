using Entidad.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Entidad.Response.Perfil
{
    public class AlbumImagenResponseRegistrarDto
    {
        public int ProcesadoOk { get; set; }
        [DisplayName("ListaErrores")]
        public List<ErrorDto> ListaError { get; set; }
        public long IdGenerado { get; set; }
        public AlbumImagenResponseRegistrarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
            IdGenerado = 0;
        }
    }
}
