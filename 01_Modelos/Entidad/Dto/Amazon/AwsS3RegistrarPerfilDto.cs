namespace Entidad.Dto.Amazon
{
    public class AwsS3RegistrarPerfilDto
    {
        public string ImagenStringBase64 { get; set; }
        public string ExtensionSinPunto { get; set; }
        public long IdUsuario { get; set; }
        public long IdPerfil { get; set; }
    }
}
