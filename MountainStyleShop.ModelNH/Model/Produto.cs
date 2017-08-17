﻿using NHibernate.Mapping.ByCode;
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

        [Display(Name = "Imagem do produto")]
        public virtual string Imagem { get; set; }
        

        [Display(Name = "Valor de venda:")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Required(ErrorMessage = "O Valor é Obrigatorio.")]
        [Range(0.01, 99999.99, ErrorMessage = "O Preço de Venda deve estar entre 0,01 e 99999,99.")]
        public virtual Double Valor { get; set; }

        [Range(0, 1000000, ErrorMessage ="Quantidade entre 0 e 1.000.000")]
        [Display(Name ="Quantidade em estoque")]
        [Required(ErrorMessage ="A quantidade de produtos é obrigatoria")]      
        public virtual int Quantidade { get; set; }
        
        public virtual Categoria Categoria { get; set; }

        public virtual IList<AvaliacaoProduto> AvaliacoesProdutos { get; set; }

        public virtual bool ApareeceNaVitrine { get; set; }

        public virtual double MediaNotasAvaliacao
        {
            get
            {
                int Total = 0;
                foreach (var Avalicao in this.AvaliacoesProdutos)
                {
                    Total += Avalicao.NotaAvaliacao;
                }

                return Total  / this.AvaliacoesProdutos.Count;
            }
        }

        public virtual int QuantidadeAvaliacoes {
            get
            {
                return this.AvaliacoesProdutos.Count;
            }
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
            Property<string>(x => x.Imagem);
            Property<Double>(x => x.Valor);
            Property<int>(x => x.Quantidade);
            Property<bool>(x => x.ApareeceNaVitrine);


            ManyToOne<Categoria>(x => x.Categoria, m =>
            {
                m.Column("IdCategoria");
            });

            Bag<AvaliacaoProduto>(x => x.AvaliacoesProdutos, m =>
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
