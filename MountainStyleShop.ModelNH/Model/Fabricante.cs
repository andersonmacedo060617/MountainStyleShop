using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainStyleShop.ModelNH.Model
{
    public class Fabricante
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual Pais Pais { get; set; }
        public virtual IList<Produto> Produtos { get; set; }
    }

    public class FabricanteMap : ClassMapping<Fabricante>
    {
        public FabricanteMap()
        {
            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });

            Property<string>(x => x.Nome);
            
            ManyToOne<Pais>(x => x.Pais, m =>
            {
                m.Column("IdPais");
            });


        }
    }
}
