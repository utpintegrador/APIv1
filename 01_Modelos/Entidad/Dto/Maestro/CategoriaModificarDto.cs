using System;
using System.Collections.Generic;
using System.Text;

namespace Entidad.Dto.Maestro
{
    public class CategoriaModificarDto
    {
        public int IdCategoria { get; set; }
        public string Descripcion { get; set; }
        /// <summary>
        /// 1: Activo       2: Inactivo
        /// </summary>
        public int IdEstado { get; set; }
    }
}
