using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainStyleShop.ModelNH.Model
{
    public class NotaDeCompra
    {
        public virtual int Id { get; set; }

        public virtual DateTime DataDaCompra { get; set; }

        public virtual DateTime DataDeEntrega { get; set; }

        public virtual Double ValorTotal { get; set; }

        public virtual DateTime DataDeCadastro { get; set; }

        public virtual IList<ItemPedido> ItensPedidos{get; set;}

        public virtual Usuario UsuarioCadastro { get; set; }

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

            ManyToOne<Usuario>(x => x.UsuarioCadastro, m =>
            {
                m.Column("idUsuarioCadastro");
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
