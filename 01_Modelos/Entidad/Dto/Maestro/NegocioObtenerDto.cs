using System.Runtime.Serialization;

namespace Entidad.Dto.Maestro
{
    public class NegocioObtenerDto
    {
        public long IdNegocio { get; set; }
        public string DocumentoIdentificacion { get; set; }
        public string Nombre { get; set; }
        public string Resenia { get; set; }
        public string DescripcionTipoDocumentoIdentificacion { get; set; }
        public string DescripcionEstado { get; set; }
        public string FechaRegistro { get; set; }
        public string CorreoElectronico { get; set; }
        [IgnoreDataMember]
        public long TotalItems { get; set; }

    }
}
