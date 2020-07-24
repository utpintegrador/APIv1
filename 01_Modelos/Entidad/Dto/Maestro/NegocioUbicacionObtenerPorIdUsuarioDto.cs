using Newtonsoft.Json;

namespace Entidad.Dto.Maestro
{
    public class NegocioUbicacionObtenerPorIdUsuarioDto
    {
        public long IdNegocioUbicacion { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public bool Predeterminado { get; set; }
        public string NombreNegocio { get; set; }
        [JsonIgnore]
        public int TotalItems { get; set; }
    }
}
