using MountainStyleShop.ModelNH.Config;
using MountainStyleShop.ModelNH.Model;
using MountainStyleShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MountainStyleShop.Controllers
{
    public class ItemVendaController : Controller
    {
        public PartialViewResult FormAddItemVenda(int idProduto)
        {
            var produto = ConfigDB.Instance.ProdutoRepository.BuscaPorId(idProduto);
            ItemVendaCliente itemVendaCliente = new ItemVendaCliente();
            itemVendaCliente.Produto = produto;
            return PartialView("_FormAddItemVenda", itemVendaCliente);
        }

        [HttpPost]
        public ActionResult AdicionarItem(ItemVendaCliente itemVenda)
        {
            if(UsuarioUtils.Usuario != null)
            {
                var prod = ConfigDB.Instance.ProdutoRepository.BuscaPorId(itemVenda.Produto.Id);
                itemVenda.ValorUnitario = prod.Valor;
                
                var vendasClienteSemConcluir = ConfigDB.Instance.VendaClienteRepository.GetAll().Where(x => x.VendaConfirmada == false && x.Cliente.Id == UsuarioUtils.Usuario.Id);
                VendaCliente vendaCliente;

                if(vendasClienteSemConcluir.Count() == 0)
                {
                    vendaCliente = new VendaCliente();
                    vendaCliente.Cliente = UsuarioUtils.Usuario;
                    vendaCliente.VendaConfirmada = false;
                    ConfigDB.Instance.VendaClienteRepository.Gravar(vendaCliente);
                }else
                {
                    vendaCliente = vendasClienteSemConcluir.First();
                }

                itemVenda.VendaCliente = vendaCliente;
                itemVenda.Produto = prod;

                ConfigDB.Instance.ItemVendaClienteRepository.Gravar(itemVenda);
                
            }

            return RedirectToAction("CarrinhoCompra", "VendaCliente");
        }

        public ActionResult RemoverItemVenda(int idItem)
        {
            VendaCliente vendaEmAberto = new VendaCliente();
            if (UsuarioUtils.Usuario != null)
            {
                vendaEmAberto = ConfigDB.Instance.VendaClienteRepository.GetAll().Where(x =>
                x.Cliente.Id == UsuarioUtils.Usuario.Id &&
                x.VendaConfirmada == false).First();

                var itemRemover = vendaEmAberto.ItensVendaCliente.Where(x => x.Id == idItem).First();
                if (itemRemover != null)
                {
                    ConfigDB.Instance.ItemVendaClienteRepository.Excluir(itemRemover);
                }
            }

            return RedirectToAction("CarrinhoCompra", "VendaCliente");
        }

        public ActionResult AlterarItemVenda(int idItem)
        {
            VendaCliente vendaEmAberto = new VendaCliente();
            if (UsuarioUtils.Usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            vendaEmAberto = ConfigDB.Instance.VendaClienteRepository.GetAll().Where(x =>
            x.Cliente.Id == UsuarioUtils.Usuario.Id &&
            x.VendaConfirmada == false).First();

            var itemAlterar = vendaEmAberto.ItensVendaCliente.Where(x => x.Id == idItem).First();
            if (itemAlterar == null)
            {
                return RedirectToAction("CarrinhoCompra", "VendaCliente");
            }
            

            return View(itemAlterar);

        }

        [HttpPost]
        public ActionResult GravarItemAlterado(ItemVendaCliente itemVenda)
        {
            if (UsuarioUtils.Usuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var vendaEmAberto = ConfigDB.Instance.VendaClienteRepository.GetAll().Where(x =>
            x.Cliente.Id == UsuarioUtils.Usuario.Id &&
            x.VendaConfirmada == false).First();

            if(vendaEmAberto != null)
            {
                var prod = ConfigDB.Instance.ProdutoRepository.BuscaPorId(itemVenda.Produto.Id);
                itemVenda.ValorUnitario = prod.Valor;
                itemVenda.Produto = prod;
                itemVenda.VendaCliente = vendaEmAberto;
                ConfigDB.Instance.ItemVendaClienteRepository.Gravar(itemVenda);
            }            

            return RedirectToAction("CarrinhoCompra", "VendaCliente");

        }
    }
}