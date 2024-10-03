using ComaTestClientesFacturas.Interfaces.Repositories;
using ComaTestClientesFacturas.Models;
using ComaTestClientesFacturas.Persistance;

namespace ComaTestClientesFacturas.Repositories
{
    public class FacturasRepository : IFacturaInterface
    {
        private readonly ApplicationDbContext _context;

        public FacturasRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Factura> GetFacturas() {
            var facturas = _context.Facturas.ToList();
            return facturas;
        }

        public bool AddFactura(Factura factura) {
            _context.Facturas.Add(factura);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteFactura(int id) {
            var fac = _context.Facturas.Find(id);
            if (fac != null)
            {
                _context.Facturas.Remove(fac);
                _context.SaveChanges();
            }
            return true;
        }

    }
}
