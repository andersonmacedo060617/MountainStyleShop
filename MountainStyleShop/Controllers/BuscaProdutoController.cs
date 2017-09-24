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
    public class BuscaProdutoController : Controller
    {

        [Authorize(Roles = "Usuario")]
        public ActionResult ListaHistoricoBusca()
        {
            if (UsuarioUtils.Usuario == null)
                RedirectToAction("Index", "Home");
            

            var lstHistoricoBusca = ConfigDB.Instance.BuscaProdutoRepository.GetAll().Where(x => x.Usuario.Id == UsuarioUtils.Usuario.Id);
            return PartialView("_ListaHistoricoBusca", lstHistoricoBusca.ToList());

        }

        public ActionResult UtilizarConsulta(int id)
        {
            var consuta = ConfigDB.Instance.BuscaProdutoRepository.BuscaPorId(id);
            TempData["UtilizarConsulta"] = consuta;
            return RedirectToAction("BuscaAvancada", "Produto");
        }
    }
}