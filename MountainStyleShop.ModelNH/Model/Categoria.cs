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
    public class Categoria
    {

        public virtual int Id { get; set; }
        [Display(Name="Nome da Categoria")]
        [Required(ErrorMessage ="O Nome é Obrigatorio.")]
        public virtual string Nome { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "A Descrição é Obrigatoria.")]
        public virtual string Descricao { get; set; }

        public virtual IList<Produto> Produtos { get; set; }

        public Categoria()
        {
            this.Produtos = new List<Produto>();
        }
    }

    public class CategoriaMap : ClassMapping<Categoria>
    {
        public CategoriaMap()
        {
            Table("Cateogoria");

            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });

            Property<string>(x => x.Nome);
            Property<string>(x => x.Descricao);

            Bag<Produto>(x => x.Produtos, m =>
              {
                  m.Cascade(Cascade.All);
                  m.Lazy(CollectionLazy.Lazy);
                  m.Inverse(true);
              },
                r=>r.OneToMany()
           );
            
        }
    }
}
