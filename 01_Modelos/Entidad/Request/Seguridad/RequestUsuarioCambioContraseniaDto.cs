﻿using System.ComponentModel.DataAnnotations;

namespace Entidad.Request.Seguridad
{
    public class RequestUsuarioCambioContraseniaDto
    {
        [Range(1, long.MaxValue, ErrorMessage = "{0}: debe tener un valor mayor o igual a {1}")]
        public long IdUsuario { get; set; }

        [Required(ErrorMessage = "{0}: parametro es requerido")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "{0}: longitud debe estar entre {2} y {1} caracteres")]
        public string Contrasenia { get; set; }
    }
}
