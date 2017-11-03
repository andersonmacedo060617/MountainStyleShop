using MountainStyleShop.ModelNH.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainStyleShop.ModelNH.Model
{
    public class CaixaEmpresa
    {
        public virtual IList<LancamentosCaixa> LancamentosPorMesAno(int Mes, int Ano)
        {
            return ConfigDB.Instance.LancamentosCaixaRepository.GetAll()
                .Where(x => x.DataLancamento.Month == Mes && x.DataLancamento.Year == Ano)
                .ToList();
        }

        public virtual IList<LancamentosCaixa> LancamentosPorPeriodo(DateTime DataInicio, DateTime DataFim)
        {
            if(DataInicio > DataFim)
            {
                return null;
            }

            return ConfigDB.Instance.LancamentosCaixaRepository.GetAll()
                .Where(x => x.DataLancamento > DataInicio && x.DataLancamento < DataFim)
                .ToList();
        }
    }
}
