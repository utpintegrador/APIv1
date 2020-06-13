﻿using Entidad.Dto.Global;
using Entidad.Entidad.Seguridad;
using System.Collections.Generic;
using System.ComponentModel;

namespace Entidad.Dto.Response.Seguridad
{
    public class UsuarioResponseObtenerPorIdDto
    {
        public int ProcesadoOk { get; set; }
        [DisplayName("ListaErrores")]
        public List<ErrorDto> ListaError { get; set; }
        public Usuario Cuerpo { get; set; }
        public UsuarioResponseObtenerPorIdDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
