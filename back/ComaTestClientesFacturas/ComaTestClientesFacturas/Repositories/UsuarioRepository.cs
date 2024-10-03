using ComaTestClientesFacturas.Interfaces.Repositories;
using ComaTestClientesFacturas.Models;
using ComaTestClientesFacturas.Persistance;
using Microsoft.AspNetCore.SignalR;

namespace ComaTestClientesFacturas.Repositories
{
    public class UsuarioRepository : IUsuarioInterface
    {
        private readonly ApplicationDbContext _dbContext;

        public UsuarioRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Usuario> GetUsuarios()
        {
            var usuarios = _dbContext.Usuarios.ToList();
            return usuarios;
        }

        public bool DeleteUsuarios(int id)
        {
            var user = _dbContext.Usuarios.Find(id);
            if (user != null) {                
                _dbContext.Usuarios.Remove(user);
                _dbContext.SaveChanges();
            }
            return true;
        }

        public bool AddUsuarios(Usuario usuario) {
            _dbContext.Usuarios.Add(usuario);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
