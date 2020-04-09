using Ateliex.Cadastro.Modelos.ConsultaDeModelos;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Ateliex.Cadastro.Modelos
{
    /// <summary>
    /// Interaction logic for ConsultaDeModelosWindow.xaml
    /// </summary>
    public partial class ConsultaDeModelosWindow : Window
    {
        private readonly IConsultaDeModelos consultaDeModelos;

        public ConsultaDeModelosWindow(IConsultaDeModelos consultaDeModelos)
        {
            this.consultaDeModelos = consultaDeModelos;

            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //var modelos = await consultaDeModelos.ObtemModelos();

            //var list = modelos.Select(p => ItemDeConsultaDeModeloViewModel.From(p, selecteds)).ToList();

            //CollectionViewSource modelosViewSource = ((CollectionViewSource)(this.FindResource("modelosViewSource")));

            //modelosViewSource.Source = list;
        }

        private IEnumerable<ModeloViewModel> selecteds;

        public void SetSelecteds(IEnumerable<ModeloViewModel> selecteds)
        {
            this.selecteds = selecteds;
        }

        public IEnumerable<ModeloViewModel> GetSelecteds()
        {
            var list = new List<ModeloViewModel>();

            foreach (var item in modelosDataGrid.Items)
            {
                var viewModel = item as ItemDeConsultaDeModeloViewModel;

                if (viewModel.Selected)
                {
                    list.Add(viewModel.Modelo);
                }
            }

            return list;
        }
    }

    public class ItemDeConsultaDeModeloViewModel
    {
        public bool Selected { get; set; }

        public ModeloViewModel Modelo { get; set; }

        public static ItemDeConsultaDeModeloViewModel From(Modelo modelo, IEnumerable<ModeloViewModel> selecteds)
        {
            var selected = selecteds.Any(p => p.Codigo == modelo.Codigo.Valor);

            //var modeloViewModel = ModeloViewModel.From(modelo);

            //var viewModel = new ItemDeConsultaDeModeloViewModel
            //{
            //    Selected = selected,
            //    Modelo = modeloViewModel,
            //};

            //return viewModel;

            return null;
        }
    }
}
