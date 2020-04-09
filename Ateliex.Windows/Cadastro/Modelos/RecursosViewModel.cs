using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.PresentationModel;
using System.Text;

namespace Ateliex.Cadastro.Modelos
{
    public class RecursosViewModel : ViewModelCollection<RecursoViewModel>
    {
        protected internal ModeloViewModel modeloViewModel;

        public RecursosViewModel(IList<RecursoViewModel> list)
            : base(list)
        {
            foreach (var item in list)
            {
                item.SetAggregate(modeloViewModel);
            }
        }

        protected override void OnAddNew(RecursoViewModel viewModel)
        {
            modeloViewModel.SetAsModified();

            var modelo = modeloViewModel.GetModel();

            //var repositorioDeModelos = modeloViewModel.GetRepository();

            //

            var recursoAdicionadoHandler = new Action<Recurso>(recurso =>
            {
                viewModel.SetModel(recurso);
            });

            modelo.RecursoAdicionado += recursoAdicionadoHandler;

            modelo.AdicionaRecurso(TipoDeRecurso.Material, "Custo #", 100, 1);

            modelo.RecursoAdicionado -= recursoAdicionadoHandler;

            //repositorioDeModelos.Update(modelo);

            //

            viewModel.SetAggregate(modeloViewModel);

            //viewModel.ModeloId = planoComercial.Id;

            base.OnAddNew(viewModel);
        }

        protected override void OnRemoveItem(RecursoViewModel viewModel)
        {
            var modelo = modeloViewModel.GetModel();

            //var repositorioDeModelos = modeloViewModel.GetRepository();

            //

            modelo.RemoveRecurso(viewModel.GetModel());

            //repositorioDeModelos.Update(modelo);

            //

            base.OnRemoveItem(viewModel);
        }
    }
}
