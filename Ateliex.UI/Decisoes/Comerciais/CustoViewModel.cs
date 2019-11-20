using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ateliex.Decisoes.Comerciais
{
    public class CustoViewModel : ObservableObject //, IEditableObject
    {
        protected internal CustosObservableCollection collection;

        protected internal Custo model;

        public string PlanoComercialCodigo
        {
            get { return model.PlanoComercial.Codigo; }
        }

        private TipoDeCusto tipo;
        public TipoDeCusto Tipo
        {
            get { return tipo; }
            set
            {
                tipo = value;

                OnPropertyChanged();

                try
                {
                    model.DefineTipo(value);

                    OnPropertyChanged("ValorCalculado");

                    OnPropertyChanged("PercentualCalculado");

                    collection.planoComercial.OnPropertyChanged("CustoFixoTotal");

                    collection.planoComercial.OnPropertyChanged("CustoFixoPercentualTotal");

                    collection.planoComercial.OnPropertyChanged("CustoVariavelTotal");

                    collection.planoComercial.OnPropertyChanged("CustoVariavelPercentualTotal");

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
                    model.DefineDescricao(value);

                    ClearErrors("Descricao");
                }
                catch (Exception ex)
                {
                    RaiseErrorsChanged("Descricao", ex);
                }
            }
        }

        private string valor;
        public string Valor
        {
            get { return valor; }
            set
            {
                valor = value;

                OnPropertyChanged();

                try
                {
                    var value2 = Convert.ToDecimal(value);

                    model.DefineValor(value2);

                    OnPropertyChanged("ValorCalculado");

                    OnPropertyChanged("PercentualCalculado");

                    collection.planoComercial.OnPropertyChanged("CustoFixoTotal");

                    collection.planoComercial.OnPropertyChanged("CustoFixoPercentualTotal");

                    collection.planoComercial.OnPropertyChanged("CustoVariavelTotal");

                    collection.planoComercial.OnPropertyChanged("CustoVariavelPercentualTotal");

                    ClearErrors("Valor");
                }
                catch (Exception ex)
                {
                    RaiseErrorsChanged("Valor", ex);
                }
            }
        }

        private string percentual;
        public string Percentual
        {
            get { return percentual; }
            set
            {
                percentual = value;

                OnPropertyChanged();

                try
                {
                    var value2 = Convert.ToDecimal(value);

                    model.DefinePercentual(value2);

                    OnPropertyChanged("ValorCalculado");

                    OnPropertyChanged("PercentualCalculado");

                    collection.planoComercial.OnPropertyChanged("CustoFixoTotal");

                    collection.planoComercial.OnPropertyChanged("CustoFixoPercentualTotal");

                    collection.planoComercial.OnPropertyChanged("CustoVariavelTotal");

                    collection.planoComercial.OnPropertyChanged("CustoVariavelPercentualTotal");

                    ClearErrors("Percentual");
                }
                catch (Exception ex)
                {
                    RaiseErrorsChanged("Percentual", ex);
                }
            }
        }

        public decimal ValorCalculado
        {
            get { return model.ValorCalculado; }
        }

        public decimal PercentualCalculado
        {
            get { return model.PercentualCalculado; }
        }

        public CustoViewModel()
        {

        }

        public static CustoViewModel From(Custo custo)
        {
            var viewModel = new CustoViewModel
            {
                model = custo,
                tipo = custo.Tipo,
                descricao = custo.Descricao,
                valor = custo.Valor.ToString(),
                percentual = custo.Percentual.ToString(),
            };

            return viewModel;
        }
    }
}
