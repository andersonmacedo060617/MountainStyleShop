using MountainStyleShop.ModelNH.Config;
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
            ViewBag.Fornecedores = ConfigDB.Instance.PessoaRepository.GetAll().Where(x=>x.Fornecedor==true).OrderBy(x => x.Nome);
            return View();
        }
    }
}