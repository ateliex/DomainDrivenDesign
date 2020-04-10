using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.PresentationModel;

namespace Ateliex.Cadastro.Modelos
{
    public class ModeloViewModel : ViewModel, INotifyPropertyChanged, IEditableObject
    {
        private Modelo modelo;

        internal Modelo GetModel()
        {
            return modelo;
        }

        internal void SetModel(Modelo modelo)
        {
            this.modelo = modelo;

            codigo = modelo.Codigo.Valor;

            nome = modelo.Nome;
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

                    ClearErrors("Nome");

                    OnPropertyChanged("CurrentVersion");
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

        public long OriginalVersion
        {
            get { return modelo.OriginalVersion; }
        }

        public long CurrentVersion
        {
            get { return modelo.CurrentVersion; }
        }

        public override void OnSave()
        {
            modelo.OnSave();

            OnPropertyChanged("OriginalVersion");

            base.OnSave();
        }

        public RecursosViewModel Recursos { get; set; }

        public ModeloViewModel()
        {
            Recursos = new RecursosViewModel(new List<RecursoViewModel>() { });

            Recursos.SetAggregate(this);
        }

        public static ModeloViewModel From(Modelo modelo)
        {
            var recursos = modelo.Recursos.Select(p => RecursoViewModel.From(p)).ToList();

            var recursosCollection = new RecursosViewModel(recursos);

            var viewModel = new ModeloViewModel
            {
                modelo = modelo,
                codigo = modelo.Codigo.Valor,
                nome = modelo.Nome,
                Recursos = recursosCollection,
            };

            recursosCollection.SetAggregate(viewModel);

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
