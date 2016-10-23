using MountainStyleShop.ModelNH.Config;
using MountainStyleShop.ModelNH.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MountainStyleShop.Controllers
{
    public class CategoriaController : Controller
    {
        // GET: Categoria
        public ActionResult Index()
        {
            ViewBag.Categorias = ConfigDB.Instance.CategoriaRepository.GetAll();
            return View();
        }

        public ActionResult Novo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Gravar(Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return View("Novo", categoria);
            }

            ConfigDB.Instance.CategoriaRepository.Gravar(categoria);
            
            return RedirectToAction("Index");
        }

        public ActionResult Alterar(int id)
        {
            var categoria = ConfigDB.Instance.CategoriaRepository.GetAll().FirstOrDefault(f=>f.Id == id);
            if(categoria != null)
            {
                return View(categoria);
            }

            return RedirectToAction("Index");
        }

        public ActionResult ConfirmaDelete(int id)
        {
            var categoria = ConfigDB.Instance.CategoriaRepository.GetAll().FirstOrDefault(f => f.Id == id);
            if (categoria != null)
            {
                return View(categoria);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Apagar(Categoria categoria)
        {
            ConfigDB.Instance.CategoriaRepository.Excluir(categoria);
            return RedirectToAction("Index");
        }

        public ActionResult Visualizar(int id)
        {
            var categoria = ConfigDB.Instance.CategoriaRepository.GetAll().FirstOrDefault(c=>c.Id == id);
            if(categoria != null)
            {
                return View(categoria);
            }
            return RedirectToAction("Index");
        }


    }
}