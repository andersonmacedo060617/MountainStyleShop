using MountainStyleShop.ModelNH.Config;
using MountainStyleShop.ModelNH.Model;
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
            var notasDeCompra = ConfigDB.Instance.NotaDeCompraRepository.GetAll();
            ViewBag.NotasDeCompra = notasDeCompra;
            return View();
        }

        public ActionResult Novo()
        {
            var fornecedores = ConfigDB.Instance.PessoaRepository.GetAll();
            var lstFornecedores = new SelectList(fornecedores.Where(x => x.Fornecedor == true), "Id", "Nome");
            ViewBag.lstFornecedores = lstFornecedores;

            return View();
        }

        [HttpPost]
        public ActionResult Novo(NotaDeCompra notaDeCompra)
        {
            ModelState.Remove("Fornecedor.Nome");
            notaDeCompra.Fornecedor = ConfigDB.Instance.PessoaRepository.GetAll().FirstOrDefault(x => x.Id == notaDeCompra.Fornecedor.Id);
            
            if(ConfigDB.Instance.NotaDeCompraRepository.GetAll().First(x=>x.Id == notaDeCompra.Id) == null)
            {
                notaDeCompra.DataDeCadastro = DateTime.Now;
            }

            if (!ModelState.IsValid)
            {
                return View("Novo", notaDeCompra);
            }
            
            ConfigDB.Instance.NotaDeCompraRepository.Gravar(notaDeCompra);

            return RedirectToAction("Index");
        }

        public ActionResult Alterar(int id)
        {
            var notaDeCompra = ConfigDB.Instance.NotaDeCompraRepository.GetAll().First(x => x.Id == id);
            if(notaDeCompra != null) { 
                var fornecedores = ConfigDB.Instance.PessoaRepository.GetAll();
                var lstFornecedores = new SelectList(fornecedores.Where(x => x.Fornecedor == true), "Id", "Nome");
                ViewBag.lstFornecedores = lstFornecedores;

                return View(notaDeCompra);
            }
            
            return  RedirectToAction("Index");
            
        }
    }
}