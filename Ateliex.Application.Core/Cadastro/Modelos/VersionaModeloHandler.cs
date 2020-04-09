using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Ateliex.Cadastro.Modelos
{
    public class VersionaModeloHandler : IRequestHandler<VersionaModelo>
    {
        private readonly IRepositorioDeModelos repositorioDeModelos;

        public VersionaModeloHandler(IRepositorioDeModelos repositorioDeModelos)
        {
            this.repositorioDeModelos = repositorioDeModelos;
        }

        public async Task<Unit> Handle(VersionaModelo request, CancellationToken cancellationToken)
        {
            var modelo = await repositorioDeModelos.ObtemModelo(request.Codigo);

            modelo.Version++;

            await repositorioDeModelos.Update(modelo);

            return Unit.Value;
        }
    }
}
