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
            NotaDeCompraFornecedor nota = ConfigDB.Instance.NotaDeCompraFornecedorRepository.GetAll().First(n => n.Id == IdDaNota && !n.CompraConfirmada);
            
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
            var notaCompra = ConfigDB.Instance.NotaDeCompraFornecedorRepository.GetAll().Where(x => x.Id == item.NotaDeCompra.Id && !x.CompraConfirmada).First();

            if(produto == null || notaCompra == null)
            {
                return RedirectToAction("Index", "NotaDeCompra");
            }

            item.Produto = produto;
            item.NotaDeCompra = notaCompra;

            ConfigDB.Instance.ItemNotaCompraFornecedorRepository.Gravar(item);

            return RedirectToAction("AddProduto", "NotaDeCompra", new { id = item.NotaDeCompra.Id});
        }

        public ActionResult ListaVlrAdd(int IdItemPedido)
        {
            var itemPedido = ConfigDB.Instance.ItemNotaCompraFornecedorRepository.GetAll().Where(x => x.Id == IdItemPedido).First();
            if (itemPedido == null)
            {
                return RedirectToAction("Index", "NotaDeCompra");
            }

            return View(itemPedido);
        }

        public PartialViewResult FormVlrAdd(int idItemPedido)
        {
            var itemPedido = ConfigDB.Instance.ItemNotaCompraFornecedorRepository.BuscaPorId(idItemPedido);
            ValorAddNotaCompraPedido vlrAddPedido = new ValorAddNotaCompraPedido();
            vlrAddPedido.ItemNotaCompraFornecedor = itemPedido;

            return PartialView("_FormVlrAdd", vlrAddPedido);
        }

        [HttpPost]
        public ActionResult GravaVlrAdicional(ValorAddNotaCompraPedido vlrAddPedido)
        {
            var itemPedido = ConfigDB.Instance.ItemNotaCompraFornecedorRepository.GetAll().Where(x => x.Id == vlrAddPedido.ItemNotaCompraFornecedor.Id && !x.NotaDeCompra.CompraConfirmada).First();
            
            if(itemPedido == null)
            {
                return RedirectToAction("Index", "NotaDeCompra");
            }

            vlrAddPedido.ItemNotaCompraFornecedor = itemPedido;
            ConfigDB.Instance.ValorAddNotaCompraPedidoRepository.Gravar(vlrAddPedido);

            return RedirectToAction("ListaVlrAdd", "ItemPedido", new { IdItemPedido = vlrAddPedido.ItemNotaCompraFornecedor.Id });
        }

    }
}