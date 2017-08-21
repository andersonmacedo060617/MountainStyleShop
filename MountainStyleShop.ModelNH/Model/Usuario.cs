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
    public class Usuario
    {
        public virtual int Id { get; set; }
        [Display(Name = "Nome do Usuario")]
        [Required(ErrorMessage = "O Nome é Obrigatorio.")]
        public virtual string Nome { get; set; }

        [Display(Name = "Login")]
        [Required(ErrorMessage = "O Login é Obrigatorio.")]
        public virtual string Login { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "A senha é Obrigatoria.")]
        [StringLength(maximumLength:400,
            MinimumLength =8, 
            ErrorMessage ="A senha deve conter no minimo 8 caracteres")]
        public virtual string Senha { get; set; }

        public virtual bool Admin { get; set; }

        public virtual bool Ativo { get; set; }

        public virtual IList<AvaliacaoProduto> AvaliacoesProdutos { get; set; }

        public virtual IList<ProdutoFavorito> ProdutosFavoritos { get; set; }

        public virtual IList<VendaCliente> VendasCliente { get; set; }
    }

    public class UsuarioMap : ClassMapping<Usuario>
    {
        public UsuarioMap()
        {
            

            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });

            Property<string>(x => x.Nome);
            Property<string>(x => x.Login);
            Property<string>(x => x.Senha);
            Property<bool>(x => x.Ativo);
            Property<bool>(x => x.Admin);

            Bag<AvaliacaoProduto>(x => x.AvaliacoesProdutos, m =>
            {
                m.Cascade(Cascade.All);
                m.Lazy(CollectionLazy.Lazy);
                m.Inverse(true);
            },
                r => r.OneToMany()
            );

            Bag<ProdutoFavorito>(x => x.ProdutosFavoritos, m =>
            {
                m.Cascade(Cascade.All);
                m.Lazy(CollectionLazy.Lazy);
                m.Inverse(true);
            },
                r => r.OneToMany()
            );

            Bag<VendaCliente>(x => x.VendasCliente, m =>
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
