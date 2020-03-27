using System;
using System.Collections.Generic;
using System.DomainModel;
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
    public partial class ModelosWindow
    {
        private readonly ModelosViewModel modelos;

        public ModelosWindow(ModelosViewModel modelos)
        {
            this.modelos = modelos;

            InitializeComponent();

            modelos.StatusChanged += SetStatusBar;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CollectionViewSource modelosViewSource = ((CollectionViewSource)(this.FindResource("ModelosViewSource")));

            modelosViewSource.Source = modelos;

            await modelos.Load();
        }

        private void SetStatusBar(string value)
        {
            statusBarLabel.Content = value;

            //statusBarTimer.Enabled = true;
        }

        private void AdicionarModeloButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = new ModeloViewModel();

            modelos.Add(viewModel);
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //CollectionViewSource modelosViewSource = ((CollectionViewSource)(this.FindResource("ModelosViewSource")));

            //var observableCollection = (ModelosViewModel)modelosViewSource.Source;

            await modelos.Save();
        }

        private async void SaveAllButton_Click(object sender, RoutedEventArgs e)
        {
            //CollectionViewSource modelosViewSource = ((CollectionViewSource)(this.FindResource("ModelosViewSource")));

            //var observableCollection = (ModelosViewModel)modelosViewSource.Source;

            await modelos.SaveAll();
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
