using MountainStyleShop.ModelNH.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MountainStyleShop.Controllers
{
    public class NotaDeCompraController : Controller
    {
        // GET: NotaDeCompra
        public ActionResult Index()
        {
            ViewBag.NotasDeCompra = ConfigDB.Instance.NotaDeCompraRepository.GetAll().OrderBy(x=>x.DataDeCadastro);
            return View();
        }

        public ActionResult Novo()
        {
            var fornecedores = ConfigDB.Instance.PessoaRepository.GetAll().Where(x=>x.Fornecedor == true);
            var lstFornecedores = new SelectList(fornecedores, "Id", "Nome");
            ViewBag.lstFornecedores = lstFornecedores;

            return View();
        }
    }
}