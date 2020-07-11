namespace Entidad.Dto.Correo
{
    public class RecuperacionContraseniaObtenerDto
    {
        public long IdRecuperacionContrasenia { get; set; }
        public long IdUsuario { get; set; }
        public string CorreoElectronico { get; set; }
        public string CodigoGenerado { get; set; }
        public string FechaRegistro { get; set; }
        public int MinutosVencimientoCodigo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
}
