using Entidad.Configuracion.Proceso;
using Entidad.Request.Maestro;
using Entidad.Request.Seguridad;
using Negocio.Repositorio.Maestro;
using Negocio.Repositorio.Seguridad;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            ConfiguracionJson.EstablecerConfiguracion();
        }

        [Test]
        public void Negocio_Registrar()
        {
            LnNegocio lnNegocio = new LnNegocio();
            long idNuevo = 0;
            lnNegocio.Registrar(new RequestNegocioRegistrarDto
            {
                Nombre = "Barrio",
                DocumentoIdentificacion = "12345678",
                IdTipoDocumentoIdentificacion = 1,
                IdUsuario = 4,
                Resenia = "Mejores precios siempre"
            }, ref idNuevo);
            Assert.Greater(idNuevo, 0);
        }

        [Test]
        public void Negocio_Obtener()
        {
            LnNegocio lnNegocio = new LnNegocio();
            var listaResultado = lnNegocio.Obtener(new RequestNegocioObtenerDto
            {
                Buscar = "",
                IdUsuario = 0,
                IdEstado = 1,
                CantidadRegistros = 1,
                NumeroPagina = 1,
                ColumnaOrden = "IdNegocio",
                DireccionOrden = "asc"
            });
            Assert.AreEqual(1, listaResultado.Count);
        }

        [Test]
        public void Usuario_Registrar()
        {
            LnUsuario lnUsuario = new LnUsuario();
            long idNuevo = 0;
            lnUsuario.Registrar(new RequestUsuarioRegistrarDto
            {
                CorreoElectronico = "correo30@correo.com",
                Nombre = "Juanito",
                Apellido = "Perez",
                Contrasenia = "123456"
            }, ref idNuevo);
            Assert.Greater(idNuevo, 0);
        }
    }
}