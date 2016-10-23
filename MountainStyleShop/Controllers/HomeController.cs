
using MountainStyleShop.ModelNH.Config;
using MountainStyleShop.ModelNH.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MountainStyleShop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Produtos = ConfigDB.Instance.ProdutoRepository.GetLimit(10);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ListaProdutos()
        {
            return View();
        }

        public ActionResult VisualizarProduto(int id)
        {
            var produto = ConfigDB.Instance.ProdutoRepository.GetAll().FirstOrDefault(c => c.Id == id);
            if (produto != null)
            {
                return View(produto);
            }
            return View();
        }
        

    }
}