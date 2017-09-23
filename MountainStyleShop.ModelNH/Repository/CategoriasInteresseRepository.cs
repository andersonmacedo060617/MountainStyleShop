using MountainStyleShop.ModelNH.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainStyleShop.ModelNH.Repository
{
    public class CategoriasInteresseRepository:RepositoryBase<CategoriasInteresse>
    {
        public CategoriasInteresseRepository(ISession session):base(session)
        {
        }

        public void ApagaCategoriaInteressesUsuario(int IdUsuario)
        {
            this.session.Clear();
            var transacao = this.session.BeginTransaction();
            var lstCategoriasInteresses = this.GetAll().Where(x => x.Usuario.Id == IdUsuario);
            foreach (var Interesse in lstCategoriasInteresses)
            {
                this.session.Delete(Interesse);
            }
            transacao.Commit();
        }
    }
}
