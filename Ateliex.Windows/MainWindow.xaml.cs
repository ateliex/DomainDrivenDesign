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
            var planosComerciaisWindow = ServiceProvider.GetRequiredService<PlanosComerciaisWindow>();

            planosComerciaisWindow.Show();
        }

        private async void popularBancoDeDadosDeLeituraMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var events = eventStore.LoadAllEvents();

            foreach (var @event in events)
            {
                try
                {
                    await mediator.Send(@event);
                }
                catch (Exception)
                {

                }                
            }
        }

        private async void limparBancoDeDadosDeLeituraMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var serviceScopeFactory = ServiceProvider.GetRequiredService<IServiceScopeFactory>();

            await DbModule.LimpaBancoDeDadosDeLeitura(serviceScopeFactory);
        }

        private void abrirEventStoreMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var eventStoreWindow = ServiceProvider.GetRequiredService<EventStoreWindow>();

            eventStoreWindow.Show();
        }
    }
}
