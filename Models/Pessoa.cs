using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ability.Web.Models
{
    public class Pessoa
    {

        [Key]
        [Column(Order = 1)]
        [HiddenInput(DisplayValue = false)]
        public int PessoaId { get; set; }

        [Column(TypeName = "varchar(100)", Order =2)]
        public string Nome { get; set; }

        [Column(TypeName = "varchar(100)", Order =3)]
        public string  Email { get; set; }

        [Required, Range(0, 20)]
        [Column(TypeName ="int",  Order =4)]
        public int Idade { get; set; }
    }
}
