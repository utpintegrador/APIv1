﻿using Entidad.Dto.Global;
using System.Collections.Generic;
using System.ComponentModel;

namespace Entidad.Dto.Response.Perfil
{
    public class PerfilResponseRegistrarDto
    {
        public int ProcesadoOk { get; set; }
        [DisplayName("ListaErrores")]
        public List<ErrorDto> ListaError { get; set; }
        public long IdGenerado { get; set; }
        public PerfilResponseRegistrarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
            IdGenerado = 0;
        }
    }
}
