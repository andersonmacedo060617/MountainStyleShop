using MountainStyleShop.ModelNH.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainStyleShop.ModelNH.Repository
{
    public class ItemPedidoRepository
    {
        private ISession session;
        public ItemPedidoRepository(ISession session)
        {
            this.session = session;
        }

        public IList<ItemPedido> GetAll()
        {
            var itensPedidos = this.session.CreateCriteria<ItemPedido>().List<ItemPedido>();
            return itensPedidos;
        }

        public bool Gravar(ItemPedido itemPedido)
        {
            try
            {
                session.Clear();
                var transacao = this.session.BeginTransaction();
                this.session.SaveOrUpdate(itemPedido);


                transacao.Commit();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Excluir(ItemPedido itemPedido)
        {
            try
            {
                session.Clear();
                var transacao = this.session.BeginTransaction();
                this.session.Delete(itemPedido);

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
