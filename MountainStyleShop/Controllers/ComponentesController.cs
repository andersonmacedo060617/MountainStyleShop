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
        public PartialViewResult MenuCategorias(int idCategoriaAtiva = 0)
        {
            ViewBag.Categorias = ConfigDB.Instance.CategoriaRepository.GetAll();
            ViewBag.CategoriaAtiva = idCategoriaAtiva;
            return PartialView("_MenuCategorias");
        }

        public PartialViewResult PainelImagensIndex()
        {
            return PartialView("_PainelImagensIndex");
        }

        
        
         
    }
}