namespace Entidad.Dto.Amazon
{
    public class AwsS3RegistrarAlbumDto
    {
        public string ImagenStringBase64 { get; set; }
        public string ExtensionSinPunto { get; set; }
        public long IdUsuario { get; set; }
        public long IdAlbum { get; set; }
    }
}
