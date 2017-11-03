using MountainStyleShop.ModelNH.Config;
using MountainStyleShop.ModelNH.ENum;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainStyleShop.ModelNH.Model
{
    public class NotaDeCompraFornecedor
    {
        public virtual int Id { get; set; }

        [Display(Name = "Data da Compra")]
        [Required(ErrorMessage = "A Data da Compra é obrigatoria.")]
        [DataType(dataType: DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}", NullDisplayText = "Data Invalida")]
        public virtual DateTime DataDaCompra { get; set; }

        [Display(Name = "Data Prevista da Entrega")]
        [Required(ErrorMessage = "A Data da Entrega Prevista é obrigatoria.")]
        [DataType(dataType: DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}", NullDisplayText = "Data Invalida")]
        public virtual DateTime DataEntregaPrevista { get; set; }
        
        [Display(Name = "Data da Entrega")]
        [DataType(dataType: DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}", NullDisplayText = "Data Invalida")]
        public virtual DateTime DataDeEntrega { get; set; }

        [Display(Name = "Data de Cadastro")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public virtual DateTime DataDeCadastro { get; set; }

        [Display(Name = "Itens da Compra")]
        public virtual IList<ItemNotaCompraFornecedor> ItensPedidos{get; set;}

        [Display(Name = "Fornecedor")]
        public virtual Fornecedor Fornecedor { get; set; }

        public virtual IList<ValorAddNotaCompraFornecedor> ValorAddNotaCompra { get; set; }

        public NotaDeCompraFornecedor()
        {
            ItensPedidos = new List<ItemNotaCompraFornecedor>();
            ValorAddNotaCompra = new List<ValorAddNotaCompraFornecedor>();
        }

        public virtual bool ProdutoEntregue()
        {
            if (this.DataDeEntrega.ToString("dd/MM/yyyy").Equals("01/01/0001"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public virtual double ValorTotalItens()
        {
            double valorTotalItens = 0;
            foreach (var item in this.ItensPedidos)
            {
                valorTotalItens = valorTotalItens + item.ValorTotalItens();
            }

            return valorTotalItens;
        }

        public virtual double ValorTotalAdicional()
        {
            double valorAddTotal = 0;
            foreach (var vlrAdd in this.ValorAddNotaCompra)
            {
                valorAddTotal = valorAddTotal + vlrAdd.Valor;
            }

            return valorAddTotal;
        }

        public virtual double ValorTotalNota()
        {
            return this.ValorTotalItens() + ValorTotalAdicional();
        }

        public virtual EStatusNotaCompraFornecedor StatusNotaCompra{ get; set; }

        public virtual void ConcluirNota()
        {
            
            this.StatusNotaCompra = EStatusNotaCompraFornecedor.Concluida;

            foreach (var item in this.ItensPedidos)
            {
                item.AjustaValorUnidade();
            }
            
        }

    }

    public class NotaDeCompraFornecedorMap : ClassMapping<NotaDeCompraFornecedor>
    {
        public NotaDeCompraFornecedorMap()
        {
            

            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });


            Property<DateTime>(x => x.DataDaCompra);
            Property<DateTime>(x => x.DataDeEntrega);
            Property<DateTime>(x => x.DataEntregaPrevista);
            Property<DateTime>(x => x.DataDeCadastro);
            Property<EStatusNotaCompraFornecedor>(x => x.StatusNotaCompra);


            ManyToOne<Fornecedor>(x => x.Fornecedor, m =>
            {
                m.Column("Fornecedor");
            });

            Bag<ItemNotaCompraFornecedor>(x => x.ItensPedidos, m =>
            {
                m.Cascade(Cascade.All);
                m.Lazy(CollectionLazy.Lazy);
                m.Inverse(true);
            },
                r => r.OneToMany()
            );

            Bag<ValorAddNotaCompraFornecedor>(x => x.ValorAddNotaCompra, m =>
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
