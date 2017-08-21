using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System.Collections.Generic;

namespace MountainStyleShop.ModelNH.Model
{
    public class Pais
    {
        public virtual int Id { get; set; }

        public virtual string Nome { get; set; }

        public virtual IList<UF> UFs { get; set; }
    }

    public class PaisMap : ClassMapping<Pais>
    {
        public PaisMap()
        {
            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });

            Property<string>(x => x.Nome);

            Bag<UF>(x => x.UFs, m =>
            {
                m.Cascade(Cascade.All);
                m.Lazy(CollectionLazy.Lazy);
                m.Inverse(true);
            },
                r => r.OneToMany()
           );
        }
    }
}