namespace Entidad.Dto.Interaccion
{
    public class ContactoObtenerPendienteMatchDto
    {
        public long IdContacto { get; set; }
        public long IdUsuario1 { get; set; }
        public int IdValoracionDelUsuario1AlUsuario2 { get; set; }
        public long IdUsuario2 { get; set; }
        public int IdValoracionDelUsuario2AlUsuario1 { get; set; }
    }
}
