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
    public class AvaliacaoProduto
    {
        public virtual int Id { get; set; }

        [Display(Name = "Texto da avaliação")]
        [Required(ErrorMessage = "O Texto é Obrigatorio.")]
        public virtual String Texto { get; set; }
        
        public virtual int NotaAvaliacao { get; set; }

        public virtual Produto Produto { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual DateTime Data { get; set; }
    }

    public class AvaliacaoProdutoMap : ClassMapping<AvaliacaoProduto>
    {
        public AvaliacaoProdutoMap()
        {
            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });

            Property<String>(x => x.Texto);
            Property<int>(x => x.NotaAvaliacao);
            Property<DateTime>(x => x.Data);

            ManyToOne<Produto>(x => x.Produto, m =>
            {
                m.Column("IdProduto");
            });

            ManyToOne<Usuario>(x => x.Usuario, m =>
            {
                m.Column("IdUsuario");
            });
        }

    }
}
