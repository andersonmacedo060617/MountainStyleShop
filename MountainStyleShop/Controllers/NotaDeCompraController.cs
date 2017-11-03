using MountainStyleShop.ModelNH.Config;
using MountainStyleShop.ModelNH.ENum;
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
            var fornecedores = ConfigDB.Instance.FornecedorRepository.GetAll().Where(x=>x.Ativo);
            var lstFornecedores = new SelectList(fornecedores, "Id", "Nome");
            ViewBag.lstFornecedores = lstFornecedores;

            
            return View();
        }

        [HttpPost]
        public ActionResult Gravar(NotaDeCompraFornecedor notaDeCompra)
        {
            ModelState.Remove("Fornecedor.Nome");
            ModelState.Remove("StatusNotaCompra");
            notaDeCompra.Fornecedor = ConfigDB.Instance.FornecedorRepository.GetAll().FirstOrDefault(x => x.Id == notaDeCompra.Fornecedor.Id);

            bool novoRegistro = notaDeCompra.Id == 0 ? true:false;

            //Data de Cadastro Atual para o novo registro
            if (novoRegistro)
            {
                notaDeCompra.StatusNotaCompra = EStatusNotaCompraFornecedor.Aberta;
                notaDeCompra.DataDeCadastro = DateTime.Now;
            }

            if (!ModelState.IsValid && novoRegistro)
            {
                return RedirectToAction("Novo", "NotaDeCompra", notaDeCompra);
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
                notaDeCompra.DataDeEntrega = DateTime.Now;
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

        public ActionResult ConfirmarCompra(int id)
        {
            var nota = ConfigDB.Instance.NotaDeCompraFornecedorRepository.BuscaPorId(id);

            if(nota == null)
            {
                return RedirectToAction("Index", "NotaDeCompra");
            }

            nota.StatusNotaCompra = EStatusNotaCompraFornecedor.Encomendado;
            ConfigDB.Instance.NotaDeCompraFornecedorRepository.Gravar(nota);

            return RedirectToAction("AddProduto", "NotaDeCompra", new { id = nota.Id });

        }

        public ActionResult ConfirmaRecebimento(NotaDeCompraFornecedor notaCompra)
        {
            var nota = ConfigDB.Instance.NotaDeCompraFornecedorRepository.BuscaPorId(notaCompra.Id);

            if (nota == null)
            {
                return RedirectToAction("Index", "NotaDeCompra");
            }
            nota.DataDeEntrega = notaCompra.DataDeEntrega;
            nota.ConcluirNota();
            
            ConfigDB.Instance.NotaDeCompraFornecedorRepository.Gravar(nota);

            //Gero uma nota de despesa
            LancamentosCaixa lancamento = new LancamentosCaixa();
            lancamento.DataLancamento = nota.DataDaCompra;
            lancamento.Entrada = false;
            lancamento.ValorLancamento = nota.ValorTotalNota();
            lancamento.NotaDeCompra = nota;
            lancamento.Descricao = "Compra de Produtos da Loja";
            ConfigDB.Instance.LancamentosCaixaRepository.Gravar(lancamento);
            //---------------

            return RedirectToAction("AddProduto", "NotaDeCompra", new { id = nota.Id });

        }


    }
}