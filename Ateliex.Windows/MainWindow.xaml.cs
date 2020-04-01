using Ateliex.Cadastro.Modelos;
using Ateliex.Decisoes.Comerciais;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace Ateliex
{
    public partial class MainWindow
    {
        //readonly InfrastructurePackage infrastructurePackage;

        public IServiceProvider ServiceProvider { get; private set; }

        public MainWindow(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            ServiceProvider = serviceProvider;
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
    }
}
