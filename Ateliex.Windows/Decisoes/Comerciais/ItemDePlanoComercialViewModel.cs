using Ateliex.Cadastro.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.PresentationModel;
using System.Text;

namespace Ateliex.Decisoes.Comerciais
{
    public class ItemDePlanoComercialViewModel : ViewModel //, IEditableObject
    {
        protected internal ItemDePlanoComercial itemDePlanoComercial;

        protected internal IRepositorioDePlanosComerciais repositorioDePlanosComerciais;

        public string PlanoComercialCodigo
        {
            get { return itemDePlanoComercial.PlanoComercial.Codigo; }
        }

        private ModeloViewModel modeloViewModel;
        public ModeloViewModel Modelo
        {
            get { return modeloViewModel; }
        }

        public decimal CustoDeProducao
        {
            get { return itemDePlanoComercial.CustoDeProducao; }
        }

        public decimal? CustoDeProducaoSugerido
        {
            get { return itemDePlanoComercial.CustoDeProducaoSugerido; }
        }

        //private string custoDeProducaoSugerido;
        //public string CustoDeProducaoSugerido
        //{
        //    get { return custoDeProducaoSugerido; }
        //    set
        //    {
        //        custoDeProducaoSugerido = value;

        //        OnPropertyChanged();

        //        try
        //        {
        //            var value2 = Convert.ToDecimal(value);

        //            //model.DefineCustoDeProducao(value2);

        //            OnPropertyChanged("PrecoDeVenda");

        //            ClearErrors("CustoDeProducaoSugerido");

        //            ClearErrors("PrecoDeVenda");
        //        }
        //        catch (Exception ex)
        //        {
        //            RaiseErrorsChanged("CustoDeProducaoSugerido", ex);

        //            RaiseErrorsChanged("PrecoDeVenda", ex);
        //        }
        //    }
        //}

        public decimal Margem
        {
            get { return itemDePlanoComercial.Margem; }
            set
            {
                itemDePlanoComercial.DefineMargem(value);

                OnPropertyChanged();
            }
        }

        private string margemPercentual;
        public string MargemPercentual
        {
            get { return margemPercentual; }
            set
            {
                margemPercentual = value;

                OnPropertyChanged();

                try
                {
                    var value2 = Convert.ToDecimal(value);

                    itemDePlanoComercial.DefineMargemPercentual(value2);

                    repositorioDePlanosComerciais.Update(itemDePlanoComercial.PlanoComercial);

                    OnPropertyChanged("TaxaDeMarcacao");

                    ClearErrors("MargemPercentual");

                    ClearErrors("TaxaDeMarcacao");
                }
                catch (Exception ex)
                {
                    RaiseErrorsChanged("MargemPercentual", ex);

                    RaiseErrorsChanged("TaxaDeMarcacao", ex);
                }
            }
        }

        public decimal TaxaDeMarcacao
        {
            get { return itemDePlanoComercial.TaxaDeMarcacao; }
        }

        private string taxaDeMarcacaoSugerida;
        public string TaxaDeMarcacaoSugerida
        {
            get { return (itemDePlanoComercial.TaxaDeMarcacaoSugerida.HasValue ? itemDePlanoComercial.TaxaDeMarcacaoSugerida.Value.ToString() : null); }
            set
            {
                taxaDeMarcacaoSugerida = value;

                OnPropertyChanged();

                try
                {
                    var value2 = Convert.ToDecimal(value);

                    itemDePlanoComercial.DefineMargemPercentual(value2);

                    repositorioDePlanosComerciais.Update(itemDePlanoComercial.PlanoComercial);

                    //OnPropertyChanged("TaxaDeMarcacao");

                    ClearErrors("TaxaDeMarcacaoSugerida");

                    //ClearErrors("TaxaDeMarcacao");
                }
                catch (Exception ex)
                {
                    RaiseErrorsChanged("TaxaDeMarcacaoSugerida", ex);

                    //RaiseErrorsChanged("TaxaDeMarcacao", ex);
                }
            }
        }

        public decimal PrecoDeVenda
        {
            get { return itemDePlanoComercial.PrecoDeVenda; }
        }

        private string precoDeVendaDesejado;
        public string PrecoDeVendaDesejado
        {
            get { return precoDeVendaDesejado; }
            set
            {
                precoDeVendaDesejado = value;

                OnPropertyChanged();

                try
                {
                    var value2 = Convert.ToDecimal(value);

                    itemDePlanoComercial.DefinePrecoDeVendaDesejado(value2);

                    repositorioDePlanosComerciais.Update(itemDePlanoComercial.PlanoComercial);

                    //OnPropertyChanged("PrecoDeVenda");

                    ClearErrors("PrecoDeVendaDesejado");

                    //ClearErrors("PrecoDeVenda");
                }
                catch (Exception ex)
                {
                    RaiseErrorsChanged("PrecoDeVendaDesejado", ex);

                    //RaiseErrorsChanged("PrecoDeVenda", ex);
                }
            }
        }

        public ItemDePlanoComercialViewModel()
        {
            
        }

        public static ItemDePlanoComercialViewModel From(ItemDePlanoComercial itemDePlanoComercial, IRepositorioDePlanosComerciais repositorioDePlanosComerciais, IRepositorioDeModelos repositorioDeModelos)
        {
            var modeloViewModel = ModeloViewModel.From(itemDePlanoComercial.Modelo);

            var viewModel = new ItemDePlanoComercialViewModel
            {
                itemDePlanoComercial = itemDePlanoComercial,
                repositorioDePlanosComerciais = repositorioDePlanosComerciais,
                //PlanoComercialId = itemDePlanoComercial.PlanoComercial.Id,
                modeloViewModel = modeloViewModel,
                //ModeloCodigo = itemDePlanoComercial.Modelo.Codigo,
                //ModeloNome = itemDePlanoComercial.Modelo.Nome,
                //CustoDeProducaoSugerido = itemDePlanoComercial.CustoDeProducaoSugerido.ToString(),
                Margem = itemDePlanoComercial.Margem,
                margemPercentual = itemDePlanoComercial.MargemPercentual.ToString(),
                taxaDeMarcacaoSugerida = (itemDePlanoComercial.TaxaDeMarcacaoSugerida.HasValue ? itemDePlanoComercial.TaxaDeMarcacaoSugerida.Value.ToString() : null),
                precoDeVendaDesejado = (itemDePlanoComercial.PrecoDeVendaDesejado.HasValue ? itemDePlanoComercial.PrecoDeVendaDesejado.Value.ToString() : null),
            };

            return viewModel;
        }
    }
}
