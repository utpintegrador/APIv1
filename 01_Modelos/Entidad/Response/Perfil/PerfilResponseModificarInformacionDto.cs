using Entidad.Response;
using System.Collections.Generic;
using System.ComponentModel;

namespace Entidad.Response.Perfil
{
    public class PerfilResponseModificarInformacionDto
    {
        public int ProcesadoOk { get; set; }
        [DisplayName("ListaErrores")]
        public List<ErrorDto> ListaError { get; set; }
        public PerfilResponseModificarInformacionDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
