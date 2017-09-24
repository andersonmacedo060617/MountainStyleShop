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

        #region Repositorys
        public AjusteEstoqueRepository AjusteEstoqueRepository { get; set; }
        public AvaliacaoProdutoRepository AvaliacaoProdutoRepository { get; set; }
        public CategoriaRepository CategoriaRepository { get; set; }
        public CidadeRepository CidadeRepository { get; set; }
        public CustoAddVendaClienteRepository CustoAddVendaClienteRepository { get; set; }
        public EnderecoEntregaRepository EnderecoEntregaRepository { get; set; }
        public FabricanteRepository FabricanteRepository { get; set; }
        public FormaPagamentoRepository FormaPagamentoRepository { get; set; }
        public FornecedorRepository FornecedorRepository { get; set; }
        public ItemNotaCompraFornecedorRepository ItemNotaCompraFornecedorRepository { get; set; }
        public ItemVendaClienteRepository ItemVendaClienteRepository { get; set; }
        public NotaDeCompraFornecedorRepository NotaDeCompraFornecedorRepository { get; set; }
        public PagamentoRepository PagamentoRepository { get; set; }
        public PaisRepository PaisRepository { get; set; }
        public ProdutoFavoritoRepository ProdutoFavoritoRepository { get; set; }
        public ProdutoRepository ProdutoRepository { get; set; }
        public TipoValorAddRepository TipoValorAddRepository { get; set; }
        public UFRepository UFRepository { get; set; }
        public UsuarioRepository UsuarioRepository { get; set; }
        public ValorAddNotaCompraFornecedorRepository ValorAddNotaCompraFornecedorRepository { get; set; }
        public ValorEntregaRepository ValorEntregaRepository { get; set; }
        public ValoresPagamentoVendaClienteRepository ValoresPagamentoVendaClienteRepository { get; set; }
        public VendaClienteRepository VendaClienteRepository { get; set; }
        public ImagemRepository ImagemRepository { get; set; }
        public CategoriasInteresseRepository CategoriasInteresseRepository { get; set; }
        public BuscaProdutoRepository BuscaProdutoRepository { get; set; }


        #endregion



        public ConfigDB()
        {
            #region InstanciaRepositorys
            if (Conexao())
            {
                this.AjusteEstoqueRepository = new AjusteEstoqueRepository(Session);
                this.AvaliacaoProdutoRepository = new AvaliacaoProdutoRepository(Session);
                this.CategoriaRepository = new CategoriaRepository(Session);
                this.CidadeRepository = new CidadeRepository(Session);
                this.CustoAddVendaClienteRepository = new CustoAddVendaClienteRepository(Session);
                this.EnderecoEntregaRepository = new EnderecoEntregaRepository(Session);
                this.FabricanteRepository = new FabricanteRepository(Session);
                this.FormaPagamentoRepository = new FormaPagamentoRepository(Session);
                this.FornecedorRepository = new FornecedorRepository(Session);
                this.ItemNotaCompraFornecedorRepository = new ItemNotaCompraFornecedorRepository(Session);
                this.ItemVendaClienteRepository = new ItemVendaClienteRepository(Session);
                this.NotaDeCompraFornecedorRepository = new NotaDeCompraFornecedorRepository(Session);
                this.PagamentoRepository = new PagamentoRepository(Session);
                this.PaisRepository = new PaisRepository(Session);
                this.ProdutoFavoritoRepository = new ProdutoFavoritoRepository(Session);
                this.ProdutoRepository = new ProdutoRepository(Session);
                this.TipoValorAddRepository = new TipoValorAddRepository(Session);
                this.UFRepository = new UFRepository(Session);
                this.UsuarioRepository = new UsuarioRepository(Session);
                this.ValorAddNotaCompraFornecedorRepository = new ValorAddNotaCompraFornecedorRepository(Session);
                this.ValorEntregaRepository = new ValorEntregaRepository(Session);
                this.ValoresPagamentoVendaClienteRepository = new ValoresPagamentoVendaClienteRepository(Session);
                this.VendaClienteRepository = new VendaClienteRepository(Session);
                this.ImagemRepository = new ImagemRepository(Session);
                this.CategoriasInteresseRepository = new CategoriasInteresseRepository(Session);
                this.BuscaProdutoRepository = new BuscaProdutoRepository(Session);
            }
            #endregion
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Mapeamento
        private HbmMapping Mapeamento()
        {
            
            try
            {
                var mapper = new ModelMapper();

                mapper.AddMappings(
                    Assembly.GetAssembly(typeof(AjusteEstoqueMap)).GetTypes()
                );
                mapper.AddMappings(
                    Assembly.GetAssembly(typeof(AvaliacaoProdutoMap)).GetTypes()
                );
                mapper.AddMappings(
                    Assembly.GetAssembly(typeof(CategoriaMap)).GetTypes()
                );
                mapper.AddMappings(
                    Assembly.GetAssembly(typeof(CidadeMap)).GetTypes()
                );
                mapper.AddMappings(
                    Assembly.GetAssembly(typeof(CustoAddVendaClienteMap)).GetTypes()
                );
                mapper.AddMappings(
                    Assembly.GetAssembly(typeof(EnderecoEntregaMap)).GetTypes()
                );
                mapper.AddMappings(
                    Assembly.GetAssembly(typeof(FabricanteMap)).GetTypes()
                );
                mapper.AddMappings(
                    Assembly.GetAssembly(typeof(FormaPagamentoMap)).GetTypes()
                );
                mapper.AddMappings(
                    Assembly.GetAssembly(typeof(FornecedorMap)).GetTypes()
                );
                mapper.AddMappings(
                    Assembly.GetAssembly(typeof(ItemNotaCompraFornecedorMap)).GetTypes()
                );
                mapper.AddMappings(
                    Assembly.GetAssembly(typeof(ItemVendaClienteMap)).GetTypes()
                );
                mapper.AddMappings(
                    Assembly.GetAssembly(typeof(NotaDeCompraFornecedorMap)).GetTypes()
                );
                mapper.AddMappings(
                    Assembly.GetAssembly(typeof(PagamentoMap)).GetTypes()
                );
                mapper.AddMappings(
                    Assembly.GetAssembly(typeof(PaisMap)).GetTypes()
                );
                mapper.AddMappings(
                    Assembly.GetAssembly(typeof(ProdutoMap)).GetTypes()
                );
                mapper.AddMappings(
                    Assembly.GetAssembly(typeof(ProdutoFavoritoMap)).GetTypes()
                );
                mapper.AddMappings(
                    Assembly.GetAssembly(typeof(TipoValorAddMap)).GetTypes()
                );
                mapper.AddMappings(
                    Assembly.GetAssembly(typeof(UFMap)).GetTypes()
                );
                mapper.AddMappings(
                    Assembly.GetAssembly(typeof(UsuarioMap)).GetTypes()
                );
                mapper.AddMappings(
                    Assembly.GetAssembly(typeof(ValorAddNotaCompraFornecedorMap)).GetTypes()
                );
                mapper.AddMappings(
                    Assembly.GetAssembly(typeof(ValorEntregaMap)).GetTypes()
                );
                mapper.AddMappings(
                    Assembly.GetAssembly(typeof(ValoresPagamentoVendaClienteMap)).GetTypes()
                );
                mapper.AddMappings(
                    Assembly.GetAssembly(typeof(VendaClienteMap)).GetTypes()
                );
                mapper.AddMappings(
                    Assembly.GetAssembly(typeof(ImagemMap)).GetTypes()
                );
                mapper.AddMappings(
                    Assembly.GetAssembly(typeof(CategoriasInteresseMap)).GetTypes()
                );
                mapper.AddMappings(
                    Assembly.GetAssembly(typeof(BuscaProdutoMap)).GetTypes()
                );

                return mapper.CompileMappingForAllExplicitlyAddedEntities();
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        #endregion

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
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        
    }
}
