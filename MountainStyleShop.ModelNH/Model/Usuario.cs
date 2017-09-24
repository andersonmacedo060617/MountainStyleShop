using MountainStyleShop.ModelNH.Config;
using MountainStyleShop.ModelNH.Utils;
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
    
        public virtual string Senha { get; set; }
        
        public virtual bool Admin { get; set; }

        public virtual bool Ativo { get; set; }

        public virtual IList<BuscaProduto> HistoricoBusca { get; set; }

        public virtual IList<AvaliacaoProduto> AvaliacoesProdutos { get; set; }

        public virtual IList<ProdutoFavorito> ProdutosFavoritos { get; set; }

        public virtual IList<VendaCliente> VendasCliente { get; set; }

        public virtual string CPF { get; set; }

        public virtual IList<EnderecoEntrega> Enderecos { get; set; }

        public virtual IList<CategoriasInteresse> CategoriasInteresse { get; set; }

        public virtual bool SenhaValida(string Senha)
        {
            return Criptografia.Comparar(Senha, this.Senha);
        }

        public virtual void CriptografaSenha()
        {
            this.Senha = Criptografia.CodificaMD5(this.Senha);
        }

        public Usuario()
        {
            this.Admin = false;
        }

        public virtual bool EhCategoriaInteresse(int IdCategoria)
        {
            return ConfigDB.Instance.CategoriasInteresseRepository.GetAll().
                Where(x => x.Usuario.Id == this.Id && x.Categoria.Id == IdCategoria).Count() > 0;
        }

        public virtual List<Categoria> CategoriasFavoritas()
        {
            List<CategoriasInteresse> catInteresses = ConfigDB.Instance.CategoriasInteresseRepository.GetAll().Where(x => x.Usuario.Id == this.Id).ToList();
            List<Categoria> lstCategorias = new List<Categoria>();
            foreach (CategoriasInteresse cat in catInteresses) 
            {
                lstCategorias.Add(cat.Categoria);
            }

            return lstCategorias;
        }
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
            Property<string>(x => x.CPF);
            Property<bool>(x => x.Ativo);

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

            Bag<EnderecoEntrega>(x => x.Enderecos, m =>
            {
                m.Cascade(Cascade.All);
                m.Lazy(CollectionLazy.Lazy);
                m.Inverse(true);
            },
                r => r.OneToMany()
            );

            Bag<CategoriasInteresse>(x=> x.CategoriasInteresse, m =>
            {
                m.Cascade(Cascade.All);
                m.Lazy(CollectionLazy.Lazy);
                m.Inverse(true);
            },
                r => r.OneToMany()
            );

            Bag<BuscaProduto>(x => x.HistoricoBusca, m =>
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
