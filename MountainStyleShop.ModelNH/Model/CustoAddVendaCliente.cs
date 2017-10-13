using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;

namespace MountainStyleShop.ModelNH.Model
{
    public class CustoAddVendaCliente
    {
        public virtual int Id { get; set; }
        public virtual double Valor { get; set; }
        public virtual TipoValorAdd TipoValor { get; set; }
        public virtual VendaCliente VendaCliente { get; set; }
    }

    public class CustoAddVendaClienteMap : ClassMapping<CustoAddVendaCliente>
    {
        public CustoAddVendaClienteMap()
        {
            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });

            Property<double>(x => x.Valor);

            ManyToOne<VendaCliente>(x => x.VendaCliente, m=>
            {
                m.Column("IdVendaCliente");
            });

            ManyToOne<TipoValorAdd>(x => x.TipoValor, m =>
            {
                m.Column("IdTipoValorAdd");
            });
        }
    }
}