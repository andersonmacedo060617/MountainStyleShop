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
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [HttpPost]
        public ActionResult Logar(Usuario usuario)
        {

            if (UsuarioUtils.Usuario != null)
            {
                if (UsuarioUtils.Logar(usuario.Login, usuario.Senha))
                {
                    return RedirectToAction("Principal", "Home");
                }
            }

            if (ModelState.IsValid)
            {
                if (UsuarioUtils.Logar(usuario.Login, usuario.Senha))
                {
                    return RedirectToAction("Principal", "Home");
                }
            }
            TempData["MSG_FalhaAcesso"] = "Login ou senha invalidos!";

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Administrador, Usuario")]
        public ActionResult LogOut()
        {
            UsuarioUtils.Deslogar();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Index()
        {
            var Usuarios = ConfigDB.Instance.UsuarioRepository.GetAll();

            return View(Usuarios);
        }

        public ActionResult Novo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Gravar(Usuario Usuario)
        {
            Usuario.Ativo = true;
            ConfigDB.Instance.UsuarioRepository.Gravar(Usuario);

            return RedirectToAction("Index");
        }
    }
}