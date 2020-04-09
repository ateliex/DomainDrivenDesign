using Ateliex.Cadastro.Modelos;
using Ateliex.Decisoes.Comerciais;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.DomainModel;
using System.Windows;

namespace Ateliex
{
    public partial class MainWindow
    {
        public IServiceProvider ServiceProvider { get; private set; }

        private readonly IMediator mediator;


        private readonly IEventStore eventStore;

        public MainWindow(IServiceProvider serviceProvider, IMediator mediator, IEventStore eventStore)
        {
            InitializeComponent();

            ServiceProvider = serviceProvider;

            this.mediator = mediator;

            this.eventStore = eventStore;
        }

        private void CadastroDeModelosMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var modelosWindow = ServiceProvider.GetRequiredService<ModelosWindow>();

            modelosWindow.Show();
        }

        private void PlanejamentoComercialMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var planosComerciaisForm = ServiceProvider.GetRequiredService<PlanosComerciaisWindow>();

            planosComerciaisForm.Show();
        }

        private async void popularBancoDeDadosDeLeituraMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var events = eventStore.GetAllEvents();

            foreach (var @event in events)
            {
                await mediator.Send(@event);
            }

            //var eventStreams = eventStore.GetAllEvents();

            //foreach (var eventStream in eventStreams)
            //{
            //    foreach (var @event in eventStream.Events)
            //    {
            //        await mediator.Send(@event);
            //    }

            //    //if (viewModel.State != ObjectState.Deleted)
            //    //{
            //    //    await mediator.Send(new VersionaModelo(modelo.Codigo));
            //    //}
            //}
        }

        private async void limparBancoDeDadosDeLeituraMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var serviceScopeFactory = ServiceProvider.GetRequiredService<IServiceScopeFactory>();

            await DbModule.LimpaBancoDeDadosDeLeitura(serviceScopeFactory);
        }
    }
}
