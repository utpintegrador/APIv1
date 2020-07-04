using System;
namespace Entidad.Entidad.Maestro
{
    public class NegocioEnt
    {
        public int IdNegocio { get; set; }
        public string DocumentoIdentificacion { get; set; }
        public string Nombre { get; set; }
        public string Resenia { get; set; }
        public string IdTipoDocumentoIdentificacion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int IdUsuario { get; set; }
    }
}
