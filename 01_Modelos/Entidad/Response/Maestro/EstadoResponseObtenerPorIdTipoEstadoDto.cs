﻿using Entidad.Response;
using Entidad.Dto.Maestro;
using System.Collections.Generic;
using System.ComponentModel;

namespace Entidad.Response.Maestro
{
    public class EstadoResponseObtenerPorIdTipoEstadoDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public List<EstadoObtenerPorIdTipoEstadoDto> Cuerpo { get; set; }
        public EstadoResponseObtenerPorIdTipoEstadoDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
