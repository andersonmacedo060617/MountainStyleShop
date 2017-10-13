using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;

namespace MountainStyleShop.ModelNH.Model
{
    public class VendaCliente
    {
        public virtual int Id { get; set; }
        public virtual DateTime DataVenda { get; set; }
        public virtual int DiasParaEntrega { get; set; }
        public virtual DateTime DataEntrega { get; set; }
        public virtual bool VendaConfirmada { get; set; }
        public virtual Usuario Cliente { get; set; }
        public virtual EnderecoEntrega EnderecoParaEntrega { get; set; }
        public virtual IList<ItemVendaCliente> ItensVendaCliente { get; set; }
        public virtual IList<CustoAddVendaCliente> CustosAdicionaisVenda { get; set; }
        public virtual IList<ValoresPagamentoVendaCliente> ValoresPagamentoVendaCliente { get; set; }
        public virtual double ValorFrete { get; set; }

        public virtual double ValorTotalVenda()
        {
            double valorTotal = this.ValorTotalItens();
            valorTotal = valorTotal + this.ValorTotalCustosAdicionais();
            valorTotal = valorTotal + ValorFrete ;

            return valorTotal;
        }

        public virtual double ValorTotalItens()
        {
            double valorTotal = 0;
            foreach (var itemVenda in this.ItensVendaCliente)
            {
                valorTotal = valorTotal + itemVenda.ValorTotal();
            }

            return valorTotal;
        }

        public virtual double ValorTotalCustosAdicionais()
        {
            double valorTotal = 0;
            foreach (var custoAdd in this.CustosAdicionaisVenda)
            {
                valorTotal = valorTotal + custoAdd.Valor;
            }

            return valorTotal;
        }

    }

    public class VendaClienteMap : ClassMapping<VendaCliente>
    {
        public VendaClienteMap()
        {
            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });

            Property<DateTime>(x => x.DataVenda);
            Property<int>(x => x.DiasParaEntrega);
            Property<DateTime>(x => x.DataEntrega);
            Property<bool>(x => x.VendaConfirmada);
            Property<double>(x => x.ValorFrete);

            ManyToOne<Usuario>(x => x.Cliente);

            ManyToOne<EnderecoEntrega>(x => x.EnderecoParaEntrega, m =>
            {
                m.Column("IdEnderecoEntrega");
            });

            Bag<ItemVendaCliente>(x => x.ItensVendaCliente, m =>
            {
                m.Cascade(Cascade.All);
                m.Lazy(CollectionLazy.Lazy);
                m.Inverse(true);
            },
                r => r.OneToMany()
            );

            Bag<CustoAddVendaCliente>(x => x.CustosAdicionaisVenda, m =>
            {
                m.Cascade(Cascade.All);
                m.Lazy(CollectionLazy.Lazy);
                m.Inverse(true);
            },
                r => r.OneToMany()
           );

            Bag<ValoresPagamentoVendaCliente>(x => x.ValoresPagamentoVendaCliente, m =>
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