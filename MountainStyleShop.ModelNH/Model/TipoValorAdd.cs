using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MountainStyleShop.ModelNH.Model
{
    public class TipoValorAdd
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Descricao { get; set; }
        public virtual bool Imposto { get; set; }
        public virtual bool CustoParaTransporte { get; set; }
    }

    public class TipoValorAddMap : ClassMapping<TipoValorAdd>
    {
        public TipoValorAddMap()
        {
            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });

            Property<string>(x => x.Nome);
            Property<string>(x => x.Descricao);
            Property<bool>(x => x.Imposto);
            Property<bool>(x => x.CustoParaTransporte);

        }
    }
}