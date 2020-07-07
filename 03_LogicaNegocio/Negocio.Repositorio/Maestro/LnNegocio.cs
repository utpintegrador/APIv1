﻿using Datos.Repositorio.Maestro;
using Entidad.Configuracion.Proceso;
using Entidad.Dto.Maestro;
using Entidad.Entidad.Maestro;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;

namespace Negocio.Repositorio.Maestro
{
    public class LnNegocio: Logger
    {
        private readonly AdNegocio _adNegocio = new AdNegocio();
        public List<NegocioObtenerDto> Obtener(NegocioObtenerPrmDto filtro)
        {
            if (filtro == null) filtro = new NegocioObtenerPrmDto();
            if (filtro.NumeroPagina == 0) filtro.NumeroPagina = 1;
            if (filtro.CantidadRegistros == 0) filtro.CantidadRegistros = 10;
            if (string.IsNullOrEmpty(filtro.ColumnaOrden)) filtro.ColumnaOrden = "IdNegocio";
            if (string.IsNullOrEmpty(filtro.DireccionOrden)) filtro.DireccionOrden = "desc";
            //if (filtro.IdUsuario == null) filtro.IdUsuario = 0;

            var listado = _adNegocio.Obtener(filtro);
            if (listado == null)
            {
                listado = new List<NegocioObtenerDto>();
            }
            return listado;
        }

        public List<NegocioObtenerCercanosDto> ObtenerCercanos(NegocioObtenerCercanosPrmDto filtro)
        {
            if (filtro == null) filtro = new NegocioObtenerCercanosPrmDto();
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

        public int Registrar(NegocioRegistrarPrmDto modelo, ref long idNuevo)
        {
            int respuesta = 0;
            try
            {
                if (modelo.ListaUbicacion != null)
                {
                    if (modelo.ListaUbicacion.Any())
                    {
                        modelo.ListaUbicacion = modelo.ListaUbicacion.Where(x => x.Latitud != 0 && x.Longitud != 0).ToList();
                        if (modelo.ListaUbicacion.Any())
                        {
                            int cantidadPredeterminados = modelo.ListaUbicacion.Where(x => x.Predeterminado == 1).Count();
                            if (cantidadPredeterminados > 1)
                            {
                                //Desmarcar todos menos uno
                                bool seEncontroMarcado = false;
                                foreach (var ubicacion in modelo.ListaUbicacion)
                                {
                                    if (!seEncontroMarcado)
                                    {
                                        if (ubicacion.Predeterminado == 1)
                                        {
                                            seEncontroMarcado = true;
                                        }
                                    }
                                    else
                                    {
                                        ubicacion.Predeterminado = 0;
                                    }
                                }
                            }
                            else
                            {
                                modelo.ListaUbicacion.First().Predeterminado = 1;
                            }
                        }
                    }
                }

                using (var scope = new TransactionScope())
                {
                    respuesta = _adNegocio.Registrar(modelo, ref idNuevo);
                    LnNegocioUbicacion lnNegocioUbicacion = new LnNegocioUbicacion();
                    if (modelo.ListaUbicacion == null) modelo.ListaUbicacion = new List<NegocioRegistrarUbicacionRegistrarFiltroDto>();

                    if (modelo.ListaUbicacion.Any())
                    {
                        int cantidadOkEsperadas = modelo.ListaUbicacion.Count;
                        int cantidadUbicacionesOk = 0;
                        foreach (var negUbi in modelo.ListaUbicacion)
                        {
                            var entUbi = new NegocioUbicacionRegistrarPrmDto
                            {
                                IdNegocio = idNuevo,
                                Descripcion = negUbi.Descripcion,
                                Titulo = negUbi.Titulo,
                                Latitud = negUbi.Latitud,
                                Longitud = negUbi.Longitud,
                                Predeterminado = false
                            };
                            if (negUbi.Predeterminado == 1) entUbi.Predeterminado = true;
                            long idNuevoUbicacion = 0;
                            int resultadoUbicacion = lnNegocioUbicacion.Registrar(entUbi, ref idNuevoUbicacion);
                            if (resultadoUbicacion > 0 && idNuevoUbicacion > 0)
                            {
                                cantidadUbicacionesOk++;
                            }
                        }
                        if(cantidadOkEsperadas == cantidadUbicacionesOk)
                        {
                            scope.Complete();
                        }
                    }
                    else
                    {
                        scope.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                Log(Level.Error, ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
            return respuesta;
        }

        public int Modificar(NegocioModificarPrmDto modelo)
        {
            return _adNegocio.Modificar(modelo);
        }

        public int Eliminar(long id)
        {
            return _adNegocio.Eliminar(id);
        }

    }
}
