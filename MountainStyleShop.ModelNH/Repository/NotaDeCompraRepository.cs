using MountainStyleShop.ModelNH.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainStyleShop.ModelNH.Repository
{
   
    public class NotaDeCompraRepository
    {
        private ISession session;
        public NotaDeCompraRepository(ISession session)
        {
            this.session = session;
        }

        public IList<NotaDeCompra> GetAll()
        {
            var notaDeCompra = this.session.CreateCriteria<NotaDeCompra>().List<NotaDeCompra>();
            return notaDeCompra;
        }

        public bool Gravar(NotaDeCompra notaDeCompra)
        {
            try
            {
                session.Clear();
                var transacao = this.session.BeginTransaction();
                this.session.SaveOrUpdate(notaDeCompra);


                transacao.Commit();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Excluir(NotaDeCompra notaDeCompra)
        {
            try
            {
                session.Clear();
                var transacao = this.session.BeginTransaction();
                this.session.Delete(notaDeCompra);

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
