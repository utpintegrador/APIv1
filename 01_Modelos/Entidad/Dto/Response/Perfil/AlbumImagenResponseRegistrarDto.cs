using Entidad.Dto.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Entidad.Dto.Response.Perfil
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
