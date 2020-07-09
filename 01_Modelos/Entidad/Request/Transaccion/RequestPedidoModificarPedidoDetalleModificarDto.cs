using System.ComponentModel.DataAnnotations;

namespace Entidad.Request.Transaccion
{
    public class RequestPedidoModificarPedidoDetalleModificarDto
    {
        public long IdPedidoDetalle { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "{0}: parametro es requerido")]
        public long IdProducto { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "{0}: parametro es requerido")]
        public decimal Cantidad { get; set; }

        [Range(0.001, double.MaxValue, ErrorMessage = "{0}: parametro es requerido")]
        public decimal PrecioUnitario { get; set; }

        [Required(ErrorMessage = "{0}: parametro es requerido")]
        [StringLength(4, MinimumLength = 3, ErrorMessage = "{0}: longitud debe estar entre {2} y {1} caracteres")]
        public string Accion { get; set; }
    }
}
