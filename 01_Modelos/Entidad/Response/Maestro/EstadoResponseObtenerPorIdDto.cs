﻿using Entidad.Entidad.Maestro;
using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class EstadoResponseObtenerPorIdDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public Estado Cuerpo { get; set; }
        public EstadoResponseObtenerPorIdDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
