namespace Entidad.Dto.Perfil
{
    public class PerfilObtenerPasarelaDto
    {
        public long IdPerfil { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Biografia { get; set; }
        public string NombreInstitucionLaboral { get; set; }
        public string DescripcionCargoLaboral { get; set; }
        public string DescripcionGradoAcademico { get; set; }
        public string NombreCortoInstitucionAcademica { get; set; }
        public string DescripcionCarrera { get; set; }
        public string DescripcionGenero { get; set; }
        public string DescripcionEstadoSentimental { get; set; }
        public string DescripcionInteresSentimental { get; set; }
        public string UrlImagen { get; set; }

    }
}
