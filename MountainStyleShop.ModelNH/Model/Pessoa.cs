using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainStyleShop.ModelNH.Model
{
    public class Pessoa
    {
        public virtual int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O Nome é Obrigatorio.")]
        public virtual string Nome { get; set; }

        [Display(Name = "Tipo de Pessoa")]
        public virtual bool PFisica { get; set; }

        [Display(Name = "Fornecedor?")]
        public virtual bool Fornecedor { get; set; }
    }

    public class PessoaMap : ClassMapping<Pessoa>
    {
        public PessoaMap()
        {
            

            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });

            Property<string>(x => x.Nome);
            Property<bool>(x => x.PFisica);
            Property<bool>(x => x.Fornecedor);


        }
    }
}
