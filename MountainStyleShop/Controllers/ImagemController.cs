﻿using MountainStyleShop.ModelNH.Config;
using MountainStyleShop.ModelNH.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MountainStyleShop.Controllers
{
    public class ImagemController : Controller
    {
        // GET: Imagem
        public ActionResult AddImagemProduto(int idProduto)
        {
            var Produto = ConfigDB.Instance.ProdutoRepository.BuscaPorId(idProduto);

            if (Produto == null)
                return RedirectToAction("Index", "Produto");

            ViewBag.Produto = Produto;
            return View();
        }

        public ActionResult Gravar(Imagem Imagem, HttpPostedFileBase file)
        {
            var Prod = ConfigDB.Instance.ProdutoRepository.BuscaPorId(Imagem.Produto.Id);
            if (Prod == null)
            {
                return RedirectToAction("Index", "Produto");
            }

            Imagem.Produto = Prod;

            if (file != null)
            {
                
                string[] strName = file.FileName.Split('.');
                string strExt = strName[strName.Count() - 1];
                string pathSave = string.Format("{0}{1}_{2}.{3}", Server.MapPath("~/Imagens/Produtos/"), Imagem.Produto.Id, DateTime.Now.ToString("ddMMyyyy - HH_mm_ss"), strExt);
                string pathBase = string.Format("/Imagens/Produtos/{0}_{1}.{2}", Imagem.Produto.Id, DateTime.Now.ToString("ddMMyyyy - HH_mm_ss"), strExt);
                file.SaveAs(pathSave);
                Imagem.Caminho = pathBase;

                ConfigDB.Instance.ImagemRepository.Gravar(Imagem);
            }

            return RedirectToAction("Index", "Produto");
        }

    }
}