using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Ateliex.Cadastro.Modelos
{
    /// <summary>
    /// Interaction logic for ModelosWindow.xaml
    /// </summary>
    public partial class ModelosWindow
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IRepositorioDeModelos modelosLocalService;

        private readonly IConsultaDeModelos consultaDeModelos;

        //private readonly IPlanejamentoComercial planejamentoComercial;

        public ModelosWindow(
            IUnitOfWork unitOfWork,
            IRepositorioDeModelos modelosLocalService,
            IConsultaDeModelos consultaDeModelos
            //IPlanejamentoComercial planejamentoComercial
        )
        {
            this.unitOfWork = unitOfWork;
            
            this.modelosLocalService = modelosLocalService;

            this.consultaDeModelos = consultaDeModelos;

            //this.planejamentoComercial = planejamentoComercial;

            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var modelos = await consultaDeModelos.ObtemObservavelDeModelos();

            var list = modelos.Select(p => ModeloViewModel.From(p)).ToList();

            var observableCollection = new ModelosObservableCollection(
                unitOfWork,
                modelosLocalService,
                consultaDeModelos,
                //planejamentoComercial,
                list
            );

            //modelosBindingSource.DataSource = bindingList;

            observableCollection.StatusChanged += SetStatusBar;

            CollectionViewSource modelosViewSource = ((CollectionViewSource)(this.FindResource("modelosViewSource")));

            modelosViewSource.Source = observableCollection;
        }

        private void SetStatusBar(string value)
        {
            statusBarLabel.Content = value;

            //statusBarTimer.Enabled = true;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            CollectionViewSource modelosViewSource = ((CollectionViewSource)(this.FindResource("modelosViewSource")));

            var observableCollection = (ModelosObservableCollection)modelosViewSource.Source;

            await observableCollection.SaveChanges();
        }

        private void AdicionarModeloButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    //public class ModeloValidationRule : ValidationRule
    //{
    //    public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
    //    {
    //        ModeloViewModel viewModel = (value as BindingGroup).Items[0] as ModeloViewModel;

    //        if (viewModel.HasErrors)
    //        {
    //            return new ValidationResult(false, viewModel.Error);
    //        }
    //        else
    //        {
    //            return ValidationResult.ValidResult;
    //        }
    //    }
    //}
}
