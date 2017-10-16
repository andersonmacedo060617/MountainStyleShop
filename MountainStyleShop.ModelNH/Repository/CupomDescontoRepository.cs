using MountainStyleShop.ModelNH.Model;
using MountainStyleShop.ModelNH.Repository;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainStyleShop.ModelNH.Repository
{
    public class CupomDescontoRepository : RepositoryBase<CupomDesconto>
    {
        public CupomDescontoRepository(ISession session):base(session)
        {
        }
    }
}
