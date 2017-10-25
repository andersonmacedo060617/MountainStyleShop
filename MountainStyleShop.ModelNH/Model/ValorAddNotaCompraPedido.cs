using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainStyleShop.ModelNH.Model
{
    public class ValorAddNotaCompraPedido
    {
        public virtual int Id { get; set; }
        public virtual DateTime DataDeBase { get; set; }
        public virtual double Valor { get; set; }
        public virtual TipoValorAdd TipoValor { get; set; }
        public virtual ItemNotaCompraFornecedor ItemNotaCompraFornecedor { get; set;}
    }

    public class ValorAddNotaCompraPedidoMap : ClassMapping<ValorAddNotaCompraPedido>
    {
        public ValorAddNotaCompraPedidoMap()
        {
            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });

            Property<DateTime>(x => x.DataDeBase);
            Property<double>(x => x.Valor);

            ManyToOne<TipoValorAdd>(x => x.TipoValor, m =>
            {
                m.Column("TipoValor");
            });

            ManyToOne<ItemNotaCompraFornecedor>(x => x.ItemNotaCompraFornecedor, m =>
            {
                m.Column("ItemNotaCompraFornecedor");
            });

        }
    }
}
