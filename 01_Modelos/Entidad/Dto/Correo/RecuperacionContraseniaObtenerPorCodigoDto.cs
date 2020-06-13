namespace Entidad.Dto.Correo
{
    public class RecuperacionContraseniaObtenerPorCodigoDto
    {
        public long IdUsuario { get; set; }
        public string CorreoElectronico { get; set; }
        public string CodigoGenerado { get; set; }
    }
}
