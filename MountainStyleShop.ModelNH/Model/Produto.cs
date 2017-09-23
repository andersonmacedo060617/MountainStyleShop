using MountainStyleShop.ModelNH.Config;
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

        
        public virtual string ImagemPrincipal { get; set; }

        [Display(Name = "Valor de venda:")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Required(ErrorMessage = "O Valor é Obrigatorio.")]
        [Range(0.01, 99999.99, ErrorMessage = "O Preço de Venda deve estar entre 0,01 e 99999,99.")]
        public virtual Double Valor { get; set; }

        public virtual Double Peso { get; set; }
        
        
        public virtual bool ApareeceNaVitrine { get; set; }

        public virtual IList<ProdutoFavorito> ProdutoFavoritos { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual IList<AvaliacaoProduto> AvaliacoesProdutos { get; set; }
        public virtual IList<AjusteEstoque> AjustesDeEstoque { get; set; }
        public virtual IList<ItemNotaCompraFornecedor> ItemNotaDeCompraFornecedor { get; set; }
        public virtual IList<ItemVendaCliente> ItemVendasCliente { get; set; }
        public virtual IList<Imagem> Imagens { get; set; }

        public virtual Fabricante Fabricante { get; set; }

        public virtual bool Ativo { get; set; }

        public virtual double PercentualBom()
        {
            var avaliacoes = ConfigDB.Instance.AvaliacaoProdutoRepository.GetAll()
                .Where(x=>x.Produto.Id == this.Id);
            if(avaliacoes.Count() > 0)
            {
                double bom = avaliacoes.Where(x => x.NotaAvaliacao == 2).Count();
                double total = avaliacoes.Count();
                double percentual = ((bom / total) * 100);

                return Double.Parse(percentual.ToString("N2"));
            }
            else
            {
                return 0;
            }
            
        }

        public virtual double PercentualRuim()
        {
            var avaliacoes = ConfigDB.Instance.AvaliacaoProdutoRepository.GetAll().Where(x => x.Produto.Id == this.Id);
            if (avaliacoes.Count() > 0)
            {
                double ruim = avaliacoes.Where(x => x.NotaAvaliacao == 1).Count();
                double total = avaliacoes.Count();
                double percentual = ((ruim / total) * 100);
                return Double.Parse(percentual.ToString("N2"));
            }
            else
            {
                return 0;
            }
        }

        public virtual double PercentualSemNota()
        {
            var avaliacoes = ConfigDB.Instance.AvaliacaoProdutoRepository.GetAll().Where(x => x.Produto.Id == this.Id);
            if (avaliacoes.Count() > 0)
            {
                double ruim = avaliacoes.Where(x => x.NotaAvaliacao == 0).Count();
                double total = avaliacoes.Count();
                double percentual = ((ruim / total) * 100);
                return Double.Parse(percentual.ToString("N2"));
            }
            else
            {
                return 0;
            }
        }
        
        public virtual int QuantidadeAvaliacoes() {
            return ConfigDB.Instance.AvaliacaoProdutoRepository.GetAll().Where(x=>x.Produto.Id == this.Id).ToList().Count;
        }
        
        public virtual bool DisponivelParaCompra()
        {
            return this.Ativo;
        }

        public virtual int TotalImagens()
        {
            return ConfigDB.Instance.ImagemRepository.GetAll().Where(x=>x.Produto.Id == this.Id).ToList().Count + 1;
        }
    }

    public class ProdutoMap : ClassMapping<Produto>
    {
        public ProdutoMap()
        {
            

            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });

            Property<string>(x => x.Nome);
            Property<string>(x => x.Descricao);
            Property<string>(x => x.ImagemPrincipal);
            Property<bool>(x => x.Ativo);
            Property<Double>(x => x.Valor);
            Property<Double>(x => x.Peso);
            Property<bool>(x => x.ApareeceNaVitrine);


            ManyToOne<Categoria>(x => x.Categoria, m =>
            {
                m.Column("IdCategoria");
            });

            ManyToOne<Fabricante>(x => x.Fabricante, m =>
            {
                m.Column("IdFabricante");
            });

            Bag<AvaliacaoProduto>(x => x.AvaliacoesProdutos, m =>
            {
                m.Cascade(Cascade.All);
                m.Lazy(CollectionLazy.Lazy);
                m.Inverse(true);
            },
                r => r.OneToMany()
           );

            Bag<ProdutoFavorito>(x => x.ProdutoFavoritos, m =>
            {
                m.Cascade(Cascade.All);
                m.Lazy(CollectionLazy.Lazy);
                m.Inverse(true);
            },
                r => r.OneToMany()
            );

            Bag<AjusteEstoque>(x => x.AjustesDeEstoque, m =>
            {
                m.Cascade(Cascade.All);
                m.Lazy(CollectionLazy.Lazy);
                m.Inverse(true);
            },
                r => r.OneToMany()
            );

            Bag<ItemNotaCompraFornecedor>(x => x.ItemNotaDeCompraFornecedor, m =>
            {
                m.Cascade(Cascade.All);
                m.Lazy(CollectionLazy.Lazy);
                m.Inverse(true);
            },
                r => r.OneToMany()
            );

            Bag<ItemVendaCliente>(x => x.ItemVendasCliente, m =>
            {
                m.Cascade(Cascade.All);
                m.Lazy(CollectionLazy.Lazy);
                m.Inverse(true);
            },
                r => r.OneToMany()
            );

            Bag<Imagem>(x => x.Imagens, m =>
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
