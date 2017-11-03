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
    public class FluxoCaixaController : Controller
    {
        // GET: FluxoCaixa
        public ActionResult Index()
        {
            var Lancamentos = ConfigDB.Instance.LancamentosCaixaRepository.GetAll().OrderByDescending(x=>x.DataLancamento).Take(50);
            return View(Lancamentos);
        }

        public PartialViewResult ModalNovoLancamento()
        {
            return PartialView("_ModalNovoLancamento");
        }

        public ActionResult Gravar(LancamentosCaixa lancamento)
        {
            ConfigDB.Instance.LancamentosCaixaRepository.Gravar(lancamento);

            return RedirectToAction("Index", "FluxoCaixa");
        }
    }
}