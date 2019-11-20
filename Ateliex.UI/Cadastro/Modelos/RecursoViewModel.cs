using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ateliex.Cadastro.Modelos
{
    public class RecursoViewModel : ObservableObject //, IEditableObject
    {
        protected internal RecursosObservableCollection collection;

        protected internal Recurso recurso;

        public string ModeloCodigo
        {
            get { return recurso.Modelo.Codigo; }
        }

        private TipoDeRecurso tipo;
        public TipoDeRecurso Tipo
        {
            get { return tipo; }
            set
            {
                tipo = value;

                OnPropertyChanged();

                try
                {
                    var modelo = collection.modeloViewModel.modelo;

                    var repositorioDeModelos = collection.modeloViewModel.repositorioDeModelos;

                    //

                    recurso.DefineTipo(value);

                    repositorioDeModelos.Update(modelo);

                    //

                    ClearErrors("Tipo");
                }
                catch (Exception ex)
                {
                    RaiseErrorsChanged("Tipo", ex);
                }
            }
        }

        private string descricao;
        public string Descricao
        {
            get { return descricao; }
            set
            {
                descricao = value;

                OnPropertyChanged();

                try
                {
                    var modelo = collection.modeloViewModel.modelo;

                    var repositorioDeModelos = collection.modeloViewModel.repositorioDeModelos;

                    //

                    recurso.DefineDescricao(value);

                    repositorioDeModelos.Update(modelo);

                    //

                    ClearErrors("Descricao");
                }
                catch (Exception ex)
                {
                    RaiseErrorsChanged("Descricao", ex);
                }
            }
        }

        private string custo;
        public string Custo
        {
            get { return custo; }
            set
            {
                custo = value;

                OnPropertyChanged();

                try
                {
                    var modelo = collection.modeloViewModel.modelo;

                    var repositorioDeModelos = collection.modeloViewModel.repositorioDeModelos;

                    //

                    var value2 = Convert.ToDecimal(value);

                    recurso.DefineCusto(value2);

                    repositorioDeModelos.Update(modelo);

                    //

                    OnPropertyChanged("CustoPorUnidade");

                    collection.modeloViewModel.OnPropertyChanged("CustoDeProducao");

                    ClearErrors("Valor");
                }
                catch (Exception ex)
                {
                    RaiseErrorsChanged("Valor", ex);
                }
            }
        }

        private string unidades;
        public string Unidades
        {
            get { return unidades; }
            set
            {
                unidades = value;

                OnPropertyChanged();

                try
                {
                    var modelo = collection.modeloViewModel.modelo;

                    var repositorioDeModelos = collection.modeloViewModel.repositorioDeModelos;

                    //

                    var value2 = Convert.ToInt32(value);

                    recurso.DefineUnidades(value2);

                    repositorioDeModelos.Update(modelo);

                    //

                    OnPropertyChanged("CustoPorUnidade");

                    collection.modeloViewModel.OnPropertyChanged("CustoDeProducao");

                    ClearErrors("Unidades");
                }
                catch (Exception ex)
                {
                    RaiseErrorsChanged("Unidades", ex);
                }
            }
        }

        public decimal CustoPorUnidade
        {
            get { return recurso.CustoPorUnidade; }
        }

        public RecursoViewModel()
        {

        }

        public static RecursoViewModel From(Recurso recurso)
        {
            var viewModel = new RecursoViewModel
            {
                recurso = recurso,
                tipo = recurso.Tipo,
                descricao = recurso.Descricao,
                custo = recurso.Custo.ToString(),
                unidades = recurso.Unidades.ToString(),
            };

            return viewModel;
        }
    }
}
