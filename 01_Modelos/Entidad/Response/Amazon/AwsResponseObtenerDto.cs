using Entidad.Dto.Amazon;
using Entidad.Response;
using System.Collections.Generic;
using System.ComponentModel;

namespace Entidad.Response.Amazon
{
    public class AwsResponseObtenerDto
    {
        public int ProcesadoOk { get; set; }
        [DisplayName("ListaErrores")]
        public List<ErrorDto> ListaError { get; set; }
        public List<AwsS3ObtenerDto> Cuerpo { get; set; }

        public AwsResponseObtenerDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
