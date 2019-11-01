using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ateliex.Decisoes.Comerciais
{
    public interface IConsultaDePlanosComerciais
    {
        Task<RespostaDeConsultaDePlanosComerciais> ConsultaPlanosComerciais(ParametrosDeConsultaDePlanosComerciais parametros);

        Task<IEnumerable<PlanoComercial>> ObtemObservavelDePlanosComerciais();
    }

    public class ParametrosDeConsultaDePlanosComerciais
    {

    }

    public class RespostaDeConsultaDePlanosComerciais
    {
        public ItemDeConsultaDePlanosComerciais[] Items { get; set; }
    }

    public class ItemDeConsultaDePlanosComerciais
    {
        public string Codigo { get; set; }

        public string Nome { get; set; }

        public DateTime Data { get; set; }

        public decimal RendaBrutaMensal { get; set; }
    }
}
