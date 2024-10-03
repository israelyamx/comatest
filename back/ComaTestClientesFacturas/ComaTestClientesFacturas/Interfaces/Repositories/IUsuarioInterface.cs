using ComaTestClientesFacturas.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ComaTestClientesFacturas.Interfaces.Repositories
{
    public interface IUsuarioInterface
    {
        public List<Usuario> GetUsuarios();
        public bool DeleteUsuarios(int id);
        public bool AddUsuarios(Usuario usuario);
        public string generateJwtToken(int id)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("SECRETKEYSECRETKEYSECRETKEYSECRETKEYSECRETKEYSECRETKEY");
            var rol = "";
            switch (id)
            {
                case 0:
                case 1:
                    rol = "Anonimo";
                    break;
                case 2:
                    rol = "Gerencia";
                    break;
                case 3:
                    rol = "Administrador";
                    break;
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", id.ToString()), new Claim(ClaimTypes.Role, rol), }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
