using ComaTestClientesFacturas.Interfaces.Repositories;
using ComaTestClientesFacturas.Models;
using ComaTestClientesFacturas.Persistance;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComaTestClientesFacturas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private ApplicationDbContext _context;
        private IFacturaInterface _facturainterface;

        public FacturaController(ApplicationDbContext context, IFacturaInterface inteface)
        {
            _context = context;
            _facturainterface = inteface;
        }

        /// <summary>
        /// Endpoint para obtener Lista de facturas.
        /// </summary>
        /// <remarks>
        /// Endpoint para obtener el listado de facturas para la aplicacion.
        /// </remarks>
        /// <returns>json con el listado de facturas.</returns>
        [HttpGet]
        [Authorize(Roles = "Administrador, Gerencia, Anonimo", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("ListaFacturas")]
        public IActionResult ListaFacturas()
        {
            var facturas = _facturainterface.GetFacturas();
            return Ok(facturas);
        }
        /// <summary>
        /// Endpoint para agregar facturas.
        /// </summary>
        /// <remarks>
        /// Endpoint para agregar nuevas facturas al sistema.
        /// </remarks>
        /// <param name="Factura">Referencia de objeto factura.</param>
        /// <returns>Boleano con la respuesta del servicio.</returns>
        [HttpPost]
        [Authorize(Roles = "Gerencia", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("AgregarFacturas")]
        public IActionResult AgregarFactura(Factura factura)
        {
            var respuesta = _facturainterface.AddFactura(factura);
            if (respuesta)
            {
                return Ok(respuesta);
            }
            else
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// Endpoint para eliminar facturas.
        /// </summary>
        /// <remarks>
        /// Endpoint para eliminar facturas del sistema.
        /// </remarks>
        /// <param name="id">Identificador de la factura.</param>
        /// <returns>Boleano con la respuesta del servicio.</returns>
        [HttpDelete]
        [Authorize(Roles = "Administrador", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("EliminarFacturas")]
        public IActionResult EliminarFactura(int id)
        {
            var respuesta = _facturainterface.DeleteFactura(id);
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
