using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Ateliex.Cadastro.Modelos
{
    public class NomeDeModeloAlteradoHandler : IRequestHandler<NomeDeModeloAlterado>
    {
        private readonly IRepositorioDeModelos repositorioDeModelos;

        public NomeDeModeloAlteradoHandler(IRepositorioDeModelos repositorioDeModelos)
        {
            this.repositorioDeModelos = repositorioDeModelos;
        }

        public async Task<Unit> Handle(NomeDeModeloAlterado request, CancellationToken cancellationToken)
        {
            var modelo = await repositorioDeModelos.ObtemModelo(request.Codigo);

            modelo.When(request);

            //modelo.AlteraNome(request.Nome);

            await repositorioDeModelos.Update(modelo);

            return Unit.Value;
        }
    }
}
