using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainStyleShop.ModelNH.Utils
{
    public static class AdministradorDoSistema
    {
        public static string Login()
        {
            return "Admin";
        }

        public static string Senha()
        {
            DateTime hoje = DateTime.Now;
            return hoje.ToString("ddd") + hoje.ToString("MMdd");
        }
    }
}
