using MountainStyleShop.ModelNH.Config;
using MountainStyleShop.ModelNH.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MountainStyleShop.Controllers
{
    public class CidadeController : Controller
    {
        // GET: Cidade
        [Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            var Cidades = ConfigDB.Instance.CidadeRepository.GetAll();
            return View(Cidades);
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult Novo()
        {
            var Estados = ConfigDB.Instance.UFRepository.GetAll();
            var lstEstados = new SelectList(Estados, "Id", "Estado_Pais");
            ViewBag.lstEstados = lstEstados;

            return View();
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult Gravar(Cidade Cidade)
        {
            if(Cidade.Nome != null)
            {
                Cidade.UF = ConfigDB.Instance.UFRepository.BuscaPorId(Cidade.UF.Id);
                ConfigDB.Instance.CidadeRepository.Gravar(Cidade);
            }
            
            return RedirectToAction("Index", "Cidade");
        }


        public ActionResult GetCidades(int idUF)
        {
            var Cidades = ConfigDB.Instance.CidadeRepository.GetAll().Where(x => x.UF.Id == idUF);
            var lstCidades = new SelectList(Cidades, "Id", "Nome");

            return Json(lstCidades);
        }
    }
}