using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainStyleShop.ModelNH.Model
{
    public class AjusteEstoque
    {
        public virtual int Id { get; set; }
        public virtual DateTime DataPerda { get; set; }
        public virtual int Quantidade { get; set; }
        public virtual string MotivoAjuste { get; set; }
        public virtual double ValorUnitario { get; set; }
        public virtual Produto Produto { get; set; }

    }

    public class AjusteEstoqueMap : ClassMapping<AjusteEstoque>
    {
        public AjusteEstoqueMap()
        {
            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });

            Property<DateTime>(x => x.DataPerda);
            Property<int>(x => x.Quantidade);
            Property<string>(x => x.MotivoAjuste);
            Property<double>(x => x.ValorUnitario);

            ManyToOne<Produto>(x => x.Produto, m =>
            {
                m.Column("IdProduto");
            });


        }
    }
}
