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
    public class CategoriasInteresseController : Controller
    {
        // GET: CategoriasInteresse
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Gravar(string[] idCategorias)
        {
            if(UsuarioUtils.Usuario != null)
            {
                ConfigDB.Instance.CategoriasInteresseRepository.ApagaCategoriaInteressesUsuario(
                    UsuarioUtils.Usuario.Id
                    );
                if(idCategorias != null)
                {
                    foreach (var idCat in idCategorias)
                    {
                        CategoriasInteresse catInteresse = new CategoriasInteresse();
                        catInteresse.Categoria = ConfigDB.Instance.CategoriaRepository.BuscaPorId(Int32.Parse(idCat));
                        catInteresse.Usuario = ConfigDB.Instance.UsuarioRepository.BuscaPorId(UsuarioUtils.Usuario.Id);
                        ConfigDB.Instance.CategoriasInteresseRepository.Gravar(catInteresse);
                    }
                }

                return RedirectToAction("Configuracoes", "Usuario");
            }


            return RedirectToAction("Index", "Home");
        }
    }
}