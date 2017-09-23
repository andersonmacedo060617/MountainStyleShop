using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace MountainStyleShop.ModelNH.Model
{
    public class CategoriasInteresse
    {
        public virtual int Id { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Categoria Categoria { get; set; }

    }

    public class CategoriasInteresseMap : ClassMapping<CategoriasInteresse>
    {
        public CategoriasInteresseMap()
        {
            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });

            ManyToOne<Categoria>(x => x.Categoria, m =>
            {
                m.Column("IdCategoria");
            });

            ManyToOne<Usuario>(x => x.Usuario, m =>
            {
                m.Column("IdUsuario");
            });

        }
    }
}