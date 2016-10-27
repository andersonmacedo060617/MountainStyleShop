using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System.ComponentModel.DataAnnotations;

namespace MountainStyleShop.ModelNH.Model
{
    public class TipoDetalhamento
    {
        public virtual int Id { get; set; }

        [Display(Name = "Nome do Tipo de Detalhe")]
        [Required(ErrorMessage = "O Nome é Obrigatorio.")]
        public virtual string Nome { get; set; }

        [Display(Name = "Este Tipo é um Lucro?")]
        public virtual bool Lucro { get; set; }


    }

    public class TipoDetalhamentoMap : ClassMapping<TipoDetalhamento>
    {
        public TipoDetalhamentoMap()
        {
            Table("TipoDetalhamento");

            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });

            Property<string>(x => x.Nome);
            Property<bool>(x => x.Lucro);
            
        }
    }
}