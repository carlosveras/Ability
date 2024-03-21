using Ability.Web.Models;
using Ability.Web.Utils;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Ability.Web.Controllers
{
    public class PedidosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PedidoValidator _pedidoValidator;

        public PedidosController(ApplicationDbContext context, PedidoValidator pedidoValidator)
        {
            _context = context;
            _pedidoValidator = pedidoValidator;
        }

        // GET: Pedidos
        public async Task<IActionResult> Index()
        {
              return _context.Pedidos != null ? 
                          View(await _context.Pedidos.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Pedidos'  is null.");
        }

        // GET: Pedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedidos/Create
        public async Task<IActionResult> Create(int? id)
        {
            List<Produto> produtos = new();

            try
            {
                produtos = await _context.Produtos.ToListAsync();
            }
            catch (CustomException ex)
            {
                TempData["Message"] = ex.Mensagem;
            }

            ViewBag.Produtos = new SelectList(produtos, "Id", "Descricao");

            if (id == null || id == 0)
            {
                ViewBag.Acao = "Criacao";
                return View(new Pedido());
            }
            else
            {
                ViewBag.Acao = "Edicao";
                Pedido pedido = null;

                try
                {
                    pedido = await _context.Pedidos.FindAsync(id);

                    if (pedido == null)
                        return NotFound();
                }
                catch (CustomException ex)
                {
                    TempData["Message"] = ex.Mensagem;
                    return View(pedido);
                }

                ViewBag.ProdutosSelecionados = await _context.PedidoItens
                                                             .Where(nt => nt.PedidoId == id)
                                                             .Select(nt => nt.PedidoId)
                                                             .ToListAsync();

                return View(pedido);
            }
        }

        // POST: Pedidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pedido pedido, int[] ProdutosSelecionados)
        {
            bool modelValido = validarModel(pedido, ProdutosSelecionados);

            if (modelValido)
            {
                if (pedido.Id == 0)
                    TempData["Message"] = "Pedido cadastrado com sucesso";
                else
                    TempData["Message"] = "Pedido atualizado com sucesso";

                try
                {
                    await CreateOrUpdatePedidoAsync(pedido, ProdutosSelecionados);
                    return RedirectToAction("Index", "Pedidos");
                }
                catch (CustomException ex)
                {
                    TempData["Message"] = ex.Message;
                    return RedirectToAction("Index", "Pedidos");
                }
            }

            var produtos = await _context.Produtos.ToListAsync();

            ViewBag.Tags = new SelectList(produtos, "Id", "Descricao");

            return View(pedido);
        }
        // GET: Pedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Texto")] Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pedido);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pedidos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Pedidos'  is null.");
            }
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
          return (_context.Pedidos?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task CreateOrUpdatePedidoAsync(Pedido pedido, int[] ProdutosSelecionados)
        {
            if (pedido.Id == 0)
            {
                try
                {
                    _context.Pedidos.Add(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (CustomException ex)
                {
                    throw new CustomException("", ex);
                }

                if (ProdutosSelecionados != null)
                {
                    foreach (var produtoId in ProdutosSelecionados)
                    {
                        var pedidoItem = new PedidoItem
                        {
                            PedidoId = pedido.Id,
                            ProdutoId = produtoId
                        };
                        _context.PedidoItens.Add(pedidoItem);
                    }
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (CustomException)
                    {
                        throw new CustomException();
                    }
                }
            }
            else if (pedido.Id != 0)
            {
                try
                {
                    _context.Pedidos.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (CustomException ex)
                {
                    throw new CustomException("", ex);
                }

                if (ProdutosSelecionados != null)
                {
                    List<PedidoItem> pedidoItensExistentes = new();

                    try
                    {
                        pedidoItensExistentes = _context.PedidoItens.Where(nt => nt.PedidoId == pedido.Id).ToList();
                    }
                    catch (CustomException ex)
                    {
                        throw new CustomException("", ex);
                    }

                    foreach (var pedidoItem in pedidoItensExistentes)
                    {
                        if (!ProdutosSelecionados.Contains(pedidoItem.PedidoId))
                        {
                            _context.PedidoItens.Remove(pedidoItem);
                        }
                    }

                    foreach (var pedidoId in ProdutosSelecionados)
                    {
                        if (!pedidoItensExistentes.Any(nt => nt.PedidoId == pedidoId))
                        {
                            var pedidoItem = new PedidoItem
                            {
                                PedidoId = pedido.Id,
                                ProdutoId = pedidoItensExistentes[0].ProdutoId
                            };

                            _context.PedidoItens.Add(pedidoItem);
                        }
                    }

                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (CustomException ex)
                    {
                        throw new CustomException("", ex);
                    }
                }
            }
        }

        private bool validarModel(Pedido pedido, int[] produtosSelecionados)
        {
            var validationResult = _pedidoValidator.Validate(pedido);

            if (produtosSelecionados == null || produtosSelecionados.Length == 0)
                validationResult.Errors.Add(new ValidationFailure("ProdutosSelecionados", "Você deve selecionar pelo menos um produto."));

            if (validationResult.Errors.Count > 0)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return false;
            }

            return true;
        }

    }
}
