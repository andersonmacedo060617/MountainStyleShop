using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.ComponentModel.DataAnnotations;

namespace MountainStyleShop.ModelNH.Model
{
    
    public class BuscaProduto
    {
        public virtual int Id { get; set; }

        [Required(AllowEmptyStrings =true)]
        public virtual int IdProduto { get; set; }
        [Required(AllowEmptyStrings = true)]
        public virtual string Nome { get; set; }
        [Required(AllowEmptyStrings = true)]
        public virtual Categoria Categoria { get; set; }
        [Required(AllowEmptyStrings = true)]
        public virtual string Descricao { get; set; }
        [Required(AllowEmptyStrings = true)]
        public virtual Fabricante Fabricante { get; set; }
        [Required(AllowEmptyStrings = true)]
        public virtual double ValorInicio { get; set; }
        [Required(AllowEmptyStrings = true)]
        public virtual double ValorFim { get; set; }
        [Required(AllowEmptyStrings = true)]
        public virtual DateTime DataHoraBusca { get; set; }
        [Required(AllowEmptyStrings = true)]
        public virtual Usuario Usuario { get; set; }
    }

    public class BuscaProdutoMap : ClassMapping<BuscaProduto>
    {
        public BuscaProdutoMap()
        {
            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });

            Property<int>(x => x.IdProduto);
            Property<string>(x => x.Nome);
            Property<string>(x => x.Descricao);
            Property<double>(x => x.ValorInicio);
            Property<double>(x => x.ValorFim);
            Property<DateTime>(x => x.DataHoraBusca);

            ManyToOne<Categoria>(x => x.Categoria, m =>
            {
                m.Column("IdCategoria");
            });

            ManyToOne<Usuario>(x => x.Usuario, m =>
            {
                m.Column("IdUsuario");
            });

            ManyToOne<Fabricante>(x => x.Fabricante, m =>
            {
                m.Column("IdFabricante");
            });

        }
    }
}