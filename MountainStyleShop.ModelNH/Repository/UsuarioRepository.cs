using MountainStyleShop.ModelNH.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainStyleShop.ModelNH.Repository
{
    public class UsuarioRepository
    {
        private ISession session;
        public UsuarioRepository(ISession session)
        {
            this.session = session;
        }

        public IList<Usuario> GetAll()
        {
            var categorias = this.session.CreateCriteria<Usuario>().List<Usuario>();
            return categorias;
        }

        public bool Gravar(Usuario usuario)
        {
            try
            {
                session.Clear();
                var transacao = this.session.BeginTransaction();
                this.session.SaveOrUpdate(usuario);

                transacao.Commit();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Excluir(Usuario usuario)
        {
            try
            {
                session.Clear();
                var transacao = this.session.BeginTransaction();
                this.session.Delete(usuario);

                transacao.Commit();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
