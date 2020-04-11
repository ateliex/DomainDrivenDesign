using System;

namespace Ateliex.Cadastro.Modelos.ConsultaDeModelos
{
    public interface IConsultaDeModelos
    {
        IObservable<Modelo[]> ConsultaModelos(ParametrosDeConsultaDeModelos solicitacao);
    }

    public class ParametrosDeConsultaDeModelos
    {
        public string Nome { get; set; }

        public long PrimeiraPagina { get; set; }

        public long TamanhoDaPagina { get; set; }
    }
}