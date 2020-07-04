namespace Entidad.Dto.Seguridad
{
    public class UsuarioModificarImagenMetodo1FiltroDto
    {
        public long IdUsuario { get; set; }
        public string ExtensionSinPunto { get; set; }
        //public string NombreArchivoConExtension { get; set; }
        public byte[] ArchivoBytes { get; set; }
    }
}
