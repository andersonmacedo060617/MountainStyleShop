using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainStyleShop.ModelNH.Model
{
    public class Imagem
    {
        public virtual int Id { get; set; }
        public virtual string Caminho { get; set; }
        public virtual bool Principal { get; set; }

        public virtual Produto Produto { get; set; }
    }

    public class ImagemMap : ClassMapping<Imagem>
    {
        public ImagemMap()
        {
            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });

            Property<string>(x => x.Caminho);
            Property<bool>(x => x.Principal);

            ManyToOne<Produto>(x => x.Produto, m =>
            {
                m.Column("IdProduto");
            });
        }
    }
}
