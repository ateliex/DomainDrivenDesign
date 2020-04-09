using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Ateliex.Cadastro.Modelos
{
    public partial class ModelosWindow
    {
        private readonly ModelosViewModel modelos;

        public ModelosWindow(ModelosViewModel modelos)
        {
            InitializeComponent();

            this.modelos = modelos;

            modelos.StatusChanged += SetStatusBar;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CollectionViewSource modelosViewSource = ((CollectionViewSource)(this.FindResource("modelosViewSource")));

            modelosViewSource.Source = modelos;
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
            await modelos.Save();
        }

        private async void SaveAllButton_Click(object sender, RoutedEventArgs e)
        {
            await modelos.SaveAll();
        }
    }

    public class ModeloValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            ModeloViewModel viewModel = (value as BindingGroup).Items[0] as ModeloViewModel;

            if (viewModel.HasErrors)
            {
                return new ValidationResult(false, viewModel.Error);
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
