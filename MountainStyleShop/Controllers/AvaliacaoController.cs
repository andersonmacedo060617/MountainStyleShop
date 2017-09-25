using MountainStyleShop.ModelNH.Config;
using MountainStyleShop.ModelNH.Model;
using MountainStyleShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MountainStyleShop.Controllers
{
    public class AvaliacaoController : Controller
    {
        
        public PartialViewResult PainelAvaliacoesProduto(int idProduto)
        {
            ViewBag.Avaliacoes = ConfigDB.Instance.AvaliacaoProdutoRepository.GetAll().Where(x=>x.Produto.Id == idProduto).ToList();
            return PartialView("_PainelAvaliacoesProduto");
        }
        
        public PartialViewResult FormularioInseriAvaliacao(int idProduto)
        {
            AvaliacaoProduto avaliacao = new AvaliacaoProduto();
            avaliacao.Produto = ConfigDB.Instance.ProdutoRepository.BuscaPorId(idProduto);

            //ViewBag.Avaliacao = avaliacao;

            return PartialView("_FormularioInseriAvaliacao", avaliacao);
        }

        [Authorize(Roles = "Usuario")]
        [HttpPost]
        public ActionResult Gravar(AvaliacaoProduto avaliacaoProduto)
        {
            avaliacaoProduto.Produto = ConfigDB.Instance.ProdutoRepository.BuscaPorId(avaliacaoProduto.Produto.Id);
            avaliacaoProduto.Data = DateTime.Now;
            avaliacaoProduto.Usuario = UsuarioUtils.Usuario.Login == "Admin" ? null : UsuarioUtils.Usuario;

            ConfigDB.Instance.AvaliacaoProdutoRepository.Gravar(avaliacaoProduto);

            return RedirectToAction("Visualizar", "Produto", new { id = avaliacaoProduto.Produto.Id });
        }

    }
}