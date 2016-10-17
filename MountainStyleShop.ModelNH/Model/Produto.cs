using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainStyleShop.ModelNH.Model
{
    public class Produto
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Descricao { get; set; }
        public virtual string Imagem { get; set; }
        public virtual Double Valor { get; set; }
        public virtual Categoria Categoria { get; set; }

        
    }

    public class ProdutoMap : ClassMapping<Produto>
    {
        public ProdutoMap()
        {
            Table("Produto");

            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });

            Property<string>(x => x.Nome);
            Property<string>(x => x.Descricao);
            Property<string>(x => x.Imagem);
            Property<Double>(x => x.Valor);

            ManyToOne<Categoria>(x => x.Categoria, m =>
            {
                m.Column("IdCategoria");
            });
        }
    }
}
