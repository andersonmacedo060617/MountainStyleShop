using MountainStyleShop.ModelNH.ENum;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainStyleShop.ModelNH.Model
{
    public class ValorAddNotaCompraPedido
    {
        public virtual int Id { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:F}")]
        public virtual double Percentual { get; set; }

        public virtual EOperadorValorAdicional OperadorValorAdicional { get; set; }

        public virtual String NomeValor { get; set; }
        public virtual ItemNotaCompraFornecedor ItemNotaCompraFornecedor { get; set;}

        public virtual double Valor()
        {
            return this.ValorAddCalculado() * this.ItemNotaCompraFornecedor.Quantidade;
        }

        public virtual double ValorAddCalculado()
        { 
            return this.ItemNotaCompraFornecedor.ValorUnitario * (Percentual / 100);
        }

        
    }

    public class ValorAddNotaCompraPedidoMap : ClassMapping<ValorAddNotaCompraPedido>
    {
        public ValorAddNotaCompraPedidoMap()
        {
            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });
            
            Property<double>(x => x.Percentual);
            Property<String>(x => x.NomeValor);
            Property<EOperadorValorAdicional>(x => x.OperadorValorAdicional);


            ManyToOne<ItemNotaCompraFornecedor>(x => x.ItemNotaCompraFornecedor, m =>
            {
                m.Column("ItemNotaCompraFornecedor");
            });

        }
    }
}
