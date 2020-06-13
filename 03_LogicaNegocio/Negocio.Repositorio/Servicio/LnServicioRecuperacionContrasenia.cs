using Entidad.Configuracion.Proceso;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Negocio.Repositorio.Servicio
{
    public class LnServicioRecuperacionContrasenia //: IHostedService
    {
        //private readonly int _intervaloEnvioCorreoConCodigoRecuperarContrasenia = 60;
        //private readonly int _intervaloEliminarProcesados = 60;
        //Timer _tmrEnvioCodigoRecuperacionContrasenia;
        //Timer _tmrEliminarProcesados;

        //public Task StartAsync(CancellationToken cancellationToken)
        //{

        //    if (cancellationToken.IsCancellationRequested)
        //    {
        //        Logger.Log(Logger.Level.Error, "Se recibio la solicitud de cancelacion antes de iniciar el temporizador");
        //        cancellationToken.ThrowIfCancellationRequested();
        //    }

        //    _tmrEliminarProcesados = new Timer(callback: async o =>
        //    await BgEliminarProcesos(o),
        //    state: null,
        //    dueTime: TimeSpan.FromSeconds(0),
        //    period: TimeSpan.FromSeconds(_intervaloEliminarProcesados)
        //    );

        //    _tmrEnvioCodigoRecuperacionContrasenia = new Timer(callback: async o =>
        //    await BgEnvioCodigoRecuperacionContrasenia(o),
        //    state: null,
        //    dueTime: TimeSpan.FromSeconds(0),
        //    period: TimeSpan.FromSeconds(_intervaloEnvioCorreoConCodigoRecuperarContrasenia)
        //    );



        //    return Task.CompletedTask;

        //}

        //private Task BgEnvioCodigoRecuperacionContrasenia(object o)
        //{

        //    LnRecuperacionContrasenia lnRecuperacionContrasenia = new LnRecuperacionContrasenia();
        //    return Task.FromResult<object>(lnRecuperacionContrasenia.ProcesarAlertaRecuperacionContrasenia());
        //}

        //private Task BgEliminarProcesos(object o)
        //{
        //    LnRecuperacionContrasenia lnRecuperacionContrasenia = new LnRecuperacionContrasenia();
        //    return Task.FromResult<object>(lnRecuperacionContrasenia.EliminarProcesados());
        //}

        //public Task StopAsync(CancellationToken cancellationToken)
        //{
        //    if (cancellationToken.IsCancellationRequested)
        //    {
        //        Logger.Log(Logger.Level.Error, "Se recibio la solicitud de cancelacion antes de detener el temporizador");
        //        cancellationToken.ThrowIfCancellationRequested();
        //    }    
        //    //Cambiar el inicio de temporizador a infinito, por lo tanto, el temporizador se detiene 
        //    _tmrEnvioCodigoRecuperacionContrasenia?.Change(Timeout.Infinite, 0);
        //    return Task.CompletedTask;
        //}

    }
}
