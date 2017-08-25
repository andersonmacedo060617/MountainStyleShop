using MountainStyleShop.ModelNH.Config;
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

    }
}