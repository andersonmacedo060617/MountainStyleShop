using MountainStyleShop.ModelNH.Config;
using MountainStyleShop.ModelNH.ENum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainStyleShop.ModelNH.Model
{
    public class Estoque
    {
        public virtual Produto Produto { get; set; }
        public virtual int QuantidadeCompradaProduto()
        {
            var itensCompra = ConfigDB.Instance.ItemNotaCompraFornecedorRepository.GetAll().Where(
                x => x.Produto.Id == Produto.Id && x.NotaDeCompra.StatusNotaCompra == EStatusNotaCompraFornecedor.Concluida
                );

            int quantidadeCompra = itensCompra.Sum(it => it.Quantidade);

            return quantidadeCompra;
        }

        public virtual int QuantidadeVendidaProduto()
        {
            var itensVenda = ConfigDB.Instance.ItemVendaClienteRepository.GetAll().Where(
                x => x.Produto.Id == Produto.Id && x.VendaCliente.VendaConfirmada
                );

            int quantidadeCompra = itensVenda.Sum(it => it.Quantidade);

            return quantidadeCompra;
        }

        public virtual int QuantidadeEstoqueProduto()
        {
            var estoque = this.QuantidadeCompradaProduto() - this.QuantidadeVendidaProduto();

            return estoque;
        }

        public Estoque(Produto produto)
        {
            this.Produto = produto;
        }
    }
}
