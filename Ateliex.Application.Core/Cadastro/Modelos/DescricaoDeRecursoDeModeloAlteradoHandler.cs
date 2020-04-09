using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Ateliex.Cadastro.Modelos
{
    public class DescricaoDeRecursoDeModeloAlteradoHandler : IRequestHandler<DescricaoDeRecursoDeModeloAlterado>
    {
        private readonly IRepositorioDeModelos repositorioDeModelos;

        public DescricaoDeRecursoDeModeloAlteradoHandler(IRepositorioDeModelos repositorioDeModelos)
        {
            this.repositorioDeModelos = repositorioDeModelos;
        }

        public async Task<Unit> Handle(DescricaoDeRecursoDeModeloAlterado request, CancellationToken cancellationToken)
        {
            var modelo = await repositorioDeModelos.ObtemModelo(request.Codigo);

            modelo.When(request);

            //modelo.AlteraDescricaoDeRecurso(request.Id, request.Descricao);

            await repositorioDeModelos.Update(modelo);

            return Unit.Value;
        }
    }
}
