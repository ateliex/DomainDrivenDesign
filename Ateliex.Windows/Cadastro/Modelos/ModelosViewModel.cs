using Ateliex.Cadastro.Modelos.ConsultaDeModelos;
using MediatR;
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
        private readonly IMediator mediator;

        private readonly IEventStore eventStore;

        public ModelosViewModel(IMediator mediator, IEventStore eventStore, IConsultaDeModelos consultaDeModelos)
            : base()
        {
            this.mediator = mediator;

            this.eventStore = eventStore;

            var @modelos = consultaDeModelos.ConsultaModelos(new SolicitacaoDeConsultaDeModelos());

            @modelos.Subscribe(modelos => Load(modelos));
        }

        public void Load(Modelo[] modelos)
        {
            var list = modelos.Select(modelo => ModeloViewModel.From(modelo));

            //Items.Clear();

            foreach (var item in list)
            {
                Items.Add(item);
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

        protected override void OnAddNew(ModeloViewModel viewModel)
        {
            //item.BindingList = this;

            var codigo = new CodigoDeModelo(Guid.NewGuid().ToString());

            var model = new Modelo(codigo, "Modelo #");

            viewModel.SetModel(model);

            //await repositorioDeModelos.Add(model);

            //viewModel.Itens.planoComercial = viewModel;

            base.OnAddNew(viewModel);
        }

        protected override void OnRemoveItem(ModeloViewModel viewModel)
        {
            var model = viewModel.GetModel();

            model.Exclui();

            //await repositorioDeModelos.Remove(viewModel.GetModel());

            base.OnRemoveItem(viewModel);
        }

        public override async Task SaveAll()
        {
            //try
            //{
            //    //await unitOfWork.Commit();

            //    SetStatus($"Modelo salvo com sucesso.");
            //}
            //catch (Exception ex)
            //{
            //    SetStatus(ex.Message);
            //}

            //

            var newItems = GetItemsBy(ObjectState.New);

            await Task.WhenAll(newItems.Select(newItem => Save(newItem)));

            //

            var modifiedItems = GetItemsBy(ObjectState.Modified);

            await Task.WhenAll(modifiedItems.Select(modifiedItem => Save(modifiedItem)));

            //

            var deletedItems = GetItemsBy(ObjectState.Deleted);

            await Task.WhenAll(deletedItems.Select(deletedItem => Save(deletedItem)));

            //

            await base.SaveAll();

            //SetStatus($"Modelo(s) salvo(s) com sucesso.");
        }

        public override async Task Save()
        {
            //var currentItem = new ModeloViewModel();

            //await Save(currentItem);

            await base.Save();
        }

        private async Task Save(ModeloViewModel viewModel)
        {
            try
            {
                var modelo = viewModel.GetModel();

                try
                {
                    eventStore.AppendToStream(modelo.Codigo, modelo.OriginalVersion, modelo.Changes);

                    foreach (var @event in modelo.Changes)
                    {
                        await mediator.Send(@event);
                    }
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

                    eventStore.AppendToStream(modelo.Codigo, ex.StoreVersion, modelo.Changes);
                }

                OnItemSaved(viewModel);

                SetStatus($"Modelo '{modelo.Codigo}' salvo com sucesso.");
            }
            catch (Exception ex)
            {
                viewModel.Error = ex.Message;

                SetStatus(ex.Message);
            }
        }

        private bool ConflictsWith(Event event1, Event event2)
        {
            return event1.GetType() == event2.GetType();
        }
    }
}
