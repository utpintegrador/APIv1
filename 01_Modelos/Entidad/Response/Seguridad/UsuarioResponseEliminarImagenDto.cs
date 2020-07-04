﻿using System.Collections.Generic;
using System.ComponentModel;

namespace Entidad.Response.Seguridad
{
    public class UsuarioResponseEliminarImagenDto
    {
        public int ProcesadoOk { get; set; }
        [DisplayName("ListaErrores")]
        public List<ErrorDto> ListaError { get; set; }
        public string UrlImagen { get; set; }
        public UsuarioResponseEliminarImagenDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
            UrlImagen = string.Empty;
        }
    }
}