using Entidad.Configuracion.Proceso;
using Microsoft.Extensions.Hosting;
using Negocio.Repositorio.Interaccion;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Negocio.Repositorio.Servicio
{
    public class LnServicioMatch// : IHostedService
    {
        //private readonly int _intervaloBuscarMatch = 60;
        //Timer _tmrBuscarMatch;

        //public Task StartAsync(CancellationToken cancellationToken)
        //{

        //    if (cancellationToken.IsCancellationRequested)
        //    {
        //        Logger.Log(Logger.Level.Error, "Se recibio la solicitud de cancelacion antes de iniciar el temporizador");
        //        cancellationToken.ThrowIfCancellationRequested();
        //    }

        //    _tmrBuscarMatch = new Timer(callback: async o =>
        //    await BgBuscarMatch(o),
        //    state: null,
        //    dueTime: TimeSpan.FromSeconds(0),
        //    period: TimeSpan.FromSeconds(_intervaloBuscarMatch)
        //    );



        //    return Task.CompletedTask;

        //}

        //private Task BgBuscarMatch(object o)
        //{
        //    LnMatch lnMatch = new LnMatch();
        //    return Task.FromResult<object>(lnMatch.ProcesarBusquedaMatch());
        //}

        

        //public Task StopAsync(CancellationToken cancellationToken)
        //{
        //    if (cancellationToken.IsCancellationRequested)
        //    {
        //        Logger.Log(Logger.Level.Error, "Se recibio la solicitud de cancelacion antes de detener el temporizador");
        //        cancellationToken.ThrowIfCancellationRequested();
        //    }
        //    //Cambiar el inicio de temporizador a infinito, por lo tanto, el temporizador se detiene 
        //    _tmrBuscarMatch?.Change(Timeout.Infinite, 0);
        //    return Task.CompletedTask;
        //}
    }
}
