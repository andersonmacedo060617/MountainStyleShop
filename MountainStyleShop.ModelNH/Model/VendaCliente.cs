using MountainStyleShop.ModelNH.ENum;
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
        public virtual double ValorFinal { get; set; }

        public virtual CupomDesconto CupomDesconto { get; set; }

        public VendaCliente()
        {
            this.ItensVendaCliente = new List<ItemVendaCliente>();
            this.CustosAdicionaisVenda = new List<CustoAddVendaCliente>();
            this.ValoresPagamentoVendaCliente = new List<ValoresPagamentoVendaCliente>();
        }

        public virtual double ValorTotalVenda()
        {
            double valorTotal = this.ValorTotalItens();
            valorTotal = valorTotal + this.ValorTotalCustosAdicionais();
            valorTotal = valorTotal + ValorFrete;
            
            return valorTotal;
        }

        public virtual double ValorComDesconto()
        {
            var vlTotal = this.ValorTotalVenda();
            if(this.CupomDesconto != null)
            {
                vlTotal = vlTotal - ValorDesconto();
            }
            return vlTotal;
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

        public virtual double ValorDesconto()
        {
            double valorDesconto = 0;
            if (this.CupomDesconto.TipoDesconto == ETipoDesconto.Percentual)
            {
                valorDesconto = (this.ValorTotalVenda() * (this.CupomDesconto.Valor / 100));
            }

            if (this.CupomDesconto.TipoDesconto == ETipoDesconto.Valor)
            {
                valorDesconto = this.CupomDesconto.Valor;
            }

            return valorDesconto;
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

        public virtual String ValorDescontoVendaStr()
        {
            String ValorDesconto = "R$0,00";
            if(CupomDesconto != null)
            {
                if(this.CupomDesconto.TipoDesconto == ETipoDesconto.Percentual)
                {
                    ValorDesconto = this.CupomDesconto.Valor + "% - R$" + this.ValorDesconto().ToString("N2");
                }

                if (this.CupomDesconto.TipoDesconto == ETipoDesconto.Valor)
                {
                    ValorDesconto = "R$" + this.CupomDesconto.Valor.ToString("N2");
                }
            }

            return ValorDesconto;
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
            Property<double>(x => x.ValorFinal);

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

            ManyToOne<CupomDesconto>(x => x.CupomDesconto, m =>
            {
                m.Column("CupomDesconto");
            });
        }
    }


}