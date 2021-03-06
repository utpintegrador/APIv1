﻿using Entidad.Dto.Correo;
using System.Collections.Generic;

namespace Entidad.Response.Correo
{
    public class ResponseRecuperacionContraseniaObtenerPorCodigoDto
    {
        public int ProcesadoOk { get; set; }
        public List<ErrorDto> ListaError { get; set; }
        public RecuperacionContraseniaObtenerPorCodigoDto Cuerpo { get; set; }

        public ResponseRecuperacionContraseniaObtenerPorCodigoDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
