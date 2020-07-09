﻿using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseTipoBusquedaEliminarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public ResponseTipoBusquedaEliminarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
