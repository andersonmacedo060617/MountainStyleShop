using MountainStyleShop.ModelNH.Config;
using MountainStyleShop.ModelNH.Model;
using MountainStyleShop.ModelNH.Utils;
using MountainStyleShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MountainStyleShop.RolesSecurity
{
    public class Roles : RoleProvider
    {
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string userName)
        {
            List<string> sRoles = new List<string>();
            
            var user = ConfigDB.Instance.UsuarioRepository.BuscaPorLogin(userName);
            var userSession = (Usuario)HttpContext.Current.Session["Usuario"];
            if (userSession != null && user != null)
            {
                if(userSession.Login.Equals(AdministradorDoSistema.Login()) && userSession.Senha.Equals(AdministradorDoSistema.Senha()))
                {
                    sRoles.Add("Administrador");
                }
                else if (user.Login.Equals(userSession.Login) && userSession != null && user.Ativo)
                {
                    sRoles.Add("Usuario");
                }

                    //Falta Implementar aqui
                    //foreach (var perfil in user.PerfilAcesso)
                    //{
                    //    sRoles.Add(perfil.AcessoController.Nome + ((perfil.AcessoAcao.Nome != null) ? "_" + perfil.AcessoAcao.Nome : ""));
                    //}
            }else if(UsuarioUtils.Usuario != null && UsuarioUtils.Usuario.Login == "Admin")
            {
                sRoles.Add("Administrador");
            }

            string[] retorno = sRoles.ToArray();

            return retorno;

            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}