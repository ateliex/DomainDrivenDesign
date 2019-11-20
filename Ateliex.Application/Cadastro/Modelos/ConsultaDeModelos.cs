using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ateliex.Cadastro.Modelos
{
    public interface IConsultaDeModelos
    {
        Task<RespostaDeConsultaDeModelos> ConsultaModelos(ParametrosDeConsultaDeModelos parametros);
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
}
