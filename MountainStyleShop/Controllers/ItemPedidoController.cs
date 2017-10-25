using MountainStyleShop.ModelNH.Config;
using MountainStyleShop.ModelNH.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MountainStyleShop.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class ItemPedidoController : Controller
    {
        
        public ActionResult Novo(int IdDaNota)
        {
            NotaDeCompraFornecedor nota = ConfigDB.Instance.NotaDeCompraFornecedorRepository.GetAll().First(n => n.Id == IdDaNota);
            
            if(nota != null) {
                ItemNotaCompraFornecedor item = new ItemNotaCompraFornecedor()
                {
                    NotaDeCompra = nota
                };

                var produtos = ConfigDB.Instance.ProdutoRepository.GetAll().Where(x => x.Ativo);
                ViewBag.lstProdutos = new SelectList(produtos, "Id", "Nome");

                return View(item);
            }

            return RedirectToAction("AddProduto", "NotaDeCompra", IdDaNota);
        }

        [HttpPost]
        public ActionResult Gravar(ItemNotaCompraFornecedor item)
        {
            var produto = ConfigDB.Instance.ProdutoRepository.GetAll().Where(x => x.Id == item.Produto.Id && x.Ativo).First();
            var notaCompra = ConfigDB.Instance.NotaDeCompraFornecedorRepository.GetAll().Where(x => x.Id == item.NotaDeCompra.Id && !x.ProdutoEntregue()).First();

            if(produto == null || notaCompra == null)
            {
                return RedirectToAction("Index", "NotaDeCompra");
            }

            item.Produto = produto;
            item.NotaDeCompra = notaCompra;

            ConfigDB.Instance.ItemNotaCompraFornecedorRepository.Gravar(item);

            return RedirectToAction("AddProduto", "NotaDeCompra", new { id = item.NotaDeCompra.Id});
        }

    }
}