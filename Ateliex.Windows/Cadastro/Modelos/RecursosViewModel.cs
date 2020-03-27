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
                item.collection = this;
            }
        }

        protected override void OnAddNew(RecursoViewModel viewModel)
        {
            var modelo = modeloViewModel.GetModel();

            var repositorioDeModelos = modeloViewModel.GetRepository();

            //

            var recurso = modelo.AdicionaRecurso(TipoDeRecurso.Material, "Custo #", 100, 1);

            repositorioDeModelos.Update(modelo);

            //

            viewModel.collection = this;

            viewModel.recurso = recurso;

            //viewModel.ModeloId = planoComercial.Id;

            base.OnAddNew(viewModel);
        }

        protected override void OnRemoveItem(RecursoViewModel viewModel)
        {
            var modelo = modeloViewModel.GetModel();

            var repositorioDeModelos = modeloViewModel.GetRepository();

            //

            modelo.RemoveRecurso(viewModel.recurso);

            repositorioDeModelos.Update(modelo);

            //

            base.OnRemoveItem(viewModel);
        }
    }
}
