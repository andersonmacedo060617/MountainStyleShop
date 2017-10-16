using MountainStyleShop.ModelNH.Config;
using MountainStyleShop.ModelNH.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MountainStyleShop.Controllers
{
    public class CupomDescontoController : Controller
    {
        [HttpPost]
        public ActionResult GerarCupom(CupomDesconto Cupom)
        {
            var Cliente = ConfigDB.Instance.UsuarioRepository.BuscaPorId(Cupom.Cliente.Id);
            if(Cliente != null)
            {
                Cupom.Utilizado = false;
                Cupom.InserirValor();
                Cupom.Cliente = Cliente;

                ConfigDB.Instance.CupomDescontoRepository.Gravar(Cupom);
            }
            
            return RedirectToAction("Index", "Cliente");
        }
    }
}