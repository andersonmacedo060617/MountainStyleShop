using MountainStyleShop.ModelNH.Config;
using MountainStyleShop.ModelNH.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MountainStyleShop.Controllers
{
    public class ItemPedidoController : Controller
    {
        
        public ActionResult Novo(int IdDaNota)
        {
            NotaDeCompra nota = ConfigDB.Instance.NotaDeCompraRepository.GetAll().First(n => n.Id == IdDaNota);
            
            if(nota != null) {
                ItemPedido item = new ItemPedido()
                {
                    NotaDeCompra = nota
                };
                return View(item);
            }

            return RedirectToAction("AddProduto", "NotaDeCompra", IdDaNota);
        }

    }
}