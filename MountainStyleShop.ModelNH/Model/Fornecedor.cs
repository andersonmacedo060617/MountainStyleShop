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
    public class Fornecedor
    {
        public virtual int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O Nome é Obrigatorio.")]
        public virtual string Nome { get; set; }

        public virtual string Cnpj{ get; set; }

        public virtual IList<NotaDeCompraFornecedor> NotasCompraFornecedor { get; set; }
    }

    public class FornecedorMap : ClassMapping<Fornecedor>
    {
        public FornecedorMap()
        {
            

            Id<int>(x => x.Id, m =>
            {
                m.Generator(Generators.Identity);
            });

            Property<string>(x => x.Nome);
            Property<string>(x => x.Cnpj);

            Bag<NotaDeCompraFornecedor>(x => x.NotasCompraFornecedor, m =>
            {
                m.Cascade(Cascade.All);
                m.Lazy(CollectionLazy.Lazy);
                m.Inverse(true);
            },
                r => r.OneToMany()
           );


        }
    }
}
