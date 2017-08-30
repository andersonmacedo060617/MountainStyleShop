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
        public ActionResult Logar(Usuario usuario, String ReturnUrl)
        {
            
            if (UsuarioUtils.Usuario != null)
            {
                if (UsuarioUtils.Logar(usuario.Login, usuario.Senha))
                {
                    string decodedUrl = "";
                    if (!string.IsNullOrEmpty(ReturnUrl))
                        decodedUrl = Server.UrlDecode(ReturnUrl);

                    if (Url.IsLocalUrl(decodedUrl))
                    {
                        return Redirect(decodedUrl);
                    }
                    else
                    {
                        return UsuarioUtils.Usuario.Admin ? RedirectToAction("Index", "Administrativo") : RedirectToAction("Index", "Home");
                    }

                    
                }
            }
            ModelState.Remove("Nome");
            if (ModelState.IsValid)
            {
                if (UsuarioUtils.Logar(usuario.Login, usuario.Senha))
                {
                    string decodedUrl = "";
                    if (!string.IsNullOrEmpty(ReturnUrl))
                        decodedUrl = Server.UrlDecode(ReturnUrl);

                    if (Url.IsLocalUrl(decodedUrl))
                    {
                        return Redirect(decodedUrl);
                    }
                    else
                    {
                        return UsuarioUtils.Usuario.Admin ? RedirectToAction("Index", "Administrativo") : RedirectToAction("Index", "Home");
                    }
                }
            }
            TempData["MSG_FalhaAcesso"] = "Login ou senha invalidos!";

            return RedirectToAction("Login", "Home");
        }

        [Authorize(Roles = "Administrador, Usuario")]
        public ActionResult LogOut()
        {
            UsuarioUtils.Deslogar();

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            var Usuarios = ConfigDB.Instance.UsuarioRepository.GetAll();

            return View(Usuarios);
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult Novo()
        {
            return View();
        }

        [Authorize(Roles = "Administrador, Usuario")]
        [HttpPost]
        public ActionResult Gravar(Usuario Usuario)
        {
            
            Usuario.CriptografaSenha();
            ConfigDB.Instance.UsuarioRepository.Gravar(Usuario);

            return RedirectToAction("Index");
        }
        
        
    }
}