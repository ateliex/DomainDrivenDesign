using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Ateliex.Cadastro.Modelos
{
    public class RecursoDeModeloAdicionadoHandler : IRequestHandler<RecursoDeModeloAdicionado>
    {
        private readonly IRepositorioDeModelos repositorioDeModelos;

        public RecursoDeModeloAdicionadoHandler(IRepositorioDeModelos repositorioDeModelos)
        {
            this.repositorioDeModelos = repositorioDeModelos;
        }

        public async Task<Unit> Handle(RecursoDeModeloAdicionado request, CancellationToken cancellationToken)
        {
            var modelo = await repositorioDeModelos.ObtemModelo(request.Codigo);

            modelo.When(request);

            //modelo.AdicionaRecurso(request.Tipo, request.Descricao, request.Custo, request.Quantidade);

            await repositorioDeModelos.Update(modelo);

            return Unit.Value;
        }
    }
}
