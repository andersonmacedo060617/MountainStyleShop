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
    public class NotaDeCompraController : Controller
    {
        // GET: NotaDeCompra
        public ActionResult Index()
        {
            var notasDeCompra = ConfigDB.Instance.NotaDeCompraFornecedorRepository.GetAll();
            ViewBag.NotasDeCompra = notasDeCompra;
            
            return View();
        }

        public ActionResult Novo()
        {
            var fornecedores = ConfigDB.Instance.FornecedorRepository.GetAll();
            var lstFornecedores = new SelectList(fornecedores, "Id", "Nome");
            ViewBag.lstFornecedores = lstFornecedores;
            
            return View();
        }

        [HttpPost]
        public ActionResult Gravar(NotaDeCompraFornecedor notaDeCompra)
        {
            ModelState.Remove("Fornecedor.Nome");
            notaDeCompra.Fornecedor = ConfigDB.Instance.FornecedorRepository.GetAll().FirstOrDefault(x => x.Id == notaDeCompra.Fornecedor.Id);

            bool novoRegistro = notaDeCompra.Id == 0 ? true:false;

            //Data de Cadastro Atual para o novo registro
            if (novoRegistro)
            {
                notaDeCompra.DataDeCadastro = DateTime.Now;
            }

            if (!ModelState.IsValid && novoRegistro)
            {
                return View("Novo", notaDeCompra);
            }else if(!ModelState.IsValid && !novoRegistro)
            {
                return View("Alterar", notaDeCompra);
            }
            
            ConfigDB.Instance.NotaDeCompraFornecedorRepository.Gravar(notaDeCompra);

            return RedirectToAction("Index");
        }

        public ActionResult Alterar(int id)
        {
            var notaDeCompra = ConfigDB.Instance.NotaDeCompraFornecedorRepository.GetAll().First(x => x.Id == id);
            if(notaDeCompra != null) { 
                var fornecedores = ConfigDB.Instance.FornecedorRepository.GetAll();
                var lstFornecedores = new SelectList(fornecedores, "Id", "Nome");
                ViewBag.lstFornecedores = lstFornecedores;

                return View(notaDeCompra);
            }
            
            return  RedirectToAction("Index");
            
        }

        public ActionResult ConfirmaDelete(int id)
        {
            var notaDeCompra = ConfigDB.Instance.NotaDeCompraFornecedorRepository.GetAll().FirstOrDefault(f => f.Id == id);
            if (notaDeCompra != null)
            {
                if(notaDeCompra.ItensPedidos.Count != 0)
                {
                    return RedirectToAction("Index");
                }
                return View(notaDeCompra);
            }

            return RedirectToAction("Index");
        }

        public ActionResult AddProduto(int id)
        {
            var notaDeCompra = ConfigDB.Instance.NotaDeCompraFornecedorRepository.GetAll().First(x => x.Id == id);

            if (notaDeCompra != null)
            {
                return View(notaDeCompra);
            }
            
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            var notaCompra = ConfigDB.Instance.NotaDeCompraFornecedorRepository.GetAll().FirstOrDefault(f => f.Id == id);
            if (notaCompra != null)
            {
                if (notaCompra.ItensPedidos.Count == 0) { 
                    ConfigDB.Instance.NotaDeCompraFornecedorRepository.Excluir(notaCompra);
                }
            }
            return RedirectToAction("Index");
        }

    }
}