﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entidad.Request.Transaccion
{
    public class RequestPedidoModificarDto
    {
        [Range(1, long.MaxValue, ErrorMessage = "{0}: debe tener un valor mayor o igual a {1}")]
        public long IdPedido { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "{0}: debe tener un valor mayor o igual a {1}")]
        public long IdNegocioVendedor { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "{0}: debe tener un valor mayor o igual a {1}")]
        public long IdNegocioComprador { get; set; }

        [MaxLength(500, ErrorMessage = "{0}: longitud máxima debe ser de {1} caracter(es)")]
        public string Direccion { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "{0}: debe tener un valor mayor o igual a {1}")]
        public int IdMoneda { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "{0}: debe tener un valor mayor o igual a {1}")]
        public long IdUsuarioRegistro { get; set; }

        [Required(ErrorMessage = "{0}: parametro es requerido")]
        [StringLength(20, MinimumLength = 7, ErrorMessage = "{0}: longitud debe estar entre {2} y {1} caracteres")]
        public string NumeroCelular { get; set; }


        [MaxLength(500, ErrorMessage = "{0}: longitud máxima es de {1} caracter(es)")]
        public string Observaciones { get; set; }

        [Required(ErrorMessage = "{0}: parametro es requerido")]
        [MinLength(1, ErrorMessage = "{0}: se requiere {1} elemento(s) como mínimo")]
        public List<RequestPedidoModificarPedidoDetalleModificarDto> ListaPedidoDetalle { get; set; }

        public RequestPedidoModificarDto()
        {
            ListaPedidoDetalle = new List<RequestPedidoModificarPedidoDetalleModificarDto>();
        }
    }
}
