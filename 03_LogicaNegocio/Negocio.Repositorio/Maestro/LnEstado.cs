using Datos.Repositorio.Maestro;
using Entidad.Dto.Maestro;
using Entidad.Request.Maestro;
using System.Collections.Generic;

namespace Negocio.Repositorio.Maestro
{
    public class LnEstado
    {
        private readonly AdEstado _adEstado = new AdEstado();

        //Obtener Estado
        public List<EstadoObtenerDto> Obtener(RequestEstadoObtenerDto filtro)
        {
            if (filtro == null) filtro = new RequestEstadoObtenerDto();
            if (filtro.NumeroPagina == 0) filtro.NumeroPagina = 1;
            if (filtro.CantidadRegistros == 0) filtro.CantidadRegistros = 10;
            if (string.IsNullOrEmpty(filtro.ColumnaOrden)) filtro.ColumnaOrden = "IdEstado";
            if (string.IsNullOrEmpty(filtro.DireccionOrden)) filtro.DireccionOrden = "desc";

            var listado = _adEstado.Obtener(filtro);
            if (listado == null)
            {
                listado = new List<EstadoObtenerDto>();
            }
            return listado;
            
        }
        
        //Obtener Estado por ID
        public EstadoObtenerPorIdDto ObtenerPorId(int id)
        {
            return _adEstado.ObtenerPorId(id);
        }

        //Registar Estado
        public int Registrar(RequestEstadoRegistrarDto modelo, ref int idNuevo)
        {
            return _adEstado.Registrar(modelo, ref idNuevo);
        }

        //Modificar Estado
        public int Modificar(RequestEstadoModificarDto modelo)
        {
            return _adEstado.Modificar(modelo);
        }

        //Eliminar Estado
        public int Eliminar(int id)
        {
            return _adEstado.Eliminar(id);
        }

        //Lstar Estados
        public List<EstadoObtenerComboDto> ObtenerCombo(int idTipoEstado)
        {
            var listado = _adEstado.ObtenerCombo(idTipoEstado);
            if (listado == null)
            {
                listado = new List<EstadoObtenerComboDto>();
            }
            return listado;
        }

    }
}
