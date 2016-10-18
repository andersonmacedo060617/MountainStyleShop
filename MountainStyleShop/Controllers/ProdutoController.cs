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
            var categorias = ConfigDB.Instance.CategoriaRepository.GetAll();
            var lstCategoria = new SelectList(categorias, "Id", "Nome");
            ViewBag.lstCategoria = lstCategoria;

            return View();
        }

        [HttpPost]
        public ActionResult Gravar(Produto produto)
        {
            produto.Categoria = ConfigDB.Instance.CategoriaRepository.GetAll().FirstOrDefault(c => c.Id == produto.Categoria.Id);
            if (!ModelState.IsValid)
            {
                return View("Novo", produto);
            }

            ConfigDB.Instance.ProdutoRepository.Gravar(produto);

            return RedirectToAction("Index");
        }



    }
}