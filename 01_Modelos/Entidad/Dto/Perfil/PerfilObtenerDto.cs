using System;

namespace Entidad.Dto.Perfil
{
    public class PerfilObtenerDto
    {

        public long IdPerfil { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Biografia { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string DescripcionEstadoOcupacional { get; set; }
        public string NombreInstitucionLaboral { get; set; }
        public string DescripcionCargoLaboral { get; set; }
        public string DescripcionGradoAcademico { get; set; }
        public string NombreCortoInstitucionAcademica { get; set; }
        public string DescripcionCarrera { get; set; }
        public string DescripcionPaisResidencia { get; set; }
        public string DescripcionGenero { get; set; }
        public string DescripcionEstadoSituacionSentimental { get; set; }
        public string DescripcionInteresGenero { get; set; }
        public string DescripcionInteresSentimental { get; set; }
        //public string UrlImagen { get; set; }

    }
}
