﻿using Entidad.Dto.Paginacion;
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
        public RequestPedidoObtenerComprasPorIdUsuarioDto()
        {
            Buscar = string.Empty;
        }
    }
}