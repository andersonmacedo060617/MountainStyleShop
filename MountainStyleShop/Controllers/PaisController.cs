using MountainStyleShop.ModelNH.Config;
using MountainStyleShop.ModelNH.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MountainStyleShop.Controllers
{
    public class PaisController : Controller
    {
        // GET: Pais
        [Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            var Paises = ConfigDB.Instance.PaisRepository.GetAll();
            return View(Paises);
        }
        [Authorize(Roles = "Administrador")]
        public ActionResult Novo()
        {
            return View();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public ActionResult Gravar(Pais pais)
        {
            if(pais.Nome != null)
            {
                ConfigDB.Instance.PaisRepository.Gravar(pais);
            }

            return RedirectToAction("Index");
        }
    }
}