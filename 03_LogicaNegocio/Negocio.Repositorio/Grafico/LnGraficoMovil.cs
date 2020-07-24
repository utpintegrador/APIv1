using Datos.Repositorio.Grafico;
using Entidad.Dto.Grafico;
using Entidad.Request.Grafico;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Negocio.Repositorio.Grafico
{
    public class LnGraficoMovil
    {
        private readonly AdGraficoMovil _adGraficoMovil = new AdGraficoMovil();
        public List<GraficoObtenerResumenComprasDto> ObtenerResumenCompras(RequestGraficoObtenerResumenComprasDto prm)
        {
            List<GraficoObtenerResumenComprasDto> respuesta = new List<GraficoObtenerResumenComprasDto>();
            //El parametro solo solicita la cantidad de meses atras, en base a ello se debe calcular las fechas
            try
            {
                DateTime fechaActual = DateTime.Now;
                DateTime fechaDesde = fechaActual.AddMonths(prm.CantidadMesesAtras * -1);

                fechaActual = fechaActual.AddMonths(1);
                fechaActual = new DateTime(fechaActual.Year, fechaActual.Month, 1);
                fechaActual = fechaActual.AddDays(-1);

                fechaDesde = new DateTime(fechaDesde.Year, fechaDesde.Month, 1);

                prm.Desde = fechaDesde;
                prm.Hasta = fechaActual;

                respuesta = _adGraficoMovil.ObtenerResumenCompras(prm);
                if (respuesta == null) respuesta = new List<GraficoObtenerResumenComprasDto>();

                if (respuesta.Any())
                {
                    foreach (var item in respuesta)
                    {
                        item.NombreMes = ObtenerNombreMes(item.Mes);
                    }
                }
            }
            catch
            {
            }
            return respuesta;
        }

        private string ObtenerNombreMes(int numeroMes)
        {
            string respuesta = string.Empty;
            switch (numeroMes)
            {
                case 1:
                    respuesta = "Enero";
                    break;
                case 2:
                    respuesta = "Febrero";
                    break;
                case 3:
                    respuesta = "Marzo";
                    break;
                case 4:
                    respuesta = "Abril";
                    break;
                case 5:
                    respuesta = "Mayo";
                    break;
                case 6:
                    respuesta = "Junio";
                    break;
                case 7:
                    respuesta = "Julio";
                    break;
                case 8:
                    respuesta = "Agosto";
                    break;
                case 9:
                    respuesta = "Septiembre";
                    break;
                case 10:
                    respuesta = "Octubre";
                    break;
                case 11:
                    respuesta = "Noviembre";
                    break;
                case 12:
                    respuesta = "Diciembre";
                    break;
                default:
                    break;
            }

            return respuesta;
        }

        public List<GraficoObtenerResumenVentasDto> ObtenerResumenVentas(RequestGraficoObtenerResumenVentasDto prm)
        {
            List<GraficoObtenerResumenVentasDto> respuesta = new List<GraficoObtenerResumenVentasDto>();
            //El parametro solo solicita la cantidad de meses atras, en base a ello se debe calcular las fechas
            try
            {
                DateTime fechaActual = DateTime.Now;
                DateTime fechaDesde = fechaActual.AddMonths(prm.CantidadMesesAtras * -1);

                fechaActual = fechaActual.AddMonths(1);
                fechaActual = new DateTime(fechaActual.Year, fechaActual.Month, 1);
                fechaActual = fechaActual.AddDays(-1);

                fechaDesde = new DateTime(fechaDesde.Year, fechaDesde.Month, 1);

                prm.Desde = fechaDesde;
                prm.Hasta = fechaActual;

                respuesta = _adGraficoMovil.ObtenerResumenVentas(prm);
                if (respuesta == null) respuesta = new List<GraficoObtenerResumenVentasDto>();

                if (respuesta.Any())
                {
                    foreach (var item in respuesta)
                    {
                        item.NombreMes = ObtenerNombreMes(item.Mes);
                    }
                }
            }
            catch
            {
            }
            return respuesta;
        }

    }
}
