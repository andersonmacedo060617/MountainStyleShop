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
    public class ItemPedido
    {
        public virtual int Id { get; set; }

        [Display(Name = "Valor Unitario para compra")]
        [Required(ErrorMessage = "O Valor Unitario é Obrigatorio.")]
        [Range(0, 1000000, ErrorMessage ="O Valor Unitario deve estar entre 0 e 1.000.000")]
        [DisplayFormat(DataFormatString = "{0:n2}",
            ApplyFormatInEditMode = true,
            NullDisplayText = "Sem preço")]
        public virtual Double ValorUnitario { get; set; }

        [Display(Name = "Quantidade do produto")]
        [Required(ErrorMessage = "É Obrigatorio informar a quantidade.")]
        [Range(1, 1000000, ErrorMessage = "A quantidade deve estar entre 1 e 1.000.000")]
        public virtual int Quantidade { get; set; }

        [Display(Name ="Nota de Compra")]
        public virtual NotaDeCompra NotaDeCompra { get; set; }

        [Display(Name = "Produto")]
        public virtual Produto Produto { get; set; }

        [Display(Name ="Detalhes do Custo")]
        public virtual IList<DetalhaCusto> DetalhamentoValor { get; set; }


        public ItemPedido()
        {
            DetalhamentoValor = new List<DetalhaCusto>();
        }
    }

    public class ItemPedidoMap : ClassMapping<ItemPedido>
    {
        public ItemPedidoMap()
        {
            Table("ItemPedido");

            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });


            Property<Double>(x => x.ValorUnitario);
            Property<int>(x => x.Quantidade);

            ManyToOne<NotaDeCompra>(x => x.NotaDeCompra, m =>
            {
                m.Column("IdNotaDeCompra");
            });

            ManyToOne<Produto>(x => x.Produto, m =>
            {
                m.Column("IdProduto");
            });

            Bag<DetalhaCusto>(x => x.DetalhamentoValor, m =>
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
