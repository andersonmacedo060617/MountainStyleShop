using MountainStyleShop.ModelNH.Config;
using MountainStyleShop.ModelNH.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MountainStyleShop.Controllers
{
    public class FabricanteController : Controller
    {
        // GET: Fabricante
        public ActionResult Index()
        {
            var Fabricantes = ConfigDB.Instance.FabricanteRepository.GetAll();
            return View(Fabricantes);
        }

        public ActionResult Novo()
        {
            var paises = ConfigDB.Instance.PaisRepository.GetAll();
            var lstPaises = new SelectList(paises, "Id", "Nome");
            ViewBag.lstPaises = lstPaises;

            return View();
        }

        [HttpPost]
        public ActionResult Gravar(Fabricante Fabricante)
        {
            if (Fabricante.Nome != null)
                ConfigDB.Instance.FabricanteRepository.Gravar(Fabricante);

            return RedirectToAction("Index");
        }
    }
}