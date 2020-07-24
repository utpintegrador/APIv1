using Newtonsoft.Json;

namespace Entidad.Dto.Maestro
{
    public class NegocioObtenerCercanosDto
    {
        public long IdNegocio { get; set; }
        public string DocumentoIdentificacion { get; set; }
        public string Nombre { get; set; }
        public string Resenia { get; set; }
        public string DescripcionTipoDocumentoIdentificacion { get; set; }
        public string DescripcionEstado { get; set; }
        public string FechaRegistro { get; set; }
        public string CorreoElectronico { get; set; }
        public decimal Longitud { get; set; }
        public decimal Latitud { get; set; }
        public string Telefono { get; set; }
        public double DistanciaKm { get; set; }
        //[JsonIgnore]
        //public int TotalItems { get; set; }
    }
}
