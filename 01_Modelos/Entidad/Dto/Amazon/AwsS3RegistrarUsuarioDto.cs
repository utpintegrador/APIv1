namespace Entidad.Dto.Amazon
{
    public class AwsS3RegistrarUsuarioDto
    {
        public byte[] Archivo { get; set; }
        public string ExtensionSinPunto { get; set; }
        public long IdUsuario { get; set; }
    }
}
