using Newtonsoft.Json;
using System;

namespace Entidad.Dto.Perfil
{
    public class PerfilObtenerInformacionCompletaDto
    {

        public long IdPerfil { get; set; }
        public long IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }
        [JsonIgnore]
        public DateTime? FechaNacimientoFormatoDate { get; set; }
        public string FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public string Biografia { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int IdEstadoOcupacional { get; set; }
        public string DescripcionEstadoOcupacional { get; set; }
        public string NombreInstitucionLaboral { get; set; }
        public string DescripcionCargoLaboral { get; set; }
        public int IdGradoAcademico { get; set; }
        public string DescripcionGradoAcademico { get; set; }
        public string NombreCortoInstitucionAcademica { get; set; }
        public int IdCarrera { get; set; }
        public string DescripcionCarrera { get; set; }
        public int IdPaisResidencia { get; set; }
        public string DescripcionPais { get; set; }
        public int IdGenero { get; set; }
        public string DescripcionGenero { get; set; }
        public int IdEstadoSituacionSentimental { get; set; }
        public string DescripcionEstadoSituacionSentimental { get; set; }
        public int IdInteresGenero { get; set; }
        public string DescripcionInteresGenero { get; set; }
        public int IdInteresSentimental { get; set; }
        public string DescripcionInteresSentimental { get; set; }

        public PerfilObtenerInformacionCompletaDto()
        {
            FechaNacimiento = string.Empty;
        }

    }
}
