using MountainStyleShop.ModelNH.Config;
using MountainStyleShop.ModelNH.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MountainStyleShop.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
        public ActionResult Index()
        {
            ViewBag.Produtos = ConfigDB.Instance.ProdutoRepository.GetAll();
            return View();
        }

        public ActionResult Novo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Gravar(Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return View("Criar", produto);
            }

            ConfigDB.Instance.CategoriaRepository.Gravar(categoria);

            return RedirectToAction("Index");
        }


    }
}