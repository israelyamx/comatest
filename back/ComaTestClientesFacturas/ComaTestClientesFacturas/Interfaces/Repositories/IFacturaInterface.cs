using ComaTestClientesFacturas.Models;

namespace ComaTestClientesFacturas.Interfaces.Repositories
{
    public interface IFacturaInterface
    {
        public List<Factura> GetFacturas();
        public bool AddFactura(Factura factura);
        public bool DeleteFactura(int id);
    }
}
