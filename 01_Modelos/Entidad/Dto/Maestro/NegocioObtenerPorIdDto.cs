namespace Entidad.Dto.Maestro
{
    public class NegocioObtenerPorIdDto
    {
        public long IdNegocio { get; set; }
        public string DocumentoIdentificacion { get; set; }
        public string Nombre { get; set; }
        public string Resenia { get; set; }
        public int IdTipoDocumentoIdentificacion { get; set; }
        public long IdUsuario { get; set; }
        public int IdEstado { get; set; }
        public decimal Longitud { get; set; }
        public decimal Latitud { get; set; }
    }
}
