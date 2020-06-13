namespace Entidad.Dto.Maestro
{
    public class CategoriaRegistrarDto
    {
        public string Descripcion { get; set; }
        public byte[] Archivo { get; set; }
        public string UrlImagen { get; set; }
        public string ExtensionSinPunto { get; set; }
    }
}
