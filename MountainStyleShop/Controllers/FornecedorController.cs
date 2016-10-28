using MountainStyleShop.ModelNH.Config;
using MountainStyleShop.ModelNH.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MountainStyleShop.Controllers
{
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
    }
}