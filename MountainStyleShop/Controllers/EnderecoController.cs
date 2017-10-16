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
    public class EnderecoController : Controller
    {
        // GET: Endereco
        [Authorize(Roles = "Usuario")]
        public ActionResult EnderecoUsuario(int idUsuario)
        {
            
            var Enderecos = ConfigDB.Instance.EnderecoEntregaRepository.GetAll().Where(x => x.Usuario.Id == idUsuario);
            
            return View(Enderecos);
        }

        [Authorize(Roles = "Usuario")]
        public ActionResult Novo()
        {
            var Estados = ConfigDB.Instance.UFRepository.GetAll();
            var lstEstados = new SelectList(Estados, "Id", "Estado_Pais");
            ViewBag.lstEstados = lstEstados;

            return View();
        }

        [Authorize(Roles = "Usuario")]
        [HttpPost]
        public ActionResult Gravar(EnderecoEntrega Endereco)
        {
            if(UsuarioUtils.Usuario != null)
            {
                Endereco.Usuario = UsuarioUtils.Usuario;
                Endereco.Cidade = ConfigDB.Instance.CidadeRepository.BuscaPorId(Endereco.Cidade.Id);
                ConfigDB.Instance.EnderecoEntregaRepository.Gravar(Endereco);
            }

            return RedirectToAction("EnderecoUsuario", "Endereco", new { idUsuario = UsuarioUtils.Usuario.Id });
        }

        public ActionResult GravarEnderecoVenda(VendaCliente venda)
        {
            EnderecoEntrega endereco = venda.EnderecoParaEntrega;
            if (UsuarioUtils.Usuario != null)
            {
                endereco.Usuario = UsuarioUtils.Usuario;
                endereco.Cidade = ConfigDB.Instance.CidadeRepository.BuscaPorId(endereco.Cidade.Id);
                ConfigDB.Instance.EnderecoEntregaRepository.Gravar(endereco);
            }
            

            return RedirectToAction("FinalizarCompra", "VendaCliente", new { idVendaCliente = venda.Id });

        }
    }
}