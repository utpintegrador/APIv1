using Datos.Repositorio.Maestro;
using Entidad.Dto.Maestro;
using Entidad.Request.Maestro;
using System.Collections.Generic;

namespace Negocio.Repositorio.Maestro
{
    public class LnNegocioUbicacion
    {
        private readonly AdNegocioUbicacion _adNegocioUbicacion = new AdNegocioUbicacion();

        //Obtener Ubicacion de Negocio
        public List<NegocioUbicacionObtenerPorIdNegocioDto> ObtenerPorIdNegocio(RequestNegocioUbicacionObtenerPorIdNegocioDto filtro)
        {
            if (filtro == null) filtro = new RequestNegocioUbicacionObtenerPorIdNegocioDto();
            if (filtro.NumeroPagina == 0) filtro.NumeroPagina = 1;
            if (filtro.CantidadRegistros == 0) filtro.CantidadRegistros = 10;
            if (string.IsNullOrEmpty(filtro.ColumnaOrden)) filtro.ColumnaOrden = "IdNegocioUbicacion";
            if (string.IsNullOrEmpty(filtro.DireccionOrden)) filtro.DireccionOrden = "desc";

            var lista = _adNegocioUbicacion.ObtenerPorIdNegocio(filtro);
            if(lista == null)
            {
                lista = new List<NegocioUbicacionObtenerPorIdNegocioDto>();
            }
            return lista;
        }

        public List<NegocioUbicacionObtenerPorIdUsuarioDto> ObtenerPorIdUsuario(RequestNegocioUbicacionObtenerPorIdUsuarioDto filtro)
        {
            if (filtro == null) filtro = new RequestNegocioUbicacionObtenerPorIdUsuarioDto();
            if (filtro.NumeroPagina == 0) filtro.NumeroPagina = 1;
            if (filtro.CantidadRegistros == 0) filtro.CantidadRegistros = 10;
            if (string.IsNullOrEmpty(filtro.ColumnaOrden)) filtro.ColumnaOrden = "IdNegocioUbicacion";
            if (string.IsNullOrEmpty(filtro.DireccionOrden)) filtro.DireccionOrden = "desc";

            var lista = _adNegocioUbicacion.ObtenerPorIdUsuario(filtro);
            if (lista == null)
            {
                lista = new List<NegocioUbicacionObtenerPorIdUsuarioDto>();
            }
            return lista;
        }

        //Obtener Ubicacion de Negocio por ID
        public NegocioUbicacionObtenerPorIdDto ObtenerPorId(long id)
        {
            return _adNegocioUbicacion.ObtenerPorId(id);
        }


        //Registar Ubicacion de Negocio
        public int Registrar(RequestNegocioUbicacionRegistrarDto modelo, ref long idNuevo)
        {
            return _adNegocioUbicacion.Registrar(modelo, ref idNuevo);
        }

        //Modificar Ubicacion de Negocio
        public int Modificar(RequestNegocioUbicacionModificarDto modelo)
        {
            return _adNegocioUbicacion.Modificar(modelo);
        }


        //Eliminar Ubicacion de Negocio
        public int Eliminar(long id)
        {
            return _adNegocioUbicacion.Eliminar(id);
        }
    }
}
