﻿using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseProductoModificarDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public ResponseProductoModificarDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
