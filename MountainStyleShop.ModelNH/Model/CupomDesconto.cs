using MountainStyleShop.ModelNH.ENum;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainStyleShop.ModelNH.Model
{
    public class CupomDesconto
    {
        public virtual int Id { get; set; }
        public virtual ETipoDesconto TipoDesconto { get; set; }
        public virtual double Valor { get; set; }
        public virtual bool Utilizado { get; set; }

        public virtual Usuario Cliente { get; set; }

        public virtual void InserirValor()
        {
            if(this.TipoDesconto == ETipoDesconto.Percentual)
            {
                this.Valor = 10;
            }

            if (this.TipoDesconto == ETipoDesconto.Valor)
            {
                this.Valor = 20;
            }
        }

        public virtual String ValorCupomStr()
        {
            String texto = "";
            if (this.TipoDesconto == ETipoDesconto.Percentual)
            {
                texto = Valor.ToString() + "%";
            }

            if (this.TipoDesconto == ETipoDesconto.Valor)
            {
                texto = "R$" + Valor.ToString();
            }

            return texto;
        }
    }

    public class CupomDescontoMap : ClassMapping<CupomDesconto>
    {
        public CupomDescontoMap()
        {
            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });

            Property<ETipoDesconto>(x => x.TipoDesconto);
            Property<double>(x => x.Valor);
            Property<bool>(x => x.Utilizado);

            ManyToOne<Usuario>(x => x.Cliente, m =>
            {
                m.Column("Cliente");
            });
        }
    }
}
