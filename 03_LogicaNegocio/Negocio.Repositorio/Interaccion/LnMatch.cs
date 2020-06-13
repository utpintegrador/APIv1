using Datos.Repositorio.Interaccion;
using Entidad.Configuracion.Proceso;
using Entidad.Dto.Interaccion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Negocio.Repositorio.Interaccion
{
    public class LnMatch: Logger
    {

        public Boolean ProcesarBusquedaMatch()
        {
            try
            {
                LnContacto lnContacto = new LnContacto();
                var listado = lnContacto.ObtenerPendienteMatch();
                if (listado != null)
                {
                    if (listado.Any())
                    {
                        foreach (var item in listado)
                        {
                            ProcesarAlertaRecuperacionIndividual(item);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Log(Level.Error, ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
            return false;
        }

        private void ProcesarAlertaRecuperacionIndividual(ContactoObtenerPendienteMatchDto entidad)
        {
            try
            {
                MatchRegistrarDto match = new MatchRegistrarDto
                {
                    IdUsuarioEmisor = entidad.IdUsuario1,
                    IdEstadoValoracionUsuarioEmisor = entidad.IdValoracionDelUsuario1AlUsuario2,
                    IdUsuarioMatch = entidad.IdUsuario2,
                    IdEstadoValoracionUsuarioMatch = entidad.IdValoracionDelUsuario2AlUsuario1
                };

                long idNuevo = 0;

                using (TransactionScope scope = new TransactionScope())
                {
                    int resultadoRegistrarMatch = Registrar(match, ref idNuevo);
                    if (resultadoRegistrarMatch > 0 && idNuevo > 0)
                    {
                        LnContacto lnContacto = new LnContacto();
                        int resultadoEliminarContacto = lnContacto.Eliminar(entidad.IdContacto);
                        if (resultadoEliminarContacto > 0)
                        {
                            scope.Complete();
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                Log(Level.Error, ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }

        public int Registrar(MatchRegistrarDto modelo, ref long idNuevo)
        {
            AdMatch adMatch = new AdMatch();
            return adMatch.Registrar(modelo, ref idNuevo);
        }

        public List<MatchObtenerPorIdUsuarioDto> ObtenerPorIdUsuario(long idUsuario)
        {
            AdMatch adMatch = new AdMatch();
            return adMatch.ObtenerPorIdUsuario(idUsuario);
        }

    }
}
