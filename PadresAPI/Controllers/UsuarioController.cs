using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PadresAPI.DTOs;
using PadresAPI.Models;
using PadresAPI.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace PadresAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        Repository<Usuario> repository;
        public UsuarioController(Sistem21PrimariaContext context)
        {
            repository = new(context);
        }

        public IActionResult Get()
        {
            try
            {
                var usuarios = repository.Get().Select(x => new UsuarioDTO()
                {
                    NombreUsuario = x.Usuario1,
                    Password = x.Contraseña,
                    Rol = x.Rol
                }).ToList();

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Get(UsuarioDTO usuario)
        {
            try
            {
                var u = repository.Get().FirstOrDefault(x => x.Usuario1.ToLower() == usuario.NombreUsuario.ToLower() &&
                x.Contraseña == usuario.Password);

                if (u != null)
                {
                    UsuarioDTO usuarioencontrado = new UsuarioDTO()
                    {
                        NombreUsuario = u.Usuario1,
                        Password = u.Contraseña,
                        Rol = u.Rol
                    };

                    return Ok(usuarioencontrado);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes); // .NET 5 +

                // Convert the byte array to hexadecimal string prior to .NET 5
                // StringBuilder sb = new System.Text.StringBuilder();
                // for (int i = 0; i < hashBytes.Length; i++)
                // {
                //     sb.Append(hashBytes[i].ToString("X2"));
                // }
                // return sb.ToString();
            }
        }
    }
}
