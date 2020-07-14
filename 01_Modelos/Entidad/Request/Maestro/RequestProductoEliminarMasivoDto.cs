using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entidad.Request.Maestro
{
    public class RequestProductoEliminarMasivoDto
    {

        [MinLength(1, ErrorMessage = "{0}: se requiere un registro como mínimo")]
        public List<ObjetoProducto> ListaIdProducto { get; set; }
        public RequestProductoEliminarMasivoDto()
        {
            ListaIdProducto = new List<ObjetoProducto>();
        }
    }

    public class ObjetoProducto
    {
        public long IdProducto { get; set; }
    }
}
