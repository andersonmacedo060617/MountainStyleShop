using MountainStyleShop.ModelNH.Config;
using MountainStyleShop.ModelNH.Model;
using MountainStyleShop.ModelNH.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MountainStyleShop.Models
{
    public class UsuarioUtils
    {
        public static Usuario Usuario
        {
            get
            {
                Usuario user = null;
                if (HttpContext.Current.Session["Usuario"] != null)
                {
                    user = (Usuario)HttpContext.Current.Session["Usuario"];
                }

                return user;
            }
        }

        public static bool Logar(string Login, string Senha)
        {
            if(AdministradorDoSistema.Login().Equals(Login) && AdministradorDoSistema.Senha().Equals(Senha))
            {
                var admin = new Usuario()
                {
                    Login = AdministradorDoSistema.Login(),
                    Senha = AdministradorDoSistema.Senha(),
                    Ativo = true,
                    Admin = true,
                };

                FormsAuthentication.SetAuthCookie(admin.Login, false);
                HttpContext.Current.Session["Usuario"] = admin;
                return true;

            }

            var usuario = ConfigDB.Instance.UsuarioRepository.BuscaPorLogin(Login);
            
            if (usuario != null && usuario.Ativo && usuario.SenhaValida(Senha))
            {
                FormsAuthentication.SetAuthCookie(usuario.Login, false);
                HttpContext.Current.Session["Usuario"] = usuario;
                return true;
            }
            return false;
        }

        public static void Deslogar()
        {

            HttpContext.Current.Session["Usuario"] = null;
            HttpContext.Current.Session.Remove("Usuario");
            FormsAuthentication.SignOut();
        }
    }
}