using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComaTestClientesFacturas.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? apellido { get; set; }
        public int  edad { get; set; }
        public string? correo { get; set; }
        public int tipo_usuario { get; set; }
    }
}
