namespace Entidad.Dto.Perfil
{
    public class PerfilObtenerDatoAcademicoDto
    {
        public long IdPerfil { get; set; }
        public int IdGradoAcademico { get; set; }
        public string DescripcionGradoAcademico { get; set; }
        public int IdCarrera { get; set; }
        public string DescripcionCarrera { get; set; }
        public string NombreCortoInstitucionAcademica { get; set; }
    }
}
