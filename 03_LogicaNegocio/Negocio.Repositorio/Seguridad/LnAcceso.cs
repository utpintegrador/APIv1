using Datos.Repositorio.Seguridad;
using Entidad.Dto.Seguridad;
using Entidad.Request.Seguridad;
using System.Collections.Generic;
using System.Linq;

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

        public AccesoRootDto ObtenerPorIdUsuario(long idUsuario)
        {
            var listado = _adAcceso.ObtenerPorIdUsuario(idUsuario);
            return ProcesarAccesosPorIdUsuario(listado);
        }

        private AccesoRootDto ProcesarAccesosPorIdUsuario(List<AccesoObtenerPorIdUsuarioDto> listaAcceso)
        {
            //Todos con IdAccesoPadre cero son los grupos
            var listadoGrupos = listaAcceso.Where(x => x.IdAccesoPadre == 0).ToList();
            List<AccesoGrupoDto> grupos = new List<AccesoGrupoDto>();
            AccesoGrupoDto grupo;
            foreach (var itemGrupo in listadoGrupos)
            {
                grupo = new AccesoGrupoDto
                {
                     IdAcceso = itemGrupo.IdAcceso,
                     Titulo = itemGrupo.Titulo,
                     UrlAcceso = itemGrupo.UrlAcceso,
                     Icono = itemGrupo.Icono,
                     Orden = itemGrupo.Orden,
                     EstiloDeGrupo = itemGrupo.EstiloDeGrupo
                };
                grupos.Add(grupo);
            }

            foreach (var itemAgrupado in grupos)
            {
                List<AccesoItemDto> listaItems = (from tab in listaAcceso
                                                  where tab.IdAccesoPadre == itemAgrupado.IdAcceso
                                                  select new AccesoItemDto
                                                  {
                                                      Titulo = tab.Titulo,
                                                      UrlAcceso = tab.UrlAcceso,
                                                      Icono = tab.Icono,
                                                      Orden = tab.Orden
                                                  }).ToList();
                itemAgrupado.ListaItem = new List<AccesoItemDto>();
                itemAgrupado.ListaItem.AddRange(listaItems);
            }

            AccesoRootDto root = new AccesoRootDto();
            root.ListaGrupo = new List<AccesoGrupoDto>();
            root.ListaGrupo.AddRange(grupos);

            return root;
        }
    }
}



/*
 {
	"Grupos":[
		{
			"Orden": 1,
			"Titulo": "GESTION",
			"UrlAcceso": "#",
			"Icono": "mdi mdi-gauge",
			"Items":[
				{
					"Orden": 1,
					"Titulo": "Dashboard",
					"UrlAcceso": "/",
					"Icono": "fa fa-bar-chart"
				},
				{
					"Orden": 2,
					"Titulo": "Ventas",
					"UrlAcceso": "/Venta",
					"Icono": "fa fa-truck"
				},
				{
					"Orden": 3,
					"Titulo": "Compras",
					"UrlAcceso": "/Compra",
					"Icono": "fa fa-shopping-bag"
				}
			]
		},
		{
			"Orden": 2,
			"Titulo": "MAESTROS",
			"UrlAcceso": "#",
			"Icono": "mdi mdi-book-open-variant",
			"Items":[
				{
					"Orden": 1,
					"Titulo": "Negocio",
					"UrlAcceso": "/Negocio",
					"Icono": "fa fa-university"
				},
				{
					"Orden": 2,
					"Titulo": "Ubicaciones",
					"UrlAcceso": "/Ubicaciones",
					"Icono": "fa fa-map-marker"
				},
				{
					"Orden": 3,
					"Titulo": "Producto",
					"UrlAcceso": "/Producto",
					"Icono": "fa fa-shopping-bag"
				}
			]
		},
		{
			"Orden": 3,
			"Titulo": "ADMIN",
			"UrlAcceso": "#",
			"Icono": "fa fa-bla bla",
			"Items":[
				{
					"Orden": 1,
					"Titulo": "Categoria",
					"UrlAcceso": "/Categoria",
					"Icono": "fa fa-filter"
				},
				{
					"Orden": 2,
					"Titulo": "Usuario",
					"UrlAcceso": "/Usuario",
					"Icono": "fa fa-user"
				},
				{
					"Orden": 3,
					"Titulo": "Estado",
					"UrlAcceso": "/Estado",
					"Icono": "fa fa-check"
				},
				{
					"Orden": 4,
					"Titulo": "Tipo Estado",
					"UrlAcceso": "/TipoEstado",
					"Icono": "fa fa-list-ul"
				},
				{
					"Orden": 5,
					"Titulo": "Tipo Usuario",
					"UrlAcceso": "/TipoUsuario",
					"Icono": "fa fa-users"
				},
				{
					"Orden": 6,
					"Titulo": "Tipo Busqueda",
					"UrlAcceso": "/TipoBusqueda",
					"Icono": "fa fa-filter"
				},
				{
					"Orden": 7,
					"Titulo": "Tipo Descuento",
					"UrlAcceso": "/TipoDescuento",
					"Icono": "fa fa-money"
				},
				{
					"Orden": 8,
					"Titulo": "Tipo Doc. Identificacion",
					"UrlAcceso": "/TipoDocumentoIdentificacion",
					"Icono": "fa fa-id-card-o"
				},
				{
					"Orden": 9,
					"Titulo": "Moneda",
					"UrlAcceso": "/Moneda",
					"Icono": "fa fa-usd"
				}
			]
		}
	]
}
     
     */
