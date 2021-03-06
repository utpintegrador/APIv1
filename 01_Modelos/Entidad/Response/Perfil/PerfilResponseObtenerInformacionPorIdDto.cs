﻿using Entidad.Response;
using Entidad.Dto.Perfil;
using System.Collections.Generic;
using System.ComponentModel;

namespace Entidad.Response.Perfil
{
    public class PerfilResponseObtenerInformacionPorIdDto
    {
        public int ProcesadoOk { get; set; }
        [DisplayName("ListaErrores")]
        public List<ErrorDto> ListaError { get; set; }
        public PerfilObtenerInformacionDto Cuerpo { get; set; }
        public PerfilResponseObtenerInformacionPorIdDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
