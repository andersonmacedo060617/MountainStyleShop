using MountainStyleShop.ModelNH.Model;
using MountainStyleShop.ModelNH.Repository;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Context;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Configuration;

namespace MountainStyleShop.ModelNH.Config
{
    public class ConfigDB
    {
        public static string StringConexao = ConnectionString();
        
        public ConnectionStringSettingsCollection Configuracao = ConfigurationManager.ConnectionStrings;

        private ISessionFactory SessionFactory;

        


        #region PegaStringDeConexao
        private static string ConnectionString()
        {
            ConnectionStringSettingsCollection Configuracoes = ConfigurationManager.ConnectionStrings;
            var strConn = Configuracoes["MountainStyleShop"].ConnectionString;
            return strConn;
        }
        #endregion


        private static ConfigDB _instance = null;
        public static ConfigDB Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ConfigDB();
                }

                return _instance;
            }
        }

       

        public ProdutoRepository ProdutoRepository { get; set; }
        public UsuarioRepository UsuarioRepository { get; set; }
        public CategoriaRepository CategoriaRepository { get; set; }
        public DetalhaCustoRepository DetalhaCustoRepository { get; set; }
        public ItemPedidoRepository ItemPedidoRepository { get; set; }
        public NotaDeCompraRepository NotaDeCompraRepository { get; set; }
        public TipoDetalhamentoRepository TipoDetalhamentoRepository { get; set; }
        public PessoaRepository PessoaRepository { get; set; }

        public ConfigDB()
        {
            if (Conexao())
            {
                this.ProdutoRepository = new ProdutoRepository(Session);
                this.UsuarioRepository = new UsuarioRepository(Session);
                this.CategoriaRepository = new CategoriaRepository(Session);
                this.DetalhaCustoRepository = new DetalhaCustoRepository(Session);
                this.ItemPedidoRepository = new ItemPedidoRepository(Session);
                this.NotaDeCompraRepository = new NotaDeCompraRepository(Session);
                this.TipoDetalhamentoRepository = new TipoDetalhamentoRepository(Session);
                this.PessoaRepository = new PessoaRepository(Session);
            }
        }


        private bool Conexao()
        {
            //Cria a configuração com o NH
            var config = new NHibernate.Cfg.Configuration();
            try
            {
                //Integração com o Banco de Dados
                config.DataBaseIntegration(c =>
                {
                    //Dialeto de Banco
                    c.Dialect<NHibernate.Dialect.MySQLDialect>();
                    //Conexao String
                    c.ConnectionString = StringConexao;
                    //Drive de conexão com o banco
                    c.Driver<NHibernate.Driver.MySqlDataDriver>();
                    // Provedor de conexão do MySQL 
                    c.ConnectionProvider<NHibernate.Connection.DriverConnectionProvider>();
                    // GERA LOG DOS SQL EXECUTADOS NO CONSOLE
                    c.LogSqlInConsole = true;
                    // DESCOMENTAR CASO QUEIRA VISUALIZAR O LOG DE SQL FORMATADO NO CONSOLE
                    c.LogFormattedSql = true;
                    // CRIA O SCHEMA DO BANCO DE DADOS SEMPRE QUE A CONFIGURATION FOR UTILIZADA
                    c.SchemaAction = SchemaAutoAction.Update;
                });

                //Realiza o mapeamento das classes
                var maps = this.Mapeamento();
                config.AddMapping(maps);

                //Verifico se a aplicação é Desktop ou Web
                if (HttpContext.Current == null)
                {
                    config.CurrentSessionContext<ThreadStaticSessionContext>();
                }
                else
                {
                    config.CurrentSessionContext<WebSessionContext>();
                }

                this.SessionFactory = config.BuildSessionFactory();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private HbmMapping Mapeamento()
        {
            try
            {
                var mapper = new ModelMapper();

                mapper.AddMappings(
                    Assembly.GetAssembly(typeof(UsuarioMap)).GetTypes()
                );
                mapper.AddMappings(
                    Assembly.GetAssembly(typeof(ProdutoMap)).GetTypes()
                );
                mapper.AddMappings(
                    Assembly.GetAssembly(typeof(Categoria)).GetTypes()
                );
                mapper.AddMappings(
                    Assembly.GetAssembly(typeof(DetalhaCustoMap)).GetTypes()
                );
                mapper.AddMappings(
                    Assembly.GetAssembly(typeof(ItemPedidoMap)).GetTypes()
                );
                mapper.AddMappings(
                    Assembly.GetAssembly(typeof(NotaDeCompraMap)).GetTypes()
                );
                mapper.AddMappings(
                    Assembly.GetAssembly(typeof(TipoDetalhamentoMap)).GetTypes()
                );


                return mapper.CompileMappingForAllExplicitlyAddedEntities();
            }
            catch (Exception)
            {
                throw;

            }
        }

        private ISession Session
        {
            get
            {

                try
                {
                    if (CurrentSessionContext.HasBind(SessionFactory))
                    {
                        return SessionFactory.GetCurrentSession();

                    }
                    var session = SessionFactory.OpenSession();
                    CurrentSessionContext.Bind(session);
                    return session;
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }

        
    }
}
