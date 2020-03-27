using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.PresentationModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Transactions;

namespace Ateliex.Cadastro.Modelos
{
    public class ModeloViewModel : ViewModel, INotifyPropertyChanged, IEditableObject
    {
        private Modelo modelo;

        private IRepositorioDeModelos repositorioDeModelos;

        internal Modelo GetModel()
        {
            return modelo;
        }

        internal IRepositorioDeModelos GetRepository()
        {
            return repositorioDeModelos;
        }

        internal void SetModel(Modelo modelo, IRepositorioDeModelos repositorioDeModelos)
        {
            this.modelo = modelo;

            codigo = modelo.Codigo;

            nome = modelo.Nome;

            this.repositorioDeModelos = repositorioDeModelos;
        }

        protected internal string codigo;
        [Required(ErrorMessage = "Teste: Código Obrigatório")]
        public string Codigo
        {
            get { return codigo; }
            set
            {
                codigo = value;

                OnPropertyChanged();

                try
                {
                    var codigo = new CodigoDeModelo(value);

                    modelo.AlteraCodigo(codigo);

                    //repositorioDeModelos.Update(modelo);

                    ClearErrors("Codigo");
                }
                catch (Exception ex)
                {
                    RaiseErrorsChanged("Codigo", ex);
                }
            }
        }

        protected internal string nome;
        [Required(ErrorMessage = "Teste: Nome Obrigatório")]
        public string Nome
        {
            get { return nome; }
            set
            {
                nome = value;

                OnPropertyChanged();

                try
                {
                    modelo.AlteraNome(value);

                    //repositorioDeModelos.Update(modelo);

                    ClearErrors("Nome");
                }
                catch (Exception ex)
                {
                    RaiseErrorsChanged("Nome", ex);
                }
            }
        }

        public decimal CustoDeProducao
        {
            get { return modelo.CustoDeProducao; }
        }

        public RecursosViewModel Recursos { get; set; }

        public ModeloViewModel()
        {
            Recursos = new RecursosViewModel(new List<RecursoViewModel>() { });

            Recursos.modeloViewModel = this;
        }

        public static ModeloViewModel From(Modelo modelo, IRepositorioDeModelos repositorioDeModelos)
        {
            var recursos = modelo.Recursos.Select(p => RecursoViewModel.From(p)).ToList();

            var recursosObservableCollection = new RecursosViewModel(recursos);

            var viewModel = new ModeloViewModel
            {
                modelo = modelo,
                repositorioDeModelos = repositorioDeModelos,
                codigo = modelo.Codigo,
                nome = modelo.Nome,
                Recursos = recursosObservableCollection,
            };

            recursosObservableCollection.modeloViewModel = viewModel;

            return viewModel;
        }

        private bool inEdidt;

        public void BeginEdit()
        {
            if (inEdidt)
            {
                return;
            }

            inEdidt = true;
        }

        public void EndEdit()
        {
            if (!inEdidt)
            {
                return;
            }

            inEdidt = false;
        }

        public void CancelEdit()
        {
            if (!inEdidt)
            {
                return;
            }

            inEdidt = false;

            nome = modelo.Nome;
        }
    }
}
