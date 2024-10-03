using ComaTestClientesFacturas.Interfaces.Repositories;
using ComaTestClientesFacturas.Models;
using ComaTestClientesFacturas.Persistance;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ComaTestClientesFacturas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private ApplicationDbContext _context;
        private IUsuarioInterface _usuarioInterface;

        public UsuarioController(ApplicationDbContext context, IUsuarioInterface usuarioInterface)
        {
            _context = context;
            _usuarioInterface = usuarioInterface;
        }

        [HttpGet]
        [Route("GenerarTokens")]
        public IActionResult GenerarToken(int id)
        {
            var token = _usuarioInterface.generateJwtToken(id);
            return Ok(token);
        }

        [HttpGet]
        [Route("ListaUsuarios")]
        public IActionResult ListaUsuarios() {
            var usuarios = _usuarioInterface.GetUsuarios();
            return Ok(usuarios);
        }

        [HttpPost]
        [Authorize(Roles = "Gerencia", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("AgregarUsuarios")]
        public IActionResult AgregarUsuario(Usuario usuario)
        {
            var respuesta = _usuarioInterface.AddUsuarios(usuario);
            if (respuesta)
            {
                return Ok(respuesta);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Administrador", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("EliminarUsuarios")]
        public IActionResult EliminarUsuario(int id)
        {
            var respuesta = _usuarioInterface.DeleteUsuarios(id);
            if (respuesta)
            {
                return Ok(respuesta);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
