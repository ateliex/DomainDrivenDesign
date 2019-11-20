using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Ateliex.Cadastro.Modelos
{
    public class ModelosObservableCollection : ExtendedObservableCollection<ModeloViewModel>
    {
        private readonly IRepositorioDeModelos repositorioDeModelos;

        public ModelosObservableCollection(IRepositorioDeModelos repositorioDeModelos)
            : base()
        {
            this.repositorioDeModelos = repositorioDeModelos;
        }

        public async Task Load()
        {
            var modelos = await repositorioDeModelos.ObtemModelos();

            var list = modelos.Select(p => ModeloViewModel.From(p, repositorioDeModelos));

            foreach (var item in list)
            {
                this.Items.Add(item);
            }

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

            await Task.Run(() => { });
        }

        //protected override object AddNewCore()
        //{
        //    var model = new Modelo(
        //        Guid.NewGuid().ToString(),
        //        null,
        //        6000,
        //        20
        //    );

        //    var viewModel = ModeloViewModel.From(model);

        //    OnAddNew(viewModel);

        //    return viewModel;
        //}

        protected override async void OnAddNew(ModeloViewModel viewModel)
        {
            //item.BindingList = this;

            var model = new Modelo(
                            Guid.NewGuid().ToString(),
                            "Modelo #"
                        );

            viewModel.modelo = model;

            await repositorioDeModelos.Add(model);

            //viewModel.Itens.planoComercial = viewModel;

            base.OnAddNew(viewModel);
        }

        protected override async void OnRemoveItem(ModeloViewModel viewModel)
        {
            await repositorioDeModelos.Remove(viewModel.modelo);

            base.OnRemoveItem(viewModel);
        }

        //public override async Task SaveChanges()
        //{
        //    try
        //    {
        //        //await unitOfWork.Commit();

        //        SetStatus($"Modelo salvo com sucesso.");
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

        //    //        SetStatus($"Modelo '{modifiedItem.Id}' atualizado com sucesso.");
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

        //    //        SetStatus($"Modelo '{deletedItem.Id}' excluído com sucesso.");
        //    //    }
        //    //    catch (Exception ex)
        //    //    {
        //    //        SetStatus(ex.Message);
        //    //    }
        //    //}
        //}
    }
}
