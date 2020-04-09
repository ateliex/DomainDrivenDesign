using System;

namespace Ateliex.Cadastro.Modelos.ConsultaDeModelos
{
    public interface IConsultaDeModelos
    {
        IObservable<Modelo[]> ConsultaModelos(SolicitacaoDeConsultaDeModelos solicitacao);

        //Modelo ObtemModelo(CodigoDeModelo codigo);
    }

    public class SolicitacaoDeConsultaDeModelos
    {
        public string Nome { get; set; }

        public long PrimeiraPagina { get; set; }

        public long TamanhoDaPagina { get; set; }
    }
}