using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Ateliex.Decisoes.Comerciais
{
    public class PlanosComerciaisObservableCollection : ExtendedObservableCollection<PlanoComercialViewModel>
    {
        private readonly IRepositorioDePlanosComerciais repositorioDePlanosComerciais;

        public PlanosComerciaisObservableCollection(IRepositorioDePlanosComerciais repositorioDePlanosComerciais)
        {
            this.repositorioDePlanosComerciais = repositorioDePlanosComerciais;
        }

        public async Task Load()
        {
            //var modelos = await repositorioDeModelos.ObtemModelos();

            //var list = modelos.Select(p => ModeloViewModel.From(p, repositorioDeModelos));

            //foreach (var item in list)
            //{
            //    this.Add(item);
            //}

            await Task.Run(() => { });
        }

        //protected override object AddNewCore()
        //{
        //    var model = new PlanoComercial(
        //        Guid.NewGuid().ToString(),
        //        null,
        //        6000,
        //        20
        //    );

        //    var viewModel = PlanoComercialViewModel.From(model);

        //    OnAddNew(viewModel);

        //    return viewModel;
        //}

        protected override async void OnAddNew(PlanoComercialViewModel viewModel)
        {
            //item.BindingList = this;

            var model = new PlanoComercial(
                            Guid.NewGuid().ToString(),
                            null,
                            6000
                        );

            viewModel.model = model;

            await repositorioDePlanosComerciais.Add(model);

            //viewModel.Itens.planoComercial = viewModel;

            base.OnAddNew(viewModel);
        }

        protected override async void OnRemoveItem(PlanoComercialViewModel viewModel)
        {
            await repositorioDePlanosComerciais.Remove(viewModel.model);

            base.OnRemoveItem(viewModel);
        }

        //public override async Task SaveChanges()
        //{
        //    try
        //    {
        //        await unitOfWork.Commit();

        //        SetStatus($"PlanoComercial salvo com sucesso.");
        //    }
        //    catch (Exception ex)
        //    {
        //        SetStatus(ex.Message);
        //    }

        //    //var newItems = GetItemsBy(ObjectState.New);

        //    //foreach (var newItem in newItems)
        //    //{
        //    //    try
        //    //    {
        //    //        await planosComerciaisLocalService.Add(newItem.model);

        //    //        SetStatus($"Novo planoComercial '{newItem.model.Id}' cadastrado com sucesso.");
        //    //    }
        //    //    catch (Exception ex)
        //    //    {
        //    //        SetStatus(ex.Message);
        //    //    }
        //    //}

        //    ////

        //    //var modifiedItems = GetItemsBy(ObjectState.Modified);

        //    //foreach (var modifiedItem in modifiedItems)
        //    //{
        //    //    try
        //    //    {
        //    //        await planosComerciaisLocalService.Update(modifiedItem.model);

        //    //        SetStatus($"PlanoComercial '{modifiedItem.Id}' atualizado com sucesso.");
        //    //    }
        //    //    catch (Exception ex)
        //    //    {
        //    //        SetStatus(ex.Message);
        //    //    }
        //    //}

        //    ////

        //    //var deletedItems = GetItemsBy(ObjectState.Deleted);

        //    //foreach (var deletedItem in deletedItems)
        //    //{
        //    //    try
        //    //    {
        //    //        await planosComerciaisLocalService.Remove(deletedItem.model);

        //    //        SetStatus($"PlanoComercial '{deletedItem.Id}' excluído com sucesso.");
        //    //    }
        //    //    catch (Exception ex)
        //    //    {
        //    //        SetStatus(ex.Message);
        //    //    }
        //    //}
        //}
    }
}
