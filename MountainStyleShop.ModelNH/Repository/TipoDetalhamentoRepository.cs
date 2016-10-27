using MountainStyleShop.ModelNH.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainStyleShop.ModelNH.Repository
{
   
    public class TipoDetalhamentoRepository
    {
        private ISession session;
        public TipoDetalhamentoRepository(ISession session)
        {
            this.session = session;
        }

        public IList<TipoDetalhamento> GetAll()
        {
            var tiposDetalhamentos = this.session.CreateCriteria<TipoDetalhamento>().List<TipoDetalhamento>();
            return tiposDetalhamentos;
        }

        public bool Gravar(TipoDetalhamento tipoDetalhamento)
        {
            try
            {
                session.Clear();
                var transacao = this.session.BeginTransaction();
                this.session.SaveOrUpdate(tipoDetalhamento);


                transacao.Commit();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Excluir(TipoDetalhamento tipoDetalhamento)
        {
            try
            {
                session.Clear();
                var transacao = this.session.BeginTransaction();
                this.session.Delete(tipoDetalhamento);

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
