using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Entidad.Request.Maestro
{
    public class RequestProductoDescuentoModificarDto
    {
        [Range(1, long.MaxValue, ErrorMessage = "{0}: debe tener un valor mayor o igual a {1}")]
        public long IdProductoDescuento { get; set; }

        [MaxLength(19, ErrorMessage = "{0}: longitud máxima debe ser de {1} caracteres")]
        public string FechaInicio { get; set; }

        [MaxLength(19, ErrorMessage = "{0}: longitud máxima debe ser de {1} caracteres")]
        public string FechaFin { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "{0}: debe tener un valor mayor o igual a {1}")]
        public int IdTipoDescuento { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "{0}: debe tener un valor mayor o igual a {1}")]
        public decimal Valor { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "{0}: debe tener un valor mayor o igual a {1}")]
        public int IdEstado { get; set; }

        [JsonIgnore]
        public DateTime? FechaInicioDate { get; set; }
        [JsonIgnore]
        public DateTime? FechaFinDate { get; set; }
    }
}
