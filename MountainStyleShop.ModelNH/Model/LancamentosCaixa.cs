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
    public class LancamentosCaixa
    {
        public virtual int Id { get; set; }
        [Display(Name = "Data do Lançamento")]
        [DataType(dataType: DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}", NullDisplayText = "Data Invalida")]
        public virtual DateTime DataLancamento { get; set; }
        [Display(Name = "Valor do Lançamento")]
        public virtual double ValorLancamento { get; set; }
        [Display(Name = "Entrada ou Saida")]
        public virtual bool Entrada { get; set; }
        public virtual NotaDeCompraFornecedor NotaDeCompra { get; set; }
        public virtual VendaCliente VendaCliente { get; set; }

        [Display(Name = "Descrição do Lançamento")]
        public virtual String Descricao { get; set; }
        

    }

    public class LancamentosCaixaMap : ClassMapping<LancamentosCaixa>
    {
        public LancamentosCaixaMap()
        {
            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });

            Property<DateTime>(x => x.DataLancamento);
            Property<double>(x => x.ValorLancamento);
            Property<bool>(x => x.Entrada);
            Property<String>(x => x.Descricao);

            ManyToOne<NotaDeCompraFornecedor>(x => x.NotaDeCompra, m =>
            {
                m.Column("NotaDeCompra");
            });

            ManyToOne<VendaCliente>(x => x.VendaCliente, m =>
            {
                m.Column("VendaCliente");
            });
        }
    }
}
