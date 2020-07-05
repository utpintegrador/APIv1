namespace Entidad.Dto.Maestro
{
    public class CategoriaModificarImagenMetodo1FiltroDto
    {
        public long IdCategoria { get; set; }
        public string ExtensionSinPunto { get; set; }
        //public string NombreArchivoConExtension { get; set; }
        public byte[] ArchivoBytes { get; set; }
    }
}
