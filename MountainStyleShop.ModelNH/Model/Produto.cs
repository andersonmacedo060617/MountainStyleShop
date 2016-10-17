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
    public class Produto
    {
        public virtual int Id { get; set; }
        
        [Display(Name = "Nome do Produto")]
        [Required(ErrorMessage = "O Nome é Obrigatorio.")]
        public virtual string Nome { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "A Descrição é Obrigatorio.")]
        public virtual string Descricao { get; set; }
        public virtual string Imagem { get; set; }

        [Display(Name = "Valor de venda:")]
        [Required(ErrorMessage = "O Valor é Obrigatorio.")]
        [Range(0.01, 99999.99, ErrorMessage = "O Preço de Venda deve estar entre 10,00 e 99999,99.")]
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
