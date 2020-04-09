using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Ateliex.Cadastro.Modelos
{
    public class ModeloCriadoHandler : IRequestHandler<ModeloCriado>
    {
        private readonly IRepositorioDeModelos repositorioDeModelos;

        public ModeloCriadoHandler(IRepositorioDeModelos repositorioDeModelos)
        {
            this.repositorioDeModelos = repositorioDeModelos;
        }

        public async Task<Unit> Handle(ModeloCriado request, CancellationToken cancellationToken)
        {
            var modelo = new Modelo(new[] { request });

            await repositorioDeModelos.Add(modelo);

            return Unit.Value;
        }
    }
}
