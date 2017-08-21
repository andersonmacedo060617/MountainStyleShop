using MountainStyleShop.ModelNH.Config;
using MountainStyleShop.ModelNH.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MountainStyleShop.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class FornecedorController : Controller
    {
        // GET: Fornecedor
        public ActionResult Index()
        {
            IList<Fornecedor> fornecedores = ConfigDB.Instance.FornecedorRepository.GetAll();
            fornecedores.OrderBy(x => x.Nome);
            ViewBag.Fornecedores = fornecedores;
            return View();
        }

        public ActionResult Novo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Gravar(Fornecedor fornecedor)
        {
            
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Novo", fornecedor);
            }

            ConfigDB.Instance.FornecedorRepository.Gravar(fornecedor);

            return RedirectToAction("Index");
        }

        public ActionResult Alterar(int id)
        {
            var fornecedor = ConfigDB.Instance.FornecedorRepository.GetAll().FirstOrDefault(f => f.Id == id);
            if (fornecedor != null)
            {
                return View(fornecedor);
            }

            return RedirectToAction("Index");
        }

        public ActionResult ConfirmaDelete(int id)
        {
            var fornecedor = ConfigDB.Instance.FornecedorRepository.GetAll().FirstOrDefault(f => f.Id == id);
            if (fornecedor != null)
            {
                return View(fornecedor);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            var fornecedor = ConfigDB.Instance.FornecedorRepository.GetAll().FirstOrDefault(f => f.Id == id);
            if (fornecedor != null)
            {
                ConfigDB.Instance.FornecedorRepository.Excluir(fornecedor);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Visualizar(int id)
        {
            var fornecedor = ConfigDB.Instance.FornecedorRepository.GetAll().FirstOrDefault(c => c.Id == id);
            if (fornecedor != null)
            {
                return View(fornecedor);
            }
            return RedirectToAction("Index");
        }
    }
}