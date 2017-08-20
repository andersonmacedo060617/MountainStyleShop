using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System.Collections;
using System.Collections.Generic;

namespace MountainStyleShop.ModelNH.Model
{
    public class ValoresPagamentoVendaCliente
    {
        public virtual int Id { get; set; }
        public virtual int NumeroParecelas { get; set; }
        public virtual double ValorTotalPagamento { get; set; }
        public virtual VendaCliente VendaCliente { get; set; }
        public virtual FormaPagamento FormaPagamento { get; set; }
        public virtual IList<Pagamento> Pagamentos { get; set; }
    }

    public class ValoresPagamentoVendaClienteMap : ClassMapping<ValoresPagamentoVendaCliente>
    {
        public ValoresPagamentoVendaClienteMap()
        {
            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });

            Property<int>(x => x.NumeroParecelas);
            Property<double>(x => x.ValorTotalPagamento);

            ManyToOne<FormaPagamento>(x => x.FormaPagamento, m =>
            {
                m.Column("IdFormaPagamento");
            });

            ManyToOne<VendaCliente>(x => x.VendaCliente, m =>
            {
                m.Column("IdVendaCliente");
            });

            Bag<Pagamento>(x => x.Pagamentos, m =>
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