using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad.Dto.Maestro
{
    public class NegocioRegistrarDto
    {
        public string DocumentoIdentificacion { get; set; }
        public string Nombre { get; set; }
        public string Resenia { get; set; }
        public string IdTipoDocumentoIdentificacion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int IdUsuario { get; set; }

    }
}
