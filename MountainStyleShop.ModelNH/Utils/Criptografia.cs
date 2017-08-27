using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptSharp;

namespace MountainStyleShop.ModelNH.Utils
{
    public static class Criptografia
    {
        public static string CodificaMD5(string senha)
        {
            return Crypter.MD5.Crypt(senha);
        }

        public static bool Comparar(string senha, string hash)
        {
            return Crypter.CheckPassword(senha, hash);
        }
    }
}
