using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;

namespace MountainStyleShop.ModelNH.Model
{
    public class Pagamento
    {
        public virtual int Id { get; set; }
        public virtual double Valor { get; set; }
        public virtual int NumeroParcela { get; set; }
        public virtual DateTime DataVencimento { get; set; }
        public virtual DateTime DataPagamento { get; set; }
        public virtual bool Pago { get; set; }
        public virtual ValoresPagamentoVendaCliente ValorPagamentoVendaCliente { get; set; }
    }

    public class PagamentoMap : ClassMapping<Pagamento>
    {
        public PagamentoMap()
        {
            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });

            Property<double>(x => x.Valor);
            Property<int>(x => x.NumeroParcela);
            Property<DateTime>(x => x.DataVencimento);
            Property<DateTime>(x => x.DataPagamento);
            Property<bool>(x => x.Pago);

            ManyToOne<ValoresPagamentoVendaCliente>(x => x.ValorPagamentoVendaCliente, m =>
            {
                m.Column("IdValoresPagamentoVendaCliente");
            });

        }
    }
}