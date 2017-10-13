using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MountainStyleShop.Models
{
    public class CookieUtils : System.Web.UI.Page
    {
        private String nomeCookie = "MountainStyleShopping";
        private String parametroCarrinho  = "itensCarrinho";
        private char delimitador = "_";

        public CookieUtils()
        {
            //Sempre q usar eu verifico o cookie
            VerificandoCookie();
        }

        public void AdicionarItemCarrinho(int idProduto)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[this.nomeCookie];
            String strProdutos = cookie.Values.Get(parametroCarrinho).ToString();
            List<String> idsProdutos  = strProdutos.Split(delimitador).ToList();

            idsProdutos.Add(idProduto);
        }

        private void VerificandoCookie()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[this.nomeCookie];
            if (cookie == null)
            {
                // Criando a Instância do cookie
                cookie = new HttpCookie(nomeCookie);
                //Adicionando a propriedade "Carrinho" no cookie
                cookie.Values.Add(parametroCarrinho, "");
                
                //colocando o cookie para expirar em 365 dias
                cookie.Expires = DateTime.Now.AddDays(30);
                // Definindo a segurança do nosso cookie
                cookie.HttpOnly = true;
                // Registrando cookie
                this.Page.Response.AppendCookie(cookie);
            }
        }

    }
}