﻿using System;
using System.PresentationModel;

namespace Ateliex.Cadastro.Modelos
{
    public class RecursoViewModel : ViewModel //, IEditableObject
    {
        private ModeloViewModel aggregate;

        internal void SetAggregate(ModeloViewModel aggregate)
        {
            this.aggregate = aggregate;
        }

        private Recurso recurso;

        internal Recurso GetModel()
        {
            return recurso;
        }

        internal void SetModel(Recurso recurso)
        {
            this.recurso = recurso;

            id = recurso.Id.ToString();

            descricao = recurso.Descricao;
        }

        public string ModeloCodigo
        {
            get { return recurso.Modelo.Codigo.Valor; }
        }

        protected internal string id;
        public string Id
        {
            get { return id; }
            set
            {
                id = value;

                OnPropertyChanged();

                try
                {
                    //recurso.AlteraId(id);

                    ClearErrors("Id");
                }
                catch (Exception ex)
                {
                    RaiseErrorsChanged("Id", ex);
                }
            }
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
                    aggregate.SetAsModified();

                    var modelo = aggregate.GetModel();

                    //

                    recurso.AlteraTipo(value);

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
                    aggregate.SetAsModified();

                    var modelo = aggregate.GetModel();

                    //

                    recurso.AlteraDescricao(value);

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
                    var modelo = aggregate.GetModel();

                    //

                    var value2 = Convert.ToDecimal(value);

                    recurso.AlteraCusto(value2);

                    //

                    OnPropertyChanged("CustoPorUnidade");

                    aggregate.OnPropertyChanged("CustoDeProducao");

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
                    var modelo = aggregate.GetModel();

                    //

                    var value2 = Convert.ToInt32(value);

                    recurso.AlteraUnidades(value2);

                    //

                    OnPropertyChanged("CustoPorUnidade");

                    aggregate.OnPropertyChanged("CustoDeProducao");

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
