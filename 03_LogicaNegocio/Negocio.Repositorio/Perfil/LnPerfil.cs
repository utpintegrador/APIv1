using Datos.Repositorio.Perfil;
using Entidad.Dto.Perfil;
using System;
using System.Collections.Generic;

namespace Negocio.Repositorio.Perfil
{
    public class LnPerfil
    {
        private readonly AdPerfil _adPerfil = new AdPerfil();
        public PerfilObtenerDto ObtenerPorIdUsuario(long idUsuario)
        {
            return _adPerfil.ObtenerPorIdUsuario(idUsuario);
        }

        public Entidad.Entidad.Perfil.Perfil ObtenerPorId(long id)
        {
            return _adPerfil.ObtenerPorId(id);
        }

        public int Registrar(PerfilRegistrarDto modelo, ref long idNuevo)
        {
            return _adPerfil.Registrar(modelo, ref idNuevo);
        }

        public int Modificar(PerfilModificarDto modelo)
        {
            return _adPerfil.Modificar(modelo);
        }

        public PerfilObtenerDatoAcademicoDto ObtenerDatoAcademicoPorId(long id)
        {
            return _adPerfil.ObtenerDatoAcademicoPorId(id);
        }

        public PerfilObtenerDatoLaboralDto ObtenerDatoLaboralPorId(long id)
        {
            return _adPerfil.ObtenerDatoLaboralPorId(id);
        }

        public PerfilObtenerInformacionDto ObtenerInformacionPorId(long id)
        {
            return _adPerfil.ObtenerInformacionPorId(id);
        }

        public PerfilObtenerInteresesDto ObtenerInteresesPorId(long id)
        {
            return _adPerfil.ObtenerInteresesPorId(id);
        }

        public int ModificarDatoAcademico(PerfilModificarDatoAcademicoDto modelo)
        {
            return _adPerfil.ModificarDatoAcademico(modelo);
        }

        public int ModificarDatoLaboral(PerfilModificarDatoLaboralDto modelo)
        {
            return _adPerfil.ModificarDatoLaboral(modelo);
        }

        public int ModificarInformacion(PerfilModificarInformacionDto modelo)
        {
            return _adPerfil.ModificarInformacion(modelo);
        }

        public int ModificarIntereses(PerfilModificarInteresesDto modelo)
        {
            return _adPerfil.ModificarIntereses(modelo);
        }

        //public int ModificarUrlImagen(PerfilModificarUrlImagenDto modelo)
        //{
        //    return _adPerfil.ModificarUrlImagen(modelo);
        //}

        //public int ModificarUrlImagenPorIdUsuario(long idUsuario, string url)
        //{
        //    return _adPerfil.ModificarUrlImagenPorIdUsuario(idUsuario, url);
        //}

        public List<PerfilObtenerPasarelaDto> ObtenerPasarela(long idUsuario)
        {
            return _adPerfil.ObtenerPasarela(idUsuario);
        }

        public PerfilObtenerInformacionCompletaDto ObtenerInformacionCompleta(long idUsuario)
        {
            var entidad = _adPerfil.ObtenerInformacionCompleta(idUsuario);
            if(entidad != null)
            {
                if(entidad.FechaNacimientoFormatoDate != null)
                {
                    entidad.FechaNacimiento = Convert.ToDateTime(entidad.FechaNacimientoFormatoDate).ToString("dd/MM/yyyy");
                }
            }
            return entidad;
        }

    }
}
