using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Ateliex.Cadastro.Modelos
{
    public class ModeloHandler :
        IRequestHandler<ModeloCriado>,
        IRequestHandler<NomeDeModeloAlterado>,
        IRequestHandler<RecursoDeModeloAdicionado>,
        IRequestHandler<DescricaoDeRecursoDeModeloAlterado>,
        IRequestHandler<ModeloExcluido>
    {
        private readonly IRepositorioDeModelos repositorioDeModelos;

        public ModeloHandler(IRepositorioDeModelos repositorioDeModelos)
        {
            this.repositorioDeModelos = repositorioDeModelos;
        }

        public async Task<Unit> Handle(ModeloCriado request, CancellationToken cancellationToken)
        {
            var modelo = new Modelo(new[] { request });

            await repositorioDeModelos.Add(modelo);

            return Unit.Value;
        }

        public async Task<Unit> Handle(NomeDeModeloAlterado request, CancellationToken cancellationToken)
        {
            var modelo = await repositorioDeModelos.ObtemModelo(request.Codigo);

            modelo.Replay(request);

            await repositorioDeModelos.Update(modelo);

            return Unit.Value;
        }

        public async Task<Unit> Handle(RecursoDeModeloAdicionado request, CancellationToken cancellationToken)
        {
            var modelo = await repositorioDeModelos.ObtemModelo(request.Codigo);

            modelo.Replay(request);

            await repositorioDeModelos.Update(modelo);

            return Unit.Value;
        }

        public async Task<Unit> Handle(DescricaoDeRecursoDeModeloAlterado request, CancellationToken cancellationToken)
        {
            var modelo = await repositorioDeModelos.ObtemModelo(request.Codigo);

            modelo.Replay(request);

            await repositorioDeModelos.Update(modelo);

            return Unit.Value;
        }

        public async Task<Unit> Handle(ModeloExcluido request, CancellationToken cancellationToken)
        {
            var modelo = await repositorioDeModelos.ObtemModelo(request.Codigo);

            await repositorioDeModelos.Remove(modelo);

            return Unit.Value;
        }
    }
}
