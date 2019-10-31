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
            var resposta = new RespostaDeConsultaDeModelos();

            var items = new List<ItemDeConsultaDeModelos>();

            items.Add(new ItemDeConsultaDeModelos { Codigo = "TM01", Nome = "Tati Model 01" });

            items.Add(new ItemDeConsultaDeModelos { Codigo = "TM02", Nome = "Tati Model 02" });

            items.Add(new ItemDeConsultaDeModelos { Codigo = "TM03", Nome = "Tati Model 03" });

            resposta.Items = items.ToArray();

            return resposta;
        }
    }
}
