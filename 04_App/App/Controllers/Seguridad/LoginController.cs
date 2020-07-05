﻿using Entidad.Dto.Seguridad;
using Entidad.Entidad.Seguridad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Negocio.Repositorio.Seguridad;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace App.Controllers.Seguridad
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    { 
        private IConfiguration _config;
        private readonly LnUsuario _lnUsuario = new LnUsuario();
        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        //[ProducesResponseType(403)]
        [ProducesResponseType(typeof(UsuarioTokenDto), 200)]
        [ProducesResponseType(typeof(UsuarioTokenDto), 401)]
        public async Task<ActionResult<UsuarioTokenDto>> Login([FromBody]UsuarioCredencialesDto usuario)
        {
            string mensajeValidacion = string.Empty;
            if (string.IsNullOrEmpty(usuario.CorreoElectronico) && string.IsNullOrEmpty(usuario.Contrasenia))
            {
                mensajeValidacion = "No se ha enviado ningún parametro";
            }
            else
            {
                if (string.IsNullOrEmpty(usuario.CorreoElectronico))
                {
                    mensajeValidacion = "Parametro correo electrónico es requerido";
                }
                else
                {
                    if (string.IsNullOrEmpty(usuario.Contrasenia))
                    {
                        mensajeValidacion = "Parametro contrasenia es requerido";
                    }
                }
            }

            UsuarioLoginDto result;
            if (string.IsNullOrEmpty(mensajeValidacion))
            {
                result = await Task.FromResult(_lnUsuario.ObtenerPorLogin(usuario));
                if (result != null)
                {
                    return Ok(BuildToken(result));
                }
                else
                {
                    mensajeValidacion = "Error en las credenciales";
                }
            }

            //ModelState.AddModelError(string.Empty, "Intento de logeo invalido");
            UsuarioTokenDto usuarioRetorno = new UsuarioTokenDto
            {
                IdUsuario = 0,
                Expiracion = null,
                Token = null,
                Nombre = null,
                Apellido = null,
                CorreoElectronico = null,
                UrlImagen = null,
                Resultado = mensajeValidacion
            };

            //return Unauthorized(usuarioRetorno);// StatusCode(401, usuarioRetorno);// ModelState);
            return Unauthorized(usuarioRetorno);
            
        }

        private UsuarioTokenDto BuildToken(UsuarioLoginDto usuarioDto)
        {
            //var claims = new[]
            //{
            //    new Claim(ClaimTypes.Name, usuarioDto.Nombre),
            //    new Claim(JwtRegisteredClaimNames.UniqueName, usuarioDto.CorreoElectronico),
            //    new Claim("apellido", usuarioDto.Apellido),
            //    new Claim("idusuario", usuarioDto.IdUsuario.ToString()),
            //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            //};
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuarioDto.Nombre),
                new Claim(JwtRegisteredClaimNames.UniqueName, usuarioDto.CorreoElectronico),
                new Claim("apellido", usuarioDto.Apellido),
                new Claim("idusuario", usuarioDto.IdUsuario.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            List<string> listaRoles = new List<string>();
            listaRoles.Add("Administrador");
            listaRoles.Add("Usuario");
            foreach (var item in listaRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(1);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
                );

            return new UsuarioTokenDto()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiracion = expiration,
                IdUsuario = usuarioDto.IdUsuario,
                Nombre = usuarioDto.Nombre,
                Apellido = usuarioDto.Apellido,
                CorreoElectronico = usuarioDto.CorreoElectronico,
                UrlImagen = usuarioDto.UrlImagen,
                Resultado = "Ok"
            };
        }

    }
}
