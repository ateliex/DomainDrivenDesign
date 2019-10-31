using System;
using System.Collections.Generic;
using System.Text;

namespace Ateliex.Cadastro.Modelos
{
    public interface IConsultaDeModelos
    {
        RespostaDeConsultaDeModelos ConsultaModelos(ParametrosDeConsultaDeModelos parametros);
    }

    public class ParametrosDeConsultaDeModelos
    {

    }

    public class RespostaDeConsultaDeModelos
    {
        public ItemDeConsultaDeModelos[] Items { get; set; }
    }

    public class ItemDeConsultaDeModelos
    {
        public string Codigo { get; set; }

        public string Nome { get; set; }
    }

    internal class ConsultaDeModelos : IConsultaDeModelos
    {
        public ConsultaDeModelos()
        {

        }

        public RespostaDeConsultaDeModelos ConsultaModelos(ParametrosDeConsultaDeModelos parametros)
        {
            throw new NotImplementedException();
        }
    }
}
