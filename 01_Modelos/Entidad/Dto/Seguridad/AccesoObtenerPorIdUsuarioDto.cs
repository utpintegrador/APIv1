namespace Entidad.Dto.Seguridad
{
    public class AccesoObtenerPorIdUsuarioDto
    {
        public int IdAcceso { get; set; }
        public string Titulo { get; set; }
        public string UrlAcceso { get; set; }
        public string Icono { get; set; }
        public string EstiloDeGrupo { get; set; }
        public int IdAccesoPadre { get; set; }
        public int Orden { get; set; }
        

    }
}
