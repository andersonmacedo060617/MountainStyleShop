using MountainStyleShop.ModelNH.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainStyleShop.ModelNH.Repository
{
    public class UsuarioRepository:RepositoryBase<Usuario>
    {
        public UsuarioRepository(ISession session) : base(session)
        {
        }

        public Usuario BuscaPorLogin(string Login)
        {
            session.Clear();
            var usuario = this.GetAll().Where(x => x.Login == Login).FirstOrDefault();

            return usuario;
        }

        public Usuario BuscaPorLoginSenha(string Login, string Senha)
        {
            session.Clear();
            var usuario = this.GetAll().Where(
                x => x.Login == Login && x.Senha == Senha
                ).FirstOrDefault();

            return usuario;
        }

        
    }
}
