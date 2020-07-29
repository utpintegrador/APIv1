using Entidad.Dto.Paginacion;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Entidad.Request.Transaccion
{
    public class RequestPedidoObtenerComprasPorIdUsuarioDto : FiltroPaginacion
    {
        [Range(1, long.MaxValue, ErrorMessage = "{0}: debe tener un valor mayor o igual a {1}")]
        public long IdUsuario { get; set; }
        public long IdNegocioComprador { get; set; }
        public string Buscar { get; set; }
        public int IdEstado { get; set; }
        public int IdMoneda { get; set; }
        [Required(ErrorMessage = "{0}: parametro es requerido")]
        [MaxLength(10, ErrorMessage = "{0}: se requiere valor con el formato yyyy/MM/dd {1} caracteres")]
        public string FechaDesde { get; set; }

        [Required(ErrorMessage = "{0}: parametro es requerido")]
        [MaxLength(10, ErrorMessage = "{0}: se requiere valor con el formato yyyy/MM/dd {1} caracteres")]
        public string FechaHasta { get; set; }

        [JsonIgnore]
        public DateTime FechaDesdeDate { get; set; }
        [JsonIgnore]
        public DateTime FechaHastaDate { get; set; }
        public RequestPedidoObtenerComprasPorIdUsuarioDto()
        {
            Buscar = string.Empty;
        }
    }
}
