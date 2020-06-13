﻿using Entidad.Dto.Global;
using System.Collections.Generic;
using System.ComponentModel;

namespace Entidad.Dto.Response.Maestro
{
    public class TipoEstadoResponseModificarDto
    {
        public int ProcesadoOk { get; set; }
        [DisplayName("ListaErrores")]
        public List<ErrorDto> ListaError { get; set; }
        public TipoEstadoResponseModificarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
