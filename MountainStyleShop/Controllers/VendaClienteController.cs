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
    public class VendaClienteController : Controller
    {
        // GET: VendaCliente
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CarrinhoCompra()
        {
            VendaCliente vendaEmAberto;
            if (UsuarioUtils.Usuario != null)
            {
                vendaEmAberto = ConfigDB.Instance.VendaClienteRepository.GetAll().Where(x => x.VendaConfirmada == false).First();
            }else
            {
                vendaEmAberto = new VendaCliente();
                vendaEmAberto.Cliente = UsuarioUtils.Usuario;
                vendaEmAberto.VendaConfirmada = false;

                ConfigDB.Instance.VendaClienteRepository.Gravar(vendaEmAberto);
            }

            return View("CarrinhoCompra", vendaEmAberto);
        }
    }
}