using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System.Collections.Generic;

namespace MountainStyleShop.ModelNH.Model
{
    public class Cidade
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual UF UF { get; set; }
        public virtual IList<ValorEntrega> ValoresEntrega { get; set; }
    }

    public class CidadeMap : ClassMapping<Cidade>
    {
        public CidadeMap()
        {
            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });

            Property<string>(x => x.Nome);

            ManyToOne<UF>(x => x.UF, m =>
            {
                m.Column("UF");
                m.Fetch(FetchKind.Select);
            });

            Bag<ValorEntrega>(x => x.ValoresEntrega, m =>
            {
                m.Cascade(Cascade.All);
                m.Inverse(true);
            },
               r => r.OneToMany()
           );

        }
    }
}