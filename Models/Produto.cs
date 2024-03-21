using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ability.Web.Models
{
    public class Produto
    {
        [Key]
        [Column(Order = 1)]
        public int Id { get; set; }

        [Column(TypeName = "varchar(150)", Order = 2)]
        public string Descricao { get; set; } = string.Empty;
    }
}
