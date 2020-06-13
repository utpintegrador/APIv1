namespace Entidad.Dto.Perfil
{
    public class PerfilObtenerDatoLaboralDto
    {
        public long IdPerfil { get; set; }
        public int IdEstadoOcupacional { get; set; }
        public string DescripcionEstadoOcupacional { get; set; }
        public string NombreInstitucionLaboral { get; set; }
        public string DescripcionCargoLaboral { get; set; }
    }
}
