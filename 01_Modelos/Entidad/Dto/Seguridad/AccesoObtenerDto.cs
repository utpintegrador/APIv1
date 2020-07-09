using Newtonsoft.Json;

namespace Entidad.Dto.Seguridad
{
    public class AccesoObtenerDto
    {
        public int IdAcceso { get; set; }
        public string Titulo { get; set; }
		public string UrlAcceso { get; set; }
        public string Icono { get; set; }
        public int? IdAccesoPadre { get; set; }
        [JsonIgnore]
        public int TotalItems { get; set; }
    }
}
