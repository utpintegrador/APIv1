using Entidad.Dto.Seguridad;
using Entidad.Entidad.Seguridad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Negocio.Repositorio.Seguridad;
using System;
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
        public async Task<ActionResult<UsuarioTokenDto>> Login([FromBody]UsuarioCredencialesDto usuario)
        {
            var result = await Task.FromResult(_lnUsuario.ObtenerPorLogin(usuario));
            if(result != null)
            {
                result.UserName = usuario.Usuario;
                return Ok(BuildToken(result));
            }
            else
            {
                //ModelState.AddModelError(string.Empty, "Intento de logeo invalido");
                UsuarioTokenDto usuarioRetorno = new UsuarioTokenDto
                {
                    IdUsuario = 0,
                    Expiracion = null,
                    Token = null,
                    Nombre = null,
                    Apellido = null,
                    CorreoElectronico = null,
                    UserName = null,
                    UrlImagen = null
                };

                //return Unauthorized(usuarioRetorno);// StatusCode(401, usuarioRetorno);// ModelState);
                return Ok(usuarioRetorno);
            }

        }

        private UsuarioTokenDto BuildToken(UsuarioLoginDto usuarioDto)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuarioDto.Nombre),
                new Claim(JwtRegisteredClaimNames.UniqueName, usuarioDto.UserName),
                new Claim("apellido", usuarioDto.Apellido),
                new Claim("idusuario", usuarioDto.IdUsuario.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

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
                UserName = usuarioDto.UserName,
                UrlImagen = usuarioDto.UrlImagen
            };
        }

    }
}
