using Newtonsoft.Json;
using System.Collections.Generic;

namespace Entidad.Dto.Seguridad
{
    public class AccesoGrupoDto
    {
        [JsonIgnore]
        public int IdAcceso { get; set; }
        public int Orden { get; set; }
        public string Titulo { get; set; }
        public string UrlAcceso { get; set; }
        public string Icono { get; set; }
        public string EstiloDeGrupo { get; set; }
        public List<AccesoItemDto> ListaItem { get; set; }
    }
}
