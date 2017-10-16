using MountainStyleShop.ModelNH.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MountainStyleShop.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            var clientes = ConfigDB.Instance.UsuarioRepository.GetAll();
            return View(clientes);
        }
    }
}