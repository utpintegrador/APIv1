﻿using System.ComponentModel.DataAnnotations;

namespace Entidad.Dto.Maestro
{
    public class MonedaModificarPrmDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "{0}: debe tener un valor mayor o igual a {1}")]
        public int IdMoneda { get; set; }

        [Required(ErrorMessage = "{0}: parametro es requerido")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "{0}: longitud debe estar entre {2} y {1} caracteres")]
        public string Descripcion { get; set; }
    }
}
