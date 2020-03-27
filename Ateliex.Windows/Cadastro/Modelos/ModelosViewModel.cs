using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.DomainModel;
using System.Linq;
using System.PresentationModel;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Input;

namespace Ateliex.Cadastro.Modelos
{
    public class ModelosViewModel : ViewModelCollection<ModeloViewModel>
    {
        private readonly IEventStore eventStore;

        private readonly IRepositorioDeModelos repositorioDeModelos;

        public ModelosViewModel(IEventStore eventStore, IRepositorioDeModelos repositorioDeModelos)
            : base()
        {
            this.eventStore = eventStore;

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

            var model = new Modelo(Guid.NewGuid().ToString(), "Modelo #");

            viewModel.SetModel(model, repositorioDeModelos);

            await repositorioDeModelos.Add(model);

            //viewModel.Itens.planoComercial = viewModel;

            base.OnAddNew(viewModel);
        }

        protected override async void OnRemoveItem(ModeloViewModel viewModel)
        {
            await repositorioDeModelos.Remove(viewModel.GetModel());

            base.OnRemoveItem(viewModel);
        }

        public override async Task SaveAll()
        {
            try
            {
                //await unitOfWork.Commit();

                SetStatus($"Modelo salvo com sucesso.");
            }
            catch (Exception ex)
            {
                SetStatus(ex.Message);
            }

            var newItems = GetItemsBy(ObjectState.New);

            foreach (var newItem in newItems)
            {
                try
                {
                    var modelo = newItem.GetModel();

                    try
                    {
                        eventStore.AppendToStream(modelo.Id, modelo.Version, modelo.Changes);
                    }
                    catch (EventStoreConcurrencyException ex)
                    {
                        foreach (var failedEvent in modelo.Changes)
                        {
                            foreach (var succededEvent in ex.StoreEvents)
                            {
                                if (ConflictsWith(failedEvent, succededEvent))
                                {
                                    var message = $"Conflict between ${failedEvent} and {succededEvent}";

                                    throw new RealConcurrencyException(ex);
                                }
                            }
                        }

                        eventStore.AppendToStream(modelo.Id, ex.StoreVersion, modelo.Changes);
                    }

                    //await planosComerciaisLocalService.Add(newItem.model);

                    SetStatus($"Novo modelo '{modelo.Id}' cadastrado com sucesso.");
                }
                catch (Exception ex)
                {
                    SetStatus(ex.Message);
                }
            }

            ////

            //var modifiedItems = GetItemsBy(ObjectState.Modified);

            //foreach (var modifiedItem in modifiedItems)
            //{
            //    try
            //    {
            //        await planosComerciaisLocalService.Update(modifiedItem.model);

            //        SetStatus($"Modelo '{modifiedItem.Id}' atualizado com sucesso.");
            //    }
            //    catch (Exception ex)
            //    {
            //        SetStatus(ex.Message);
            //    }
            //}

            ////

            //var deletedItems = GetItemsBy(ObjectState.Deleted);

            //foreach (var deletedItem in deletedItems)
            //{
            //    try
            //    {
            //        await planosComerciaisLocalService.Remove(deletedItem.model);

            //        SetStatus($"Modelo '{deletedItem.Id}' excluído com sucesso.");
            //    }
            //    catch (Exception ex)
            //    {
            //        SetStatus(ex.Message);
            //    }
            //}
        }

        private bool ConflictsWith(IEvent event1, IEvent event2)
        {
            return event1.GetType() == event2.GetType();
        }
    }
}
