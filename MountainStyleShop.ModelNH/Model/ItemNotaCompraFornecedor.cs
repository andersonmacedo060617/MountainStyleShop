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
    public class ItemNotaCompraFornecedor
    {
        public virtual int Id { get; set; }

        [Display(Name = "Valor Unitario para compra")]
        [Required(ErrorMessage = "O Valor Unitario é Obrigatorio.")]
        [Range(0, 1000000, ErrorMessage ="O Valor Unitario deve estar entre 0 e 1.000.000")]
        [DisplayFormat(DataFormatString = "{0:n2}",
            ApplyFormatInEditMode = true,
            NullDisplayText = "Sem preço")]
        public virtual Double ValorUnitario { get; set; }

        [Display(Name = "Quantidade do produto")]
        [Required(ErrorMessage = "É Obrigatorio informar a quantidade.")]
        [Range(1, 1000000, ErrorMessage = "A quantidade deve estar entre 1 e 1.000.000")]
        public virtual int Quantidade { get; set; }

        [Display(Name ="Nota de Compra")]
        public virtual NotaDeCompraFornecedor NotaDeCompra { get; set; }

        [Display(Name = "Produto")]
        public virtual Produto Produto { get; set; }

        public virtual double MargemLucro { get; set; }
        
        public virtual IList<ValorAddNotaCompraPedido> ValorAddNotaCompraPedido { get; set; }

        [Display(Name = "Valor Total Itens")]
        public virtual double ValorAddTotal()
        {
            double vlrAddTotal = 0;
            foreach (var vlrAdd in ValorAddNotaCompraPedido)
            {
                if(vlrAdd.OperadorValorAdicional == ENum.EOperadorValorAdicional.Substracao)
                {
                    vlrAddTotal = vlrAddTotal - vlrAdd.Valor();
                }
                else
                {
                    vlrAddTotal = vlrAddTotal + vlrAdd.Valor();
                }
                
            }

            return vlrAddTotal;
        }

        public virtual double ValorLucro()
        {
            return this.ValorUnitario * (this.MargemLucro / 100);
        }

        public virtual double ValorUnitarioComLucro()
        {
            return this.ValorUnitario + this.ValorLucro();
        }

        [Display(Name ="Valor Total Itens")]
        public virtual Double ValorItens()
        {
            return this.ValorUnitarioComLucro() * Quantidade;
        }

        
        public virtual Double ValorTotalItens()
        {
            return ValorItens() + ValorAddTotal();
        }

        public virtual double ValorUnitComVlrAdd()
        {
            return ValorTotalItens() / Quantidade;
        }

        public virtual void AjustaValorUnidade()
        {
            if(Quantidade > 0)
            {

                var produto = ConfigDB.Instance.ProdutoRepository.BuscaPorId(this.Produto.Id);
                var valorAntigo = produto.Valor;
                var valorUnitComprado = this.ValorUnitComVlrAdd();
                var estoqueAntigo = produto.Estoque.QuantidadeEstoqueProduto();
                var estoqueNovo = this.Quantidade;

                var valorNovo = (estoqueAntigo * valorAntigo) + (estoqueNovo * valorUnitComprado);
                valorNovo = valorNovo / (estoqueAntigo + estoqueNovo);
                produto.Valor = Math.Round(valorNovo, 2);
                ConfigDB.Instance.ProdutoRepository.Gravar(produto);
            }
        }

    }

    public class ItemNotaCompraFornecedorMap : ClassMapping<ItemNotaCompraFornecedor>
    {
        public ItemNotaCompraFornecedorMap()
        {
            

            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });


            Property<Double>(x => x.ValorUnitario);
            Property<int>(x => x.Quantidade);
            Property<double>(x => x.MargemLucro);

            ManyToOne<NotaDeCompraFornecedor>(x => x.NotaDeCompra, m =>
            {
                m.Column("NotaDeCompra");
            });

            ManyToOne<Produto>(x => x.Produto, m =>
            {
                m.Column("Produto");
            });

            Bag<ValorAddNotaCompraPedido>(x => x.ValorAddNotaCompraPedido, m =>
            {
                m.Cascade(Cascade.All);
                m.Lazy(CollectionLazy.NoLazy);
                m.Inverse(true);
            },
                r => r.OneToMany()
           );


        

        }
    }
}
