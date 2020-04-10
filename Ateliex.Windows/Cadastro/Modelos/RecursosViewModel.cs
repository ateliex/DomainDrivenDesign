using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.PresentationModel;
using System.Text;

namespace Ateliex.Cadastro.Modelos
{
    public class RecursosViewModel : ViewModelCollection<RecursoViewModel>
    {
        protected ModeloViewModel modeloViewModel;

        internal void SetAggregate(ModeloViewModel modeloViewModel)
        {
            this.modeloViewModel = modeloViewModel;
        }

        internal ModeloViewModel GetAggregate()
        {
            return modeloViewModel;
        }

        public RecursosViewModel(IList<RecursoViewModel> list)
            : base(list)
        {
            foreach (var item in list)
            {
                item.SetCollection(this);
            }
        }

        protected override void OnAddNew(RecursoViewModel viewModel)
        {
            modeloViewModel.SetAsModified();

            var modelo = modeloViewModel.GetModel();

            //

            var recursoAdicionadoHandler = new Action<Recurso>(recurso =>
            {
                viewModel.SetModel(recurso);
            });

            modelo.RecursoAdicionado += recursoAdicionadoHandler;

            modelo.AdicionaRecurso(TipoDeRecurso.Material, "Custo #", 100, 1);

            modelo.RecursoAdicionado -= recursoAdicionadoHandler;

            //

            viewModel.SetCollection(this);

            //viewModel.ModeloId = planoComercial.Id;

            base.OnAddNew(viewModel);
        }

        protected override void OnRemoveItem(RecursoViewModel viewModel)
        {
            var modelo = modeloViewModel.GetModel();

            //

            modelo.RemoveRecurso(viewModel.GetModel());

            //

            base.OnRemoveItem(viewModel);
        }
    }
}
