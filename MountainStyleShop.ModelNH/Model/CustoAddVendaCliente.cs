using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;

namespace MountainStyleShop.ModelNH.Model
{
    public class CustoAddVendaCliente
    {
        public virtual int Id { get; set; }
        public virtual DateTime DataBase { get; set; }
        public virtual double Valor { get; set; }
        public virtual TipoValorAdd TipoValor { get; set; }
    }

    public class CustoAddVendaClienteMap : ClassMapping<CustoAddVendaCliente>
    {
        public CustoAddVendaClienteMap()
        {
            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });

            Property<DateTime>(x => x.DataBase);
            Property<double>(x => x.Valor);

            ManyToOne<TipoValorAdd>(x => x.TipoValor, m =>
            {
                m.Column("IdTipoValorAdd");
            });

        }
    }
}