using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ability.Web.Models
{
    public class PedidoItem
    {
        [Key]
        [Column(Order = 1)]
        public int Id { get; set; }

        [ForeignKey("Pedido")]
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }

        [ForeignKey("Produtos")]
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }

    }
}
