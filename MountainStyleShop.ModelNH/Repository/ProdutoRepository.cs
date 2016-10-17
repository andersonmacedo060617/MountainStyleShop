using MountainStyleShop.ModelNH.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainStyleShop.ModelNH.Repository
{
    public class ProdutoRepository
    {
        private ISession session;
        public ProdutoRepository(ISession session)
        {
            this.session = session;
        }

        public IList<Produto> GetLimit(int quantidade)
        {
            var produtos = this.session.CreateCriteria<Produto>().SetMaxResults(quantidade).List<Produto>();
            return produtos;
        }

        public IList<Produto> GetAll()
        {
            var produtos = this.session.CreateCriteria<Produto>().List<Produto>();
            return produtos;
        }

        public bool Gravar(Produto produto)
        {
            try
            {
                session.Clear();
                var transacao = this.session.BeginTransaction();
                this.session.SaveOrUpdate(produto);

                transacao.Commit();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Excluir(Produto produto)
        {
            try
            {
                session.Clear();
                var transacao = this.session.BeginTransaction();
                this.session.Delete(produto);

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
