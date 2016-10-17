using MountainStyleShop.ModelNH.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainStyleShop.ModelNH.Repository
{
    public class CategoriaRepository
    {
        private ISession session;
        public CategoriaRepository(ISession session)
        {
            this.session = session;
        }

        public IList<Categoria> GetAll()
        {
            var categorias = this.session.CreateCriteria<Categoria>().List<Categoria>();
            return categorias;
        }

        public bool Gravar(Categoria categoria)
        {
            try
            {
                session.Clear();
                var transacao = this.session.BeginTransaction();
                this.session.SaveOrUpdate(categoria);
                

                transacao.Commit();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Excluir(Categoria categoria)
        {
            try
            {
                session.Clear();
                var transacao = this.session.BeginTransaction();
                this.session.Delete(categoria);

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
