namespace Entidad.Dto.Seguridad
{
    public class UsuarioModificarDto
    {
        public long IdUsuario { get; set; }
        public string CorreoElectronico { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string UrlImagen { get; set; }

    }
}
