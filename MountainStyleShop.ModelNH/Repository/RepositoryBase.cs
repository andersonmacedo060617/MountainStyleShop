using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainStyleShop.ModelNH.Repository
{
    public abstract class RepositoryBase<T> where T : class
    {
        protected ISession session = null;

        public RepositoryBase(ISession session)
        {
            this.session = session;
        }

        public void Start()
        {
            this.session.BeginTransaction();
        }

        public bool Gravar(T entity)
        {

            var transacao = this.session.BeginTransaction();
            this.session.SaveOrUpdate(entity);
            transacao.Commit();
            return true;
        }

        public bool Excluir(T entity)
        {
            this.session.Clear();
            var transacao = this.session.BeginTransaction();
            this.session.Delete(entity);
            transacao.Commit();
            return true;
        }

        public IList<T> GetAll()
        {
            return this.session.CreateCriteria<T>().List<T>();
        }

        public T BuscaPorId(int id)
        {

            return this.session.Get<T>(id);
        }
    }
}
