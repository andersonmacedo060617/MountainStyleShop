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
    public class DetalhaCusto
    {
        public virtual int Id { get; set; }

        [Display(Name = "Nome do Detalhe do Custo")]
        [Required(ErrorMessage = "O Nome é Obrigatorio.")]
        public virtual string Nome { get; set; }

        [Display(Name = "Tipo do Detalhamento")]
        public virtual TipoDetalhamento Tipo { get; set; }

        [Display(Name = "Nome do Detalhe do Custo")]
        [Required(ErrorMessage = "O Nome é Obrigatorio.")]
        [Range(0.01, 100, ErrorMessage ="O percentual deve estar entre 0.01 á 100" )]
        public virtual Double Percentual { get; set; }

        [Display(Name = "Item")]
        public virtual ItemPedido Item { get; set; }

    }

    public class DetalhaCustoMap : ClassMapping<DetalhaCusto>
    {
        public DetalhaCustoMap()
        {
            Table("DetalhaCusto");

            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });

            Property<string>(x => x.Nome);
            Property<Double>(x => x.Percentual);

            ManyToOne<TipoDetalhamento>(x => x.Tipo, m =>
            {
                m.Column("IdTipoDetalhamento");
            });

            ManyToOne<ItemPedido>(x => x.Item, m =>
            {
                m.Column("IdItemPedido");
            });
        }
    }
}
