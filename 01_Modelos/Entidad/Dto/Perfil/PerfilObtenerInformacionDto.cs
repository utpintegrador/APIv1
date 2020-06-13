using System;

namespace Entidad.Dto.Perfil
{
    public class PerfilObtenerInformacionDto
    {
        public long IdPerfil { get; set; }
        public string Biografia { get; set; }
        public int IdGenero { get; set; }
        public string DescripcionGenero { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public int IdPaisResidencia { get; set; }
        public DateTime DescripcionPaisResidencia { get; set; }

    }
}
