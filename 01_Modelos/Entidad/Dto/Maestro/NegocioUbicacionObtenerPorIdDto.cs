namespace Entidad.Dto.Maestro
{
    public class NegocioUbicacionObtenerPorIdDto
    {
        public long IdNegocioUbicacion { get; set; }
        public long IdNegocio { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public bool Predeterminado { get; set; }
    }
}
