namespace Entidad.Response.Maestro
{
    public class CategoriaResponseRegistrarDto
    {
        public int ProcesadoOk { get; set; }
        //[DisplayName("ListaErrores")]
        //public List<ErrorDto> ListaError { get; set; }
        public int IdGenerado { get; set; }
        public string UrlImagen { get; set; }
        public CategoriaResponseRegistrarDto()
        {
            ProcesadoOk = 0;
            //ListaError = new List<ErrorDto>();
            IdGenerado = 0;
        }
    }
}
