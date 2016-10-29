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
            ModelState.Remove("Categoria.Nome");
            ModelState.Remove("Categoria.Descricao");
            produto.Categoria = ConfigDB.Instance.CategoriaRepository.GetAll().FirstOrDefault(c => c.Id == produto.Categoria.Id);
            
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Novo", produto);
            }

            ConfigDB.Instance.ProdutoRepository.Gravar(produto);

            return RedirectToAction("Index");
        }

        public ActionResult Alterar(int id)
        {
            var produto = ConfigDB.Instance.ProdutoRepository.GetAll().FirstOrDefault(f => f.Id == id);
            if (produto != null)
            {
                var categorias = ConfigDB.Instance.CategoriaRepository.GetAll();
                var lstCategoria = new SelectList(categorias, "Id", "Nome", produto.Categoria);
                ViewBag.lstCategoria = lstCategoria;

                return View(produto);
            }

            return RedirectToAction("Index");
        }

        public ActionResult ConfirmaDelete(int id)
        {
            var produto = ConfigDB.Instance.ProdutoRepository.GetAll().FirstOrDefault(f => f.Id == id);
            if (produto != null)
            {
                return View(produto);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Apagar(Produto produto)
        {
            ConfigDB.Instance.ProdutoRepository.Excluir(produto);
            return RedirectToAction("Index");
        }

        public ActionResult Visualizar(int id)
        {
            var produto = ConfigDB.Instance.ProdutoRepository.GetAll().FirstOrDefault(x => x.Id == id);
            if(produto != null)
            {
                return View(produto);
            }
            return RedirectToAction("Index");
        }

    }
}