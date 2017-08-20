using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainStyleShop.ModelNH.Model
{
    public class ProdutoFavorito
    {
        public virtual int Id { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual Usuario Usuario { get; set; }

    }

    public class ProdutoFavoritoMap : ClassMapping<ProdutoFavorito>
    {
        public ProdutoFavoritoMap()
        {
            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });

            ManyToOne<Produto>(x => x.Produto, m =>
            {
                m.Column("IdProduto");
            });

            ManyToOne<Produto>(x => x.Produto, m =>
            {
                m.Column("IdUsuario");
            });
        }
    }
}
