using MountainStyleShop.ModelNH.Config;
using MountainStyleShop.ModelNH.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MountainStyleShop.Controllers
{
    public class UFController : Controller
    {
        // GET: UF
        [Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            var UFs = ConfigDB.Instance.UFRepository.GetAll();
            return View(UFs);
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult Novo()
        {
            
            var paises = ConfigDB.Instance.PaisRepository.GetAll();
            var lstPais = new SelectList(paises, "Id", "Nome");
            ViewBag.lstPais = lstPais;
            return View();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public ActionResult Gravar(UF UF)
        {
            if (UF.Nome != null)
            {
                UF.Pais = ConfigDB.Instance.PaisRepository.BuscaPorId(UF.Pais.Id);
                ConfigDB.Instance.UFRepository.Gravar(UF);
            }

            return RedirectToAction("Index", "UF");
        }
    }
}