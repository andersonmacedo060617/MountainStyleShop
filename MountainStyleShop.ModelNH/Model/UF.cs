using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainStyleShop.ModelNH.Model
{
    public class UF
    {
        public virtual int Id { get; set; }

        public virtual string Nome { get; set; }

        public virtual string Sigla { get; set; }

        public virtual Pais Pais { get; set; }

        public virtual IList<Cidade> Cidades { get; set; }

        public virtual string Estado_Pais {
            get
            {
                return this.Nome + " - " + Pais.Nome;
            }
        }
    }

    public class UFMap : ClassMapping<UF>
    {
        public UFMap()
        {


            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });


            Property<string>(x => x.Nome);
            Property<string>(x => x.Sigla);

            ManyToOne<Pais>(x => x.Pais, m =>
            {
                m.Column("IdPais");
            });
            

            Bag<Cidade>(x => x.Cidades, m =>
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
