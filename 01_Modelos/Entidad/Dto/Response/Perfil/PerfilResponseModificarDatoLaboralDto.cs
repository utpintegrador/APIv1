﻿using Entidad.Dto.Global;
using System.Collections.Generic;
using System.ComponentModel;

namespace Entidad.Dto.Response.Perfil
{
    public class PerfilResponseModificarDatoLaboralDto
    {
        public int ProcesadoOk { get; set; }
        [DisplayName("ListaErrores")]
        public List<ErrorDto> ListaError { get; set; }
        public PerfilResponseModificarDatoLaboralDto()
        {
            ProcesadoOk = 0;
            ListaError = new List<ErrorDto>();
        }
    }
}
