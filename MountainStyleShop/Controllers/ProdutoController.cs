using MountainStyleShop.ModelNH.Config;
using MountainStyleShop.ModelNH.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MountainStyleShop.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
        [Authorize(Roles = "Administrador")]
        public ActionResult Index(string BuscaProduto)
        {
            if (BuscaProduto != null)
            {
                ViewBag.Produtos = ConfigDB.Instance.ProdutoRepository.GetAll().Where(x => x.Nome.ToUpper().Contains(BuscaProduto.ToUpper())).ToList();
            }else
            {
                ViewBag.Produtos = ConfigDB.Instance.ProdutoRepository.GetAll();
            }
                
            return View();
        }
        [Authorize(Roles = "Administrador")]
        public ActionResult Novo()
        {
            var categorias = ConfigDB.Instance.CategoriaRepository.GetAll();
            var lstCategoria = new SelectList(categorias, "Id", "Nome");
            ViewBag.lstCategoria = lstCategoria;

            var fabricantes = ConfigDB.Instance.FabricanteRepository.GetAll();
            var lstFabricante = new SelectList(fabricantes, "Id", "Nome");
            ViewBag.lstFabricante = lstFabricante;

            return View();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public ActionResult Gravar(Produto produto, HttpPostedFileBase file)
        {
            ModelState.Remove("Categoria.Nome");
            ModelState.Remove("Categoria.Descricao");
            ModelState.Remove("Fabricante.Nome");
            ModelState.Remove("Fabricante.Pais");
            ModelState.Remove("Fabricante.Produtos");
            produto.Categoria = ConfigDB.Instance.CategoriaRepository.GetAll().FirstOrDefault(c => c.Id == produto.Categoria.Id);
            
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Novo", produto);
            }

            ConfigDB.Instance.ProdutoRepository.Gravar(produto);

            if(file != null)
            {
                if(produto.ImagemPrincipal != null)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/" + produto.ImagemPrincipal)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/" + produto.ImagemPrincipal));
                    }
                }
                string[] strName = file.FileName.Split('.');
                string strExt = strName[strName.Count() - 1];
                string pathSave = string.Format("{0}{1}_{2}.{3}", Server.MapPath("~/Imagens/Produtos/"), produto.Id, DateTime.Now.ToString("ddMMyyyy - HH_mm_ss"), strExt);
                string pathBase = string.Format("/Imagens/Produtos/{0}_{1}.{2}", produto.Id, DateTime.Now.ToString("ddMMyyyy - HH_mm_ss"), strExt);
                file.SaveAs(pathSave);
                produto.ImagemPrincipal = pathBase;

                ConfigDB.Instance.ProdutoRepository.Gravar(produto);
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult Alterar(int id)
        {
            var produto = ConfigDB.Instance.ProdutoRepository.GetAll().FirstOrDefault(f => f.Id == id);
            if (produto != null)
            {   
                var categorias = ConfigDB.Instance.CategoriaRepository.GetAll();
                var lstCategoria = new SelectList(categorias, "Id", "Nome", produto.Categoria);
                ViewBag.lstCategoria = lstCategoria;

                var fabricantes = ConfigDB.Instance.FabricanteRepository.GetAll();
                var lstFabricante = new SelectList(fabricantes, "Id", "Nome");
                ViewBag.lstFabricante = lstFabricante;

                return View(produto);
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult ConfirmaDelete(int id)
        {
            var produto = ConfigDB.Instance.ProdutoRepository.GetAll().FirstOrDefault(f => f.Id == id);
            if (produto != null)
            {
                return View(produto);
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult Apagar(Produto produto)
        {
            ConfigDB.Instance.ProdutoRepository.Excluir(produto);
            return RedirectToAction("Index");
        }

        
        public ActionResult Visualizar(int id)
        {
            var produto = ConfigDB.Instance.ProdutoRepository.GetAll().FirstOrDefault(x => x.Id == id);
            if(produto != null)
            {
                return View(produto);
            }
            return RedirectToAction("Index");
        }

        
        public ActionResult ProdutosCategoria(int idCategoria = 0)
        {
            if (idCategoria == 0)
                return RedirectToAction("Index", "Home");

            ViewBag.Produtos = ConfigDB.Instance.ProdutoRepository.GetAll().Where(x=>x.Categoria.Id == idCategoria).ToList();
            var Categoria = ConfigDB.Instance.CategoriaRepository.GetAll().FirstOrDefault(x => x.Id == idCategoria);

            if(Categoria == null)
                return RedirectToAction("Index", "Home");

            ViewBag.Categoria = Categoria;

            return View();
        }

        
        public PartialViewResult ExibirProdutos(int idCategoria = 0)
        {
            if(idCategoria == 0)
            {
                ViewBag.Produtos = ConfigDB.Instance.ProdutoRepository.GetAll().Where(x=>x.ApareeceNaVitrine).ToList();
            }
            else
            {
                ViewBag.Produtos = ConfigDB.Instance.ProdutoRepository.GetAll().Where(x => x.Categoria.Id == idCategoria).ToList();
            }
            
            return PartialView("_ExibirProdutos");
        }

    }
}