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
        public virtual List<ItemVendaCliente> ItensVendaCliente { get; set; }
        public virtual List<CustoAddVendaCliente> CustosAdicionaisVenda { get; set; }
        public virtual List<ValoresPagamentoVendaCliente> ValoresPagamentoVendaCliente { get; set; }
        public virtual double ValorFrete { get; set; }
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

            ManyToOne<Usuario>(x => x.Cliente, m =>
            {
                m.Column("IdUsuario");
            });

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