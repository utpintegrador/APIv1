namespace Entidad.Dto.Seguridad
{
    public class UsuarioObtenerPorIdDto
    {
        public long IdUsuario { get; set; }
        public string CorreoElectronico { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int IdEstado { get; set; }
        public int IdTipoUsuario { get; set; }
        public string UrlImagen { get; set; }
    }
}
