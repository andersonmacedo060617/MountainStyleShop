using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MountainStyleShop.ModelNH.Model
{
    public class ItemVendaCliente
    {
        public virtual int Id { get; set; }
        public virtual int Quantidade { get; set; }
        public virtual double ValorUnitario { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual VendaCliente VendaCliente { get; set; }

        public virtual double ValorTotal()
        {
            return this.Quantidade * this.ValorUnitario;
        }
    }

    public class ItemVendaClienteMap : ClassMapping<ItemVendaCliente>
    {
        public ItemVendaClienteMap()
        {
            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });

            Property<int>(x => x.Quantidade);
            Property<double>(x => x.ValorUnitario);

            ManyToOne<Produto>(x => x.Produto, m =>
            {
                m.Column("IdProduto");
            });

            ManyToOne<VendaCliente>(x => x.VendaCliente, m =>
            {
                m.Column("IdVendaCliente");
            });
        }
    }
}