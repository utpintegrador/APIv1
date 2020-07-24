using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Entidad.Request.Grafico
{
    public class RequestGraficoObtenerResumenVentasDto
    {
        [Range(1, long.MaxValue, ErrorMessage = "{0}: el parametro es requerido")]
        public long IdUsuario { get; set; }
        public int CantidadMesesAtras { get; set; }
        [JsonIgnore]
        public DateTime Desde { get; set; }
        [JsonIgnore]
        public DateTime Hasta { get; set; }
    }
}
