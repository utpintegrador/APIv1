using System;

namespace Entidad.Dto.Perfil
{
    public class PerfilModificarInformacionDto
    {
        public long IdPerfil { get; set; }
        public string Biografia { get; set; }
        public int IdGenero { get; set; }
        public string FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int IdPaisResidencia { get; set; }
    }
}
