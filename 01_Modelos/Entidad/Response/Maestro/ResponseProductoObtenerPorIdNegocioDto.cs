﻿using Entidad.Dto.Maestro;
using System.Collections.Generic;

namespace Entidad.Response.Maestro
{
    public class ResponseProductoObtenerPorIdNegocioDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public List<ProductoObtenerPorIdNegocioDto> Cuerpo { get; set; }
        public long CantidadTotalRegistros { get; set; }
        public ResponseProductoObtenerPorIdNegocioDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
            CantidadTotalRegistros = 0;
        }
    }
}