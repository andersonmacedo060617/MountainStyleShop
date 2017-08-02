using MountainStyleShop.ModelNH.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainStyleShop.ModelNH.Repository
{
    
    public class PessoaRepository:RepositoryBase<Pessoa>
    {
        public PessoaRepository(ISession session) : base(session)
        {
        }

        public IList<Pessoa> GetLimit(int quantidade)
        {
            var Pessoas = this.session.CreateCriteria<Pessoa>().SetMaxResults(quantidade).List<Pessoa>();
            return Pessoas;
        }
        
    }
}
