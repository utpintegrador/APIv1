using Datos.Repositorio.Maestro;
using Entidad.Dto.Maestro;
using Entidad.Request.Maestro;
using System.Collections.Generic;

namespace Negocio.Repositorio.Maestro
{
    public class LnTipoDocumentoIdentificacion
    {
        private readonly AdTipoDocumentoIdentificacion _adTipoDocumentoIdentificacion = new AdTipoDocumentoIdentificacion();

        //Documento de Identidad
        public List<TipoDocumentoIdentificacionObtenerDto> Obtener(RequestTipoDocumentoIdentificacionObtenerDto filtro)
        {
            if (filtro == null) filtro = new RequestTipoDocumentoIdentificacionObtenerDto();
            if (filtro.NumeroPagina == 0) filtro.NumeroPagina = 1;
            if (filtro.CantidadRegistros == 0) filtro.CantidadRegistros = 10;
            if (string.IsNullOrEmpty(filtro.ColumnaOrden)) filtro.ColumnaOrden = "IdTipoDocumentoIdentificacion";
            if (string.IsNullOrEmpty(filtro.DireccionOrden)) filtro.DireccionOrden = "desc";

            var listado = _adTipoDocumentoIdentificacion.Obtener(filtro);
            if (listado == null)
            {
                listado = new List<TipoDocumentoIdentificacionObtenerDto>();
            }
            return listado;
        }

        //Obtener Documento de Identidad
        public List<TipoDocumentoIdentificacionObtenerComboDto> ObtenerCombo()
        {
            var listado = _adTipoDocumentoIdentificacion.ObtenerCombo();
            if (listado == null)
            {
                listado = new List<TipoDocumentoIdentificacionObtenerComboDto>();
            }
            return listado;
        }

        //Obtener Documento de Identidad por ID
        public TipoDocumentoIdentificacionObtenerPorIdDto ObtenerPorId(int id)
        {
            return _adTipoDocumentoIdentificacion.ObtenerPorId(id);
        }

        //Registrar Documento de Identidad
        public int Registrar(RequestTipoDocumentoIdentificacionRegistrarDto modelo, ref int idNuevo)
        {
            return _adTipoDocumentoIdentificacion.Registrar(modelo, ref idNuevo);
        }

        //Modificar Documento de Identidad
        public int Modificar(RequestTipoDocumentoIdentificacionModificarDto modelo)
        {
            return _adTipoDocumentoIdentificacion.Modificar(modelo);
        }

        //Eliminar Documento de Identidad
        public int Eliminar(int id)
        {
            return _adTipoDocumentoIdentificacion.Eliminar(id);
        }
    }
}
