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
            IList<Pessoa> fornecedores = ConfigDB.Instance.PessoaRepository.GetAll();
            fornecedores.OrderBy(x => x.Nome).Where(x=>x.Fornecedor == true);
            ViewBag.Fornecedores = fornecedores;
            return View();
        }

        public ActionResult Novo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Gravar(Pessoa fornecedor)
        {
            fornecedor.Fornecedor = true;
            fornecedor.PFisica = false;


            if (!ModelState.IsValid)
            {
                return RedirectToAction("Novo", fornecedor);
            }

            ConfigDB.Instance.PessoaRepository.Gravar(fornecedor);

            return RedirectToAction("Index");
        }

        public ActionResult Alterar(int id)
        {
            var fornecedor = ConfigDB.Instance.PessoaRepository.GetAll().FirstOrDefault(f => f.Id == id);
            if (fornecedor != null)
            {
                return View(fornecedor);
            }

            return RedirectToAction("Index");
        }

        public ActionResult ConfirmaDelete(int id)
        {
            var fornecedor = ConfigDB.Instance.PessoaRepository.GetAll().FirstOrDefault(f => f.Id == id);
            if (fornecedor != null)
            {
                return View(fornecedor);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            var fornecedor = ConfigDB.Instance.PessoaRepository.GetAll().FirstOrDefault(f => f.Id == id);
            if (fornecedor != null)
            {
                ConfigDB.Instance.PessoaRepository.Excluir(fornecedor);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Visualizar(int id)
        {
            var fornecedor = ConfigDB.Instance.PessoaRepository.GetAll().FirstOrDefault(c => c.Id == id);
            if (fornecedor != null)
            {
                return View(fornecedor);
            }
            return RedirectToAction("Index");
        }
    }
}