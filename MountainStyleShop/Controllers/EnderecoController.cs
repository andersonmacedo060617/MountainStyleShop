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
        public ActionResult EnderecoUsuario(int idUsuario)
        {
            
            var Enderecos = ConfigDB.Instance.EnderecoEntregaRepository.GetAll().Where(x => x.Usuario.Id == idUsuario);
            
            return View(Enderecos);
        }

        public ActionResult Novo()
        {
            var Estados = ConfigDB.Instance.UFRepository.GetAll();
            var lstEstados = new SelectList(Estados, "Id", "Estado_Pais");
            ViewBag.lstEstados = lstEstados;

            return View();
        }
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
    }
}