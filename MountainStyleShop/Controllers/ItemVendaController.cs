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
                
                var vendasClienteSemConcluir = ConfigDB.Instance.VendaClienteRepository.GetAll().Where(x => x.VendaConfirmada == false);
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
    }
}