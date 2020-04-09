using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Ateliex.Cadastro.Modelos
{
    public class ModeloExcluidoHandler : IRequestHandler<ModeloExcluido>
    {
        private readonly IRepositorioDeModelos repositorioDeModelos;

        public ModeloExcluidoHandler(IRepositorioDeModelos repositorioDeModelos)
        {
            this.repositorioDeModelos = repositorioDeModelos;
        }

        public async Task<Unit> Handle(ModeloExcluido request, CancellationToken cancellationToken)
        {
            var modelo = await repositorioDeModelos.ObtemModelo(request.Codigo);

            await repositorioDeModelos.Remove(modelo);

            return Unit.Value;
        }
    }
}
