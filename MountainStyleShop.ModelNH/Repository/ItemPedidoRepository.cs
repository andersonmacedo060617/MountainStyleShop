using MountainStyleShop.ModelNH.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainStyleShop.ModelNH.Repository
{
    public class ItemPedidoRepository:RepositoryBase<ItemNotaCompraFornecedor>
    {
        public ItemPedidoRepository(ISession session) : base(session)
        {
        }


    }
}
