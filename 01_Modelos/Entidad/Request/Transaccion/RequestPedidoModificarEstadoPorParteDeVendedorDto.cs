using System.ComponentModel.DataAnnotations;

namespace Entidad.Request.Transaccion
{
    public class RequestPedidoModificarEstadoPorParteDeVendedorDto
    {
        [Range(1, long.MaxValue, ErrorMessage = "{0}: parametro es requerido")]
        public long IdPedido { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "{0}: parametro es requerido")]
        public long IdNegocioVendedor { get; set; }

        public int IdEstado { get; set; }

        /*
        IdEstado    Descripcion IdTipoEstado
        7	        Generado	3
        8	        Aceptado	3 *
        9	        Preparando	3 *
        10	        Entregando	3 *
        11	        Entregado	3 *
        12	        Cancelado	3
        13	        Rechazado	3 *
        */
    }
}
