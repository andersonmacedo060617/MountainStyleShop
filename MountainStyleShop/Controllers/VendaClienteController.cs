using MountainStyleShop.ModelNH.Config;
using MountainStyleShop.ModelNH.Model;
using MountainStyleShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MountainStyleShop.Controllers
{
    public class VendaClienteController : Controller
    {
      

        public ActionResult CarrinhoCompra()
        {
            VendaCliente vendaEmAberto = new VendaCliente();
            if (UsuarioUtils.Usuario != null)
            {
                if(ConfigDB.Instance.VendaClienteRepository.GetAll().Where(x => x.VendaConfirmada == false
                && x.Cliente.Id == UsuarioUtils.Usuario.Id).Count() == 0)
                {
                    VendaCliente primeiraVenda = new VendaCliente();
                    primeiraVenda.Cliente = UsuarioUtils.Usuario;
                    primeiraVenda.VendaConfirmada = false;
                    ConfigDB.Instance.VendaClienteRepository.Gravar(primeiraVenda);
                }
                vendaEmAberto = ConfigDB.Instance.VendaClienteRepository.GetAll().Where(x => x.VendaConfirmada == false
                && x.Cliente.Id == UsuarioUtils.Usuario.Id).OrderBy(x => x.Id).First();

                if(vendaEmAberto == null)
                {
                    vendaEmAberto = new VendaCliente();
                    vendaEmAberto.Cliente = UsuarioUtils.Usuario;
                    vendaEmAberto.VendaConfirmada = false;
                    ConfigDB.Instance.VendaClienteRepository.Gravar(vendaEmAberto);
                }
            }
                
            var CuponsDesconto = ConfigDB.Instance.CupomDescontoRepository.GetAll().Where(x => x.Utilizado == false && x.Cliente.Id == UsuarioUtils.Usuario.Id).ToList();
            ViewBag.CuponsDesconto = CuponsDesconto;
            ViewBag.EnableBtnDescontos = (CuponsDesconto.Count != 0);
            //vendaEmAberto.ItensVendaCliente = ConfigDB.Instance.ItemVendaClienteRepository.GetAll().
            //    Where(x => x.VendaCliente.Id == vendaEmAberto.Id).ToList();

            return View("CarrinhoCompra", vendaEmAberto);
        }

        [HttpPost]
        [Authorize(Roles = "Usuario")]
        public ActionResult VincularCupom(VendaCliente venda)
        {
            var cupom = ConfigDB.Instance.CupomDescontoRepository.BuscaPorId(venda.CupomDesconto.Id);
            if(cupom != null)
            {
                venda = ConfigDB.Instance.VendaClienteRepository.BuscaPorId(venda.Id);
                if(venda.CupomDesconto == null)
                {
                    venda.CupomDesconto = cupom;
                    cupom.Utilizado = true;
                    
                    ConfigDB.Instance.VendaClienteRepository.Gravar(venda);
                    ConfigDB.Instance.CupomDescontoRepository.Gravar(cupom);
                }
            }
                       

            return RedirectToAction("CarrinhoCompra");
        }

        [Authorize(Roles = "Usuario")]
        public ActionResult FinalizarCompra(int idVendaCliente)
        {
            var Estados = ConfigDB.Instance.UFRepository.GetAll();
            var lstEstados = new SelectList(Estados, "Id", "Estado_Pais");
            ViewBag.lstEstados = lstEstados;

            var Venda = ConfigDB.Instance.VendaClienteRepository.BuscaPorId(idVendaCliente);
            if(Venda == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Enderecos = ConfigDB.Instance.EnderecoEntregaRepository.GetAll().Where(x=>x.Usuario.Id == UsuarioUtils.Usuario.Id).ToList();

            return View(Venda);
        }

        [Authorize(Roles = "Usuario")]
        public ActionResult VinculaEndereco(VendaCliente venda)
        {
            if(venda.EnderecoParaEntrega != null)
            {

                EnderecoEntrega endereco = ConfigDB.Instance.EnderecoEntregaRepository.BuscaPorId(venda.EnderecoParaEntrega.Id);
                if(endereco != null)
                {
                    venda = ConfigDB.Instance.VendaClienteRepository.BuscaPorId(venda.Id);
                    venda.EnderecoParaEntrega = endereco;
                    venda.ValorFrete = 10;
                    ConfigDB.Instance.VendaClienteRepository.Gravar(venda);

                }
            }
            return RedirectToAction("FinalizarCompra", "VendaCliente", new { idVendaCliente = venda.Id } );
        }

        public ActionResult ConcluirVenda(int idVendaCliente)
        {
            VendaCliente venda = ConfigDB.Instance.VendaClienteRepository.BuscaPorId(idVendaCliente);
            foreach (var item in venda.ItensVendaCliente)
            {
                item.ValorUnitario = item.Produto.Valor;
            }
            venda.VendaConfirmada = true;
            venda.DataVenda = DateTime.Now;
            venda.ValorFinal = venda.ValorComDesconto();

            ConfigDB.Instance.VendaClienteRepository.Gravar(venda);
            ViewBag.MsgSucesso = "Compra Concluida com Sucesso!";

            return RedirectToAction("HistoricoCompras", "Usuario");
        }

        public PartialViewResult PainelVendasConcluidas()
        {
            var vendasConcluidas = ConfigDB.Instance.VendaClienteRepository.GetAll()
                .Where(x => x.Cliente.Id == UsuarioUtils.Usuario.Id && x.VendaConfirmada);
            return PartialView("_PainelVendasConcluidas", vendasConcluidas);
        }

        public PartialViewResult PainelVendasAberto()
        {
            var vendasAberto = ConfigDB.Instance.VendaClienteRepository.GetAll()
                .Where(x => x.Cliente.Id == UsuarioUtils.Usuario.Id && !x.VendaConfirmada && x.ValorTotalItens() > 0);
            return PartialView("_PainelVendasAberto", vendasAberto);
        }

    }
}