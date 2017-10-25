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

        [Authorize(Roles = "Administrador")]
        public ActionResult Novo()
        {
            Fornecedor fornecedor = new Fornecedor();
            fornecedor.Ativo = true;
            return View(fornecedor);
        }

        [Authorize(Roles = "Administrador")]
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

        [Authorize(Roles = "Administrador")]
        public ActionResult Alterar(int id)
        {
            var fornecedor = ConfigDB.Instance.FornecedorRepository.GetAll().FirstOrDefault(f => f.Id == id);
            if (fornecedor != null)
            {
                return View(fornecedor);
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult ConfirmaDelete(int id)
        {
            var fornecedor = ConfigDB.Instance.FornecedorRepository.GetAll().FirstOrDefault(f => f.Id == id);
            if (fornecedor != null)
            {
                return View(fornecedor);
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult Inativar(int id)
        {
            var fornecedor = ConfigDB.Instance.FornecedorRepository.GetAll().FirstOrDefault(f => f.Id == id);
            if (fornecedor != null)
            {
                fornecedor.Ativo = false;
                ConfigDB.Instance.FornecedorRepository.Gravar(fornecedor);
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