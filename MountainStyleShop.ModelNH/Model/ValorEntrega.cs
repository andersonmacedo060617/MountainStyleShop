using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainStyleShop.ModelNH.Model
{
    public class ValorEntrega
    {
        public virtual int Id { get; set; }
        public virtual string CEP { get; set; }
        public virtual double Valor { get; set; }
        public virtual Cidade Cidade { get; set; }
    }

    public class ValorEntregaMap : ClassMapping<ValorEntrega>
    {
        public ValorEntregaMap()
        {
            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });

            Property<string>(x => x.CEP);
            Property<double>(x => x.Valor);

            ManyToOne<Cidade>(x => x.Cidade, m =>
            {
                m.Column("IdCidade");
            });


        }
    }
}
