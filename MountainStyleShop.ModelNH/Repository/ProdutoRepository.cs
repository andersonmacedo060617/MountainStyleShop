using MountainStyleShop.ModelNH.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainStyleShop.ModelNH.Repository
{
    public class ProdutoRepository:RepositoryBase<Produto>
    {
        public ProdutoRepository(ISession session) : base(session)
        {
        }

        public IList<Produto> GetLimit(int quantidade)
        {
            var produtos = this.session.CreateCriteria<Produto>().SetMaxResults(quantidade).List<Produto>();
            return produtos;
        }

       
    }
}
