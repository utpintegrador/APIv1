using App.CustomHandler;
using Entidad.Dto.Seguridad;
using Entidad.Request.Seguridad;
using Entidad.Response;
using Entidad.Response.Seguridad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Negocio.Repositorio.Seguridad;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
        [ProducesResponseType(typeof(ResponseUsuarioLoginDto), 200)]
        [ProducesResponseType(typeof(ResponseUsuarioLoginDto), 400)]
        [ProducesResponseType(typeof(ResponseUsuarioLoginDto), 401)]
        [ValidationActionFilter2]
        public async Task<ActionResult<ResponseUsuarioLoginDto>> Login([FromBody]RequestUsuarioCredencialesDto usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            UsuarioLoginDto result;
            //if (string.IsNullOrEmpty(mensajeValidacion))
            //{
            result = await Task.FromResult(_lnUsuario.ObtenerPorLogin(usuario));
            if (result != null)
            {
                return Ok(BuildToken(result));
            }
            //else
            //{
            //    mensajeValidacion = "Error en las credenciales";
            //}
            //}

            //ModelState.AddModelError(string.Empty, "Intento de logeo invalido");
            List<ErrorDto> listaError = new List<ErrorDto>();
            listaError.Add(new ErrorDto
            {
                Mensaje = "Intento de inicio de sesión invalido"
            });
            ResponseUsuarioLoginDto usuarioRetorno = new ResponseUsuarioLoginDto
            {
                IdUsuario = 0,
                Expiracion = null,
                Token = null,
                Nombre = null,
                Apellido = null,
                CorreoElectronico = null,
                UrlImagen = null,
                ListaError = listaError
            };

            //return Unauthorized(usuarioRetorno);// StatusCode(401, usuarioRetorno);// ModelState);
            return Unauthorized(usuarioRetorno);
            
        }

        private ResponseUsuarioLoginDto BuildToken(UsuarioLoginDto usuarioDto)
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

            if (usuarioDto.ListaRol == null) usuarioDto.ListaRol = new List<RolObtenerPorIdUsuarioDto>();
            if (usuarioDto.ListaRol.Any())
            {
                foreach (var item in usuarioDto.ListaRol)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item.Descripcion));
                }
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "Ninguno"));
            }
           
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(1);
            //var expiration = DateTime.UtcNow.AddMinutes(1);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
                );

            return new ResponseUsuarioLoginDto()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiracion = expiration,
                IdUsuario = usuarioDto.IdUsuario,
                Nombre = usuarioDto.Nombre,
                Apellido = usuarioDto.Apellido,
                CorreoElectronico = usuarioDto.CorreoElectronico,
                UrlImagen = usuarioDto.UrlImagen
            };
        }

    }
}
