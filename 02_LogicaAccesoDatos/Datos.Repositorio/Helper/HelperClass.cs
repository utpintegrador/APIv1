using Entidad.Configuracion.Proceso;
using System.Data;
using System.Data.SqlClient;

namespace Datos.Helper
{
    internal class HelperClass
    {

        internal static IDbConnection ObtenerConeccion()
        {

            return new SqlConnection(ConfiguracionJson.Conf.Cn);

        }

    }
}
