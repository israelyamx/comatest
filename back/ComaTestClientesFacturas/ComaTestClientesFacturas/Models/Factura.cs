using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComaTestClientesFacturas.Models
{
    public class Factura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string folio { get; set; }
        public string saldo { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime fecha_facturacion { get; set; }
        [ForeignKey("Usuario")]
        public int id_usuario { get; set; }
    }
}
