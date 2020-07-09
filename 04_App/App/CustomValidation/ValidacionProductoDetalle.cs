using Entidad.Request.Transaccion;
using Entidad.Response;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.CustomValidation
{
    public static class ValidacionProductoDetalle
    {
        public static List<ErrorDto> ValidarListaModificar(RequestPedidoModificarDto objetoModificar, ref bool ok)
        {
            List<ErrorDto> lista = new List<ErrorDto>();
            try
            {
                foreach (var det in objetoModificar.ListaPedidoDetalle)
                {
                    if(det.Accion == "add")
                    {
                        if (det.IdPedidoDetalle != 0)
                        {
                            lista.Add(new ErrorDto
                            {
                                Mensaje = "Accion 'add': el parametro IdPedidoDetalle debe ser cero cuando se registra uno nuevo"
                            });
                        }
                    }
                    else if (det.Accion == "upd")
                    {
                        if (det.IdPedidoDetalle == 0)
                        {
                            lista.Add(new ErrorDto
                            {
                                Mensaje = "Accion 'upd': el parametro IdPedidoDetalle debe ser mayor a cero cuando se actualiza un registro"
                            });
                        }
                    }
                    else if (det.Accion == "del")
                    {
                        if (det.IdPedidoDetalle == 0)
                        {
                            lista.Add(new ErrorDto
                            {
                                Mensaje = "Accion 'del': el parametro IdPedidoDetalle debe ser mayor a cero cuando se elimina un registro"
                            });
                        }
                    }
                    else if (det.Accion == "read")
                    {
                    }
                    else if (string.IsNullOrEmpty(det.Accion))
                    {
                        lista.Add(new ErrorDto
                        {
                            Mensaje = "Accion '<vacio>': el parametro Accion es requerido"
                        });
                    }
                    else
                    {
                        lista.Add(new ErrorDto
                        {
                            Mensaje = string.Format("Accion '{0}': el parametro Accion no es válido", det.Accion)
                        });
                    }
                }
            }
            catch(Exception ex)
            {
                lista.Add(new ErrorDto
                {
                    Mensaje = (ex.InnerException == null ? ex.Message : ex.InnerException.Message).Replace(Environment.NewLine, " ")
                });
                ok = false;
            }

            if (lista.Any())
            {
                ok = false;
            }
            else
            {
                ok = true;
            }

            return lista;
        }
    }
}
