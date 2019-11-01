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
        private readonly ModelosService modelosService;

        public ModelosWindow(
            ModelosService modelosService
        )
        {
            this.modelosService = modelosService;

            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var modelos = await modelosService.ObtemObservavelDeModelos();

            var list = modelos.Select(p => ModeloViewModel.From(p)).ToList();

            var observableCollection = new ModelosObservableCollection(
                modelosService,
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
