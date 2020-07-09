using Datos.Repositorio.Seguridad;
using Entidad.Dto.Seguridad;
using Entidad.Request.Seguridad;
using System.Collections.Generic;

namespace Negocio.Repositorio.Seguridad
{
    public class LnAcceso
    {
        private readonly AdAcceso _adAcceso = new AdAcceso();
        public List<AccesoObtenerDto> Obtener(RequestAccesoObtenerDto filtro)
        {
            if (filtro == null) filtro = new RequestAccesoObtenerDto();
            if (filtro.NumeroPagina == 0) filtro.NumeroPagina = 1;
            if (filtro.CantidadRegistros == 0) filtro.CantidadRegistros = 10;
            if (string.IsNullOrEmpty(filtro.ColumnaOrden)) filtro.ColumnaOrden = "IdAcceso";
            if (string.IsNullOrEmpty(filtro.DireccionOrden)) filtro.DireccionOrden = "desc";

            var listado = _adAcceso.Obtener(filtro);
            if (listado == null)
            {
                listado = new List<AccesoObtenerDto>();
            }
            return listado;
        }

        public AccesoObtenerPorIdDto ObtenerPorId(int id)
        {
            return _adAcceso.ObtenerPorId(id);
        }

        public int Registrar(RequestAccesoRegistrarDto modelo, ref int idNuevo)
        {
            return _adAcceso.Registrar(modelo, ref idNuevo);
        }

        public int Modificar(RequestAccesoModificarDto modelo)
        {
            return _adAcceso.Modificar(modelo);
        }

        public int Eliminar(int id)
        {
            return _adAcceso.Eliminar(id);
        }
    }
}
