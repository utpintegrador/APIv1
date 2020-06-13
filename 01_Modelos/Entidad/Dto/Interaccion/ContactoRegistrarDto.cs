namespace Entidad.Dto.Interaccion
{
    public class ContactoRegistrarDto
    {
        public long IdUsuarioEmisor { get; set; }
        public long IdUsuarioObjetivo { get; set; }
        public int IdEstadoValoracionContacto { get; set; }
    }
}
