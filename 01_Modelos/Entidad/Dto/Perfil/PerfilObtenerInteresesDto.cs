namespace Entidad.Dto.Perfil
{
    public class PerfilObtenerInteresesDto
    {
        public long IdPerfil { get; set; }
        public int IdEstadoSituacionSentimental { get; set; }
        public string DescripcionEstadoSituacionSentimental { get; set; }
        public int IdInteresGenero { get; set; }
        public string DescripcionInteresGenero { get; set; }
        public int IdInteresSentimental { get; set; }
        public string DescripcionInteresSentimental { get; set; }

    }
}
