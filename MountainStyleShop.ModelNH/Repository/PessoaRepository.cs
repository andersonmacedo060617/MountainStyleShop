using MountainStyleShop.ModelNH.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainStyleShop.ModelNH.Repository
{
    
    public class PessoaRepository:RepositoryBase<Fornecedor>
    {
        public PessoaRepository(ISession session) : base(session)
        {
        }

        public IList<Fornecedor> GetLimit(int quantidade)
        {
            var Fornecedores = this.session.CreateCriteria<Fornecedor>().SetMaxResults(quantidade).List<Fornecedor>();
            return Fornecedores;
        }
        
    }
}
