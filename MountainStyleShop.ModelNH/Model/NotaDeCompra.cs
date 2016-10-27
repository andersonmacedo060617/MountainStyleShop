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
    public class NotaDeCompra
    {
        public virtual int Id { get; set; }

        [Display(Name = "Data da Compra")]
        [Required(ErrorMessage = "A Data da Compra é obrigatoria.")]
        [DataType(dataType: DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}", NullDisplayText = "Data Invalida")]
        public virtual DateTime DataDaCompra { get; set; }

        [Display(Name = "Data Prevista da Entrega")]
        [Required(ErrorMessage = "A Data da Entrega é obrigatoria.")]
        [DataType(dataType: DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}", NullDisplayText = "Data Invalida")]
        public virtual DateTime DataDeEntrega { get; set; }

        [Display(Name = "Valor Total da Nota")]
        [DisplayFormat(DataFormatString = "{0:n2}",
            ApplyFormatInEditMode = true)]
        public virtual Double ValorTotal { get; set; }

        [Display(Name = "Data Prevista da Entrega")]
        [Required(ErrorMessage = "A Data da Entrega é obrigatoria.")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}", NullDisplayText = "Data Invalida")]
        public virtual DateTime DataDeCadastro { get; set; }

        [Display(Name = "Itens da Compra")]
        public virtual IList<ItemPedido> ItensPedidos{get; set;}

        [Display(Name = "Fornecedor")]
        public virtual Pessoa Fornecedor { get; set; }
        
        public NotaDeCompra()
        {
            ItensPedidos = new List<ItemPedido>();
        }

    }

    public class NotaDeCompraMap : ClassMapping<NotaDeCompra>
    {
        public NotaDeCompraMap()
        {
            Table("NotaDeCompra");

            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });


            Property<DateTime>(x => x.DataDaCompra);
            Property<DateTime>(x => x.DataDeEntrega);
            Property<DateTime>(x => x.DataDeCadastro);
            Property<Double>(x => x.ValorTotal);

           

            ManyToOne<Pessoa>(x => x.Fornecedor, m =>
            {
                m.Column("idPessoa_Fornecedor");
            });

            Bag<ItemPedido>(x => x.ItensPedidos, m =>
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
