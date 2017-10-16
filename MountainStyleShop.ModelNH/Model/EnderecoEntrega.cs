using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System.Collections.Generic;

namespace MountainStyleShop.ModelNH.Model
{
    public class EnderecoEntrega
    {
        public virtual int Id { get; set; }
        public virtual string Rua { get; set; }
        public virtual string Numero { get; set; }
        public virtual string Complemento { get; set; }
        public virtual string Bairro { get; set; }
        public virtual string CEP { get; set; }
        public virtual Cidade Cidade { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual string DescricaoEndereco { get; set; }
        

        public virtual ValorEntrega ValorFrete()
        {
            List<ValorEntrega> vlrEntrega = this.Cidade.ValoresEntrega as List<ValorEntrega>;
            if(vlrEntrega.Exists(x=>x.CEP == this.CEP))
            {
                return vlrEntrega.Find(x => x.CEP == this.CEP);
            }

            ValorEntrega MaiorValor = new ValorEntrega();
            MaiorValor.Valor = 0;
            foreach (var vlr in this.Cidade.ValoresEntrega)
            {
                if(vlr.Valor > MaiorValor.Valor)
                {
                    MaiorValor = vlr;
                }
            }

            return MaiorValor;
        }

        public virtual string DescricaoEnderecoStr()
        {
            return "Rua: " + this.Rua + "- Bairro: " + this.Bairro + " - Cidade: " + this.Cidade.Nome + " - Estado: " + this.Cidade.UF.Nome + " - Pais: " + this.Cidade.UF.Pais.Nome;
        }
    }

    public class EnderecoEntregaMap : ClassMapping<EnderecoEntrega>
    {
        public EnderecoEntregaMap()
        {
            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });

            Property<string>(x => x.Rua);
            Property<string>(x => x.Numero);
            Property<string>(x => x.Complemento);
            Property<string>(x => x.Bairro);
            Property<string>(x => x.CEP);
            Property<string>(x => x.DescricaoEndereco);

            ManyToOne<Cidade>(x => x.Cidade, m =>
            {
                m.Column("IdCidade");
            });

            ManyToOne<Usuario>(x => x.Usuario, m =>
            {
                m.Column("IdUsuario");
            });
        }

    }
}