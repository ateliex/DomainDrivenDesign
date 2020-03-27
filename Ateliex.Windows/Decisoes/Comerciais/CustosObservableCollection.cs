using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.PresentationModel;
using System.Text;

namespace Ateliex.Decisoes.Comerciais
{
    public class CustosObservableCollection : ViewModelCollection<CustoViewModel>
    {
        protected internal PlanoComercialViewModel planoComercial;

        public CustosObservableCollection(IList<CustoViewModel> list)
            : base(list)
        {
            foreach (var item in list)
            {
                item.collection = this;
            }
        }

        protected override void OnAddNew(CustoViewModel viewModel)
        {
            var model = planoComercial.model.AdicionaCusto(TipoDeCusto.Fixo, "Custo #");

            viewModel.collection = this;

            viewModel.model = model;

            //viewModel.PlanoComercialId = planoComercial.Id;

            base.OnAddNew(viewModel);
        }

        protected override void OnRemoveItem(CustoViewModel viewModel)
        {
            planoComercial.model.RemoveCusto(viewModel.model);

            base.OnRemoveItem(viewModel);
        }
    }
}
