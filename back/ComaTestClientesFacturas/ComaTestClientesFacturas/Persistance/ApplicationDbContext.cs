using ComaTestClientesFacturas.Models;
using Microsoft.EntityFrameworkCore;

namespace ComaTestClientesFacturas.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Constructor de la clase ApplicationDbContext.
        /// </summary>
        /// <param name="options">Opciones para configurar el contexto de la base de datos.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Factura> Facturas { get; set; }
    }
}
