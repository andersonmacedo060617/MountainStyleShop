using MountainStyleShop.ModelNH.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainStyleShop.ModelNH.Repository
{
    

    public class DetalhaCustoRepository
    {
        private ISession session;
        public DetalhaCustoRepository(ISession session)
        {
            this.session = session;
        }

        public IList<DetalhaCusto> GetAll()
        {
            var detalhesCusto = this.session.CreateCriteria<DetalhaCusto>().List<DetalhaCusto>();
            return detalhesCusto;
        }

        public bool Gravar(DetalhaCusto detalhe)
        {
            try
            {
                session.Clear();
                var transacao = this.session.BeginTransaction();
                this.session.SaveOrUpdate(detalhe);


                transacao.Commit();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Excluir(DetalhaCusto detalhe)
        {
            try
            {
                session.Clear();
                var transacao = this.session.BeginTransaction();
                this.session.Delete(detalhe);

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
