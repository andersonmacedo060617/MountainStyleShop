using MountainStyleShop.ModelNH.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MountainStyleShop.Controllers
{
    public class ComponentesController : Controller
    {
        // GET: Componentes
        public PartialViewResult MenuCategorias()
        {
            ViewBag.Categorias = ConfigDB.Instance.CategoriaRepository.GetAll();

            return PartialView("_MenuCategorias");
        }
    }
}