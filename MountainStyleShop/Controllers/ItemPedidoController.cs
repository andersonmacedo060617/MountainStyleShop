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
                return View(item);
            }

            return RedirectToAction("AddProduto", "NotaDeCompra", IdDaNota);
        }

    }
}