using Ability.Web.Models;
using FluentValidation;

namespace Ability.Web.Utils
{
    public class PedidoValidator : AbstractValidator<Pedido>
    {
        public PedidoValidator()
        {
            RuleFor(x => x.Titulo).
                NotEmpty().
                WithMessage("Titulo não pode estar vazio");

            RuleFor(x => x.Texto).
                NotEmpty().
                WithMessage("Texto não pode estar vazio");
        }
    }
}
