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

            var produto = new Produto()
            {
                Valor = 0
            };
            return View(produto);
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
            ModelState.Remove("Estoque");
            produto.Categoria = ConfigDB.Instance.CategoriaRepository.GetAll().FirstOrDefault(c => c.Id == produto.Categoria.Id);
            
            
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
            var produto = ConfigDB.Instance.ProdutoRepository.GetAll().FirstOrDefault(x => x.Id == id && x.Valor > 0);

            if(produto == null)
            {
                return RedirectToAction("Index");
                
            }

            if (UsuarioUtils.Usuario != null)
            {
                ViewBag.UsuarioLogado = true;
            }else
            {
                ViewBag.UsuarioLogado = false;
            }
                
            return View(produto);
        }

        
        public ActionResult ProdutosCategoria(int idCategoria = 0)
        {
            if (idCategoria == 0)
                return RedirectToAction("Index", "Home");

            ViewBag.Produtos = ConfigDB.Instance.ProdutoRepository.GetAll().Where(x=>x.Categoria.Id == idCategoria && x.Valor > 0).ToList();
            var Categoria = ConfigDB.Instance.CategoriaRepository.GetAll().FirstOrDefault(x => x.Id == idCategoria);

            if(Categoria == null)
                return RedirectToAction("Index", "Home");

            ViewBag.Categoria = Categoria;

            return View();
        }

        public PartialViewResult ExibirProdutoSelecionado(int idProduto)
        {
            
             ViewBag.Produtos = ConfigDB.Instance.ProdutoRepository.GetAll().Where(x => x.Id == idProduto && x.Valor > 0).ToList();
            
            return PartialView("_ExibirProdutosMini");
        }

        public PartialViewResult ExibirProdutos(int idCategoria = 0)
        {
            if(idCategoria == 0)
            {
                if(UsuarioUtils.Usuario == null || UsuarioUtils.Usuario.CategoriasFavoritas().Count == 0) { 
                    Random rnd = new Random();
                    var Produtos = ConfigDB.Instance.ProdutoRepository.GetAll()
                        .Where(x => x.ApareeceNaVitrine && x.Ativo && x.Valor > 0)
                        //Ordem aleatoria
                        .OrderBy(i => rnd.Next())
                        .Take(8).ToList();
                
                    ViewBag.Produtos = Produtos;
                }
                else
                {
                    Usuario user = ConfigDB.Instance.UsuarioRepository.BuscaPorId(UsuarioUtils.Usuario.Id);
                    List<Produto> lstProdutos = new List<Produto>();
                    foreach (var catFav in user.CategoriasFavoritas())
                    {
                        var prodCat = ConfigDB.Instance.ProdutoRepository.GetAll().Where(x => x.Categoria.Id == catFav.Id && x.Ativo && x.Valor > 0);
                        foreach (var produto in prodCat)
                        {
                            lstProdutos.Add(produto);
                        }
                    }

                    //lstProdutos = ConfigDB.Instance.ProdutoRepository.GetAll()
                    //    .Where(x => x.ApareeceNaVitrine && x.Categoria.Id == user.)
                    //    .Take(8).ToList();

                    ViewBag.Produtos = lstProdutos.Take(8).ToList();
                }

            }
            else
            {
                ViewBag.Produtos = ConfigDB.Instance.ProdutoRepository.GetAll().Where(x => x.Categoria.Id == idCategoria && x.Ativo && x.Valor > 0).ToList();
            }
            
            return PartialView("_ExibirProdutos");
        }

        [HttpPost]
        public ActionResult ConsultaProdutos(BuscaProduto ParametrosBusca)
        {
            var filtroProdutos = ConfigDB.Instance.ProdutoRepository.GetAll().Where(x => x.Ativo && x.Valor > 0);

            bool AlgumParamPreenchido = false;

            

            if (ParametrosBusca.IdProduto != 0 && ParametrosBusca.IdProduto != null)
            {
                AlgumParamPreenchido = true;
                filtroProdutos = filtroProdutos.Where(x => x.Id == ParametrosBusca.IdProduto);
            }
            if (ParametrosBusca.Descricao != null && ParametrosBusca.Descricao != "")
            {
                AlgumParamPreenchido = true;
                filtroProdutos = filtroProdutos.Where(x => x.Descricao.ToUpper().Contains(ParametrosBusca.Descricao.ToUpper()));
            }
            if (ParametrosBusca.Categoria != null && ParametrosBusca.Categoria.Id != 0)
            {
                AlgumParamPreenchido = true;
                filtroProdutos = filtroProdutos.Where(x => x.Categoria.Id == ParametrosBusca.Categoria.Id);
            }
            else
            {
                ParametrosBusca.Categoria = null;
            }

            if (ParametrosBusca.Fabricante != null && ParametrosBusca.Fabricante.Id != 0)
            {
                AlgumParamPreenchido = true;
                filtroProdutos = filtroProdutos.Where(x => x.Fabricante.Id == ParametrosBusca.Fabricante.Id);
            }
            else
            {
                ParametrosBusca.Fabricante = null;
            }

            if (ParametrosBusca.ValorInicio != null && ParametrosBusca.ValorInicio != 0)
            {
                AlgumParamPreenchido = true;
                filtroProdutos = filtroProdutos.Where(x => x.Valor >= ParametrosBusca.ValorInicio);
            }
            if (ParametrosBusca.ValorFim != null && ParametrosBusca.ValorFim != 0)
            {
                AlgumParamPreenchido = true;
                filtroProdutos = filtroProdutos.Where(x => x.Valor <= ParametrosBusca.ValorFim);
            }
            if (ParametrosBusca.Nome != null && ParametrosBusca.Nome != "")
            {
                AlgumParamPreenchido = true;
                filtroProdutos = filtroProdutos.Where(x => x.Nome.ToUpper().Contains(ParametrosBusca.Nome.ToUpper()));
            }

            /*
                Se a variavel de verificação dos criterios de consulta vier como falsa, significa que não
                foi aplicado nenhum criterio de consulta. Não faz sentido salvar isso.
            */
            if (AlgumParamPreenchido)
            {
                if (UsuarioUtils.Usuario != null && UsuarioUtils.Usuario.Nome != "Admin")
                {
                    ParametrosBusca.Usuario = UsuarioUtils.Usuario;
                    ParametrosBusca.DataHoraBusca = DateTime.Now;
                    ConfigDB.Instance.BuscaProdutoRepository.Gravar(ParametrosBusca);
                }
            }
            
            return View(filtroProdutos.ToList());
        }

        

        public ActionResult BuscaAvancada()
        {
            var Categorias = ConfigDB.Instance.CategoriaRepository.GetAll().ToList();
            var lstCategorias = new SelectList(Categorias, "Id", "Nome");
            ViewBag.lstCategorias = lstCategorias;

            var Fabricantes = ConfigDB.Instance.FabricanteRepository.GetAll().ToList();
            var lstFabricantes = new SelectList(Fabricantes, "Id", "Nome");
            ViewBag.lstFabricantes = lstFabricantes;

            var ParametrosBusca = new BuscaProduto();

            if (TempData["UtilizarConsulta"] != null)
            {
                ParametrosBusca = (BuscaProduto)TempData["UtilizarConsulta"];
            }

            return View(ParametrosBusca);
        }

        public ActionResult Comprar(int IdProduto)
        {
            var produto = ConfigDB.Instance.ProdutoRepository.BuscaPorId(IdProduto);
            if (produto == null)
                return RedirectToAction("Index", "Home");

            return View(produto);
        }

    }
}