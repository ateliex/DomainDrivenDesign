using Ateliex.Cadastro.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ateliex.Decisoes.Comerciais
{
    public class ItensDePlanoComercialObservableCollection : ExtendedObservableCollection<ItemDePlanoComercialViewModel>
    {
        protected internal PlanoComercialViewModel planoComercial;

        protected internal IRepositorioDePlanosComerciais repositorioDePlanosComerciais;

        protected internal IRepositorioDeModelos repositorioDeModelos;

        public ItensDePlanoComercialObservableCollection(IRepositorioDePlanosComerciais repositorioDePlanosComerciais, IRepositorioDeModelos repositorioDeModelos)
        {
            this.repositorioDePlanosComerciais = repositorioDePlanosComerciais;

            this.repositorioDeModelos = repositorioDeModelos;
        }

        public bool Contains(ModeloViewModel modelo)
        {
            var contains = planoComercial.model.ExisteItemDoModelo(modelo.modelo);

            return contains;
        }

        public ItemDePlanoComercialViewModel AdicionaItem(ModeloViewModel modelo)
        {
            var model = planoComercial.model.AdicionaItem(modelo.modelo);

            var viewModel = ItemDePlanoComercialViewModel.From(model, repositorioDePlanosComerciais, repositorioDeModelos);

            viewModel.itemDePlanoComercial = model;

            Add(viewModel);

            return viewModel;
        }

        protected override void OnAddNew(ItemDePlanoComercialViewModel viewModel)
        {
            //var model = planoComercial.model.AdicionaItem(null);

            //viewModel.model = model;

            //viewModel.PlanoComercialId = planoComercial.Id;

            base.OnAddNew(viewModel);
        }

        protected override void OnRemoveItem(ItemDePlanoComercialViewModel viewModel)
        {
            planoComercial.model.RemoveItem(viewModel.itemDePlanoComercial);

            base.OnRemoveItem(viewModel);
        }
    }
}
