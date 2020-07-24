using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tests
{
    public class NUnit_API
    {
        [SetUp]
        public void Setup()
        {
            //ConfiguracionJson.EstablecerConfiguracion();
        }

        [Test]
        public void Usuario_Login()
        {
            var request = new Entidad.Request.Seguridad.RequestUsuarioCredencialesDto
            {
                CorreoElectronico = string.Empty,
                Contrasenia = string.Empty
            };
            var contexto = new ValidationContext(request, null, null);
            var listaErrores = new List<ValidationResult>();
            var esModeloValido = Validator.TryValidateObject(request, contexto, listaErrores, true);

            if (listaErrores == null) listaErrores = new List<ValidationResult>();
            foreach (var error in listaErrores)
            {
                Console.WriteLine(error.ErrorMessage);
            }
            var valorEsperado = false;
            Assert.AreEqual(valorEsperado, esModeloValido);
        }

        //[Test]
        //public void Negocio_Registrar()
        //{
        //    LnNegocio lnNegocio = new LnNegocio();
        //    long idNuevo = 0;
        //    lnNegocio.Registrar(new RequestNegocioRegistrarDto
        //    {
        //        Nombre = "Barrio",
        //        DocumentoIdentificacion = "12345678",
        //        IdTipoDocumentoIdentificacion = 1,
        //        IdUsuario = 4,
        //        Resenia = "Mejores precios siempre"
        //    }, ref idNuevo);
        //    Assert.Greater(idNuevo, 0);
        //}

        //[Test]
        //public void Negocio_Obtener()
        //{
        //    LnNegocio lnNegocio = new LnNegocio();
        //    var listaResultado = lnNegocio.Obtener(new RequestNegocioObtenerDto
        //    {
        //        Buscar = "",
        //        IdUsuario = 0,
        //        IdEstado = 1,
        //        CantidadRegistros = 1,
        //        NumeroPagina = 1,
        //        ColumnaOrden = "IdNegocio",
        //        DireccionOrden = "asc"
        //    });
        //    Assert.AreEqual(1, listaResultado.Count);
        //}

        //[Test]
        //public void Usuario_Registrar()
        //{
        //    LnUsuario lnUsuario = new LnUsuario();
        //    long idNuevo = 0;
        //    lnUsuario.Registrar(new RequestUsuarioRegistrarDto
        //    {
        //        CorreoElectronico = "correo30@correo.com",
        //        Nombre = "Juanito",
        //        Apellido = "Perez",
        //        Contrasenia = "123456"
        //    }, ref idNuevo);
        //    Assert.Greater(idNuevo, 0);
        //}
    }
}