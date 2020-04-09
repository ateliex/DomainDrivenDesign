using MediatR;

namespace Ateliex.Cadastro.Modelos
{
    public class VersionaModelo : IRequest
    {
        public CodigoDeModelo Codigo { get; }

        public VersionaModelo(CodigoDeModelo codigo)
        {
            Codigo = codigo;
        }
    }
}
