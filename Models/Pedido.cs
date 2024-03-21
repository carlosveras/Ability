using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ability.Web.Models
{
    public class Pedido
    {
        [Key]
        [Column(Order = 1)]
        public int Id { get; set; }

        [Column(TypeName = "varchar(100)", Order = 2)]
        public string Titulo { get; set; }

        [Column(TypeName = "varchar(500)", Order = 3)]
        public string Texto { get; set; }
      
        public List<PedidoItem> PedidoItens { get; set; }
    }

}
