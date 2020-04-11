using System;

namespace Ateliex.Decisoes.Comerciais.ConsultaDePlanosComerciais
{
    public interface IConsultaDePlanosComerciais
    {
        IObservable<PlanoComercial> ConsultaPlanosComerciais(ParametrosDeConsultaDePlanosComerciais parametros);
    }

    public class ParametrosDeConsultaDePlanosComerciais
    {
        public string Nome { get; set; }

        public long PrimeiraPagina { get; set; }

        public long TamanhoDaPagina { get; set; }
    }
}
