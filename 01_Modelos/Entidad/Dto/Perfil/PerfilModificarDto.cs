using System;

namespace Entidad.Dto.Perfil
{
    public class PerfilModificarDto
    {
        public long IdPerfil { get; set; }
        public string FechaNacimiento { get; set; }
        public string Biografia { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int IdEstadoOcupacional { get; set; }
        public string NombreInstitucionLaboral { get; set; }
        public string DescripcionCargoLaboral { get; set; }
        public int IdGradoAcademico { get; set; }
        public string NombreCortoInstitucionAcademica { get; set; }
        public int IdCarrera { get; set; }
        public int IdPaisResidencia { get; set; }
        public long IdUsuario { get; set; }//es necesario?
        public int IdGenero { get; set; }
        public int IdEstadoSituacionSentimental { get; set; }
        public int IdInteresGenero { get; set; }
        public int IdInteresSentimental { get; set; }
    }
}
