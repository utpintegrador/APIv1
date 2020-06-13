namespace Entidad.Dto.Interaccion
{
    public class MatchRegistrarDto
    {
        public long IdUsuarioEmisor { get; set; }
        public int IdEstadoValoracionUsuarioEmisor { get; set; }
        public long IdUsuarioMatch { get; set; }
        public int IdEstadoValoracionUsuarioMatch { get; set; }
    }
}
