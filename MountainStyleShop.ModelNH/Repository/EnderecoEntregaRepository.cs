using MountainStyleShop.ModelNH.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainStyleShop.ModelNH.Repository
{
    public class EnderecoEntregaRepository : RepositoryBase<EnderecoEntrega>
    {
        public EnderecoEntregaRepository(ISession session) : base(session)
        {

        }

        
        public bool Gravar(EnderecoEntrega entity)
        {
            this.session.Clear();
            entity.DescricaoEndereco = entity.DescricaoEnderecoStr();
            var transacao = this.session.BeginTransaction();
            this.session.SaveOrUpdate(entity);
            transacao.Commit();
            return true;
        }
    }
}
