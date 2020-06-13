using System.Threading.Tasks;

namespace Negocio.Repositorio.Amazon
{
    public interface IS3Service
    {
        Task AgregarArchivo(string nombreBucket);
        Task ObtenerArchivo(string nombreBucket);
    }
}
