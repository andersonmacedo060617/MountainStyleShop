using MountainStyleShop.ModelNH.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainStyleShop.ModelNH.Repository
{
    
    public class PessoaRepository
    {
        private ISession session;
        public PessoaRepository(ISession session)
        {
            this.session = session;
        }

        public IList<Pessoa> GetLimit(int quantidade)
        {
            var produtos = this.session.CreateCriteria<Pessoa>().SetMaxResults(quantidade).List<Pessoa>();
            return produtos;
        }

        public IList<Pessoa> GetAll()
        {
            var pessoas = this.session.CreateCriteria<Pessoa>().List<Pessoa>();
            return pessoas;
        }

        public bool Gravar(Pessoa pessoa)
        {
            try
            {
                session.Clear();
                var transacao = this.session.BeginTransaction();
                this.session.SaveOrUpdate(pessoa);

                transacao.Commit();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Excluir(Pessoa pessoa)
        {
            try
            {
                session.Clear();
                var transacao = this.session.BeginTransaction();
                this.session.Delete(pessoa);

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
