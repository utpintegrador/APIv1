using Datos.Repositorio.Maestro;
using Entidad.Dto.Maestro;
using Entidad.Entidad.Maestro;
using System.Collections.Generic;

namespace Negocio.Repositorio.Maestro
{
    public class LnNegocio
    {
        private readonly AdNegocio _adNegocio = new AdNegocio();
        public List<NegocioObtenerDto> Obtener(NegocioObtenerFiltroDto filtro)
        {
            if (filtro == null) filtro = new NegocioObtenerFiltroDto();
            if (filtro.NumeroPagina == 0) filtro.NumeroPagina = 1;
            if (filtro.CantidadRegistros == 0) filtro.CantidadRegistros = 10;
            if (string.IsNullOrEmpty(filtro.ColumnaOrden)) filtro.ColumnaOrden = "IdNegocio";
            if (string.IsNullOrEmpty(filtro.DireccionOrden)) filtro.DireccionOrden = "desc";
            if (filtro.IdUsuario == null) filtro.IdUsuario = 0;

            var listado = _adNegocio.Obtener(filtro);
            if (listado == null)
            {
                listado = new List<NegocioObtenerDto>();
            }
            return listado;
        }

        public List<NegocioObtenerCercanosDto> ObtenerCercanos(NegocioObtenerCercanosFiltroDto filtro)
        {
            if (filtro == null) filtro = new NegocioObtenerCercanosFiltroDto();
            if (filtro.NumeroPagina == 0) filtro.NumeroPagina = 1;
            if (filtro.CantidadRegistros == 0) filtro.CantidadRegistros = 10;
            if (string.IsNullOrEmpty(filtro.ColumnaOrden)) filtro.ColumnaOrden = "IdNegocio";
            if (string.IsNullOrEmpty(filtro.DireccionOrden)) filtro.DireccionOrden = "desc";
            if (filtro.CantidadKilometros == 0) filtro.CantidadKilometros = 1;

            var listado = _adNegocio.ObtenerCercanos(filtro);
            if(listado == null)
            {
                listado = new List<NegocioObtenerCercanosDto>();
            }
            return listado;
        }

        public NegocioObtenerPorIdDto ObtenerPorId(long id)
        {
            return _adNegocio.ObtenerPorId(id);
        }

        public int Registrar(NegocioRegistrarDto modelo, ref long idNuevo)
        {
            return _adNegocio.Registrar(modelo, ref idNuevo);
        }

        public int Modificar(NegocioModificarDto modelo)
        {
            return _adNegocio.Modificar(modelo);
        }

        public int Eliminar(int id)
        {
            return _adNegocio.Eliminar(id);
        }

    }
}
