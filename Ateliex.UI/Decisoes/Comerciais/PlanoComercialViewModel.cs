using Ateliex.Cadastro.Modelos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Transactions;

namespace Ateliex.Decisoes.Comerciais
{
    public class PlanoComercialViewModel : ObservableObject, INotifyPropertyChanged, IEditableObject
    {
        protected internal PlanoComercial model;

        protected internal IRepositorioDePlanosComerciais repositorioDePlanosComerciais;

        protected internal IRepositorioDeModelos repositorioDeModelos;

        //public PlanosComerciaisBindingList BindingList { get; internal set; }

        public string Id
        {
            get { return model.Codigo; }
            //set
            //{
            //    OnPropertyChanged();
            //}
        }

        private string nome;
        [Required(ErrorMessage = "Teste")]
        public string Nome
        {
            get { return nome; }
            set
            {
                nome = value;

                OnPropertyChanged();

                try
                {
                    model.DefineNome(value);

                    ClearErrors("Nome");
                }
                catch (Exception ex)
                {
                    RaiseErrorsChanged("Nome", ex);
                }
            }
        }

        public decimal RendaBrutaMensal
        {
            get { return model.RendaBrutaMensal; }
            set
            {
                model.DefineRendaBrutaMensal(value);

                OnPropertyChanged();
            }
        }

        public decimal CustoFixoTotal
        {
            get { return model.CustoFixoTotal; }
        }

        public decimal CustoFixoPercentualTotal
        {
            get { return model.CustoFixoPercentualTotal; }
        }

        public decimal CustoVariavelTotal
        {
            get { return model.CustoVariavelTotal; }
        }

        public decimal CustoVariavelPercentualTotal
        {
            get { return model.CustoVariavelPercentualTotal; }
        }

        public decimal CustoPercentualTotal
        {
            get { return model.CustoPercentualTotal; }
        }

        public CustosObservableCollection Custos { get; set; }

        public ItensDePlanoComercialObservableCollection Itens { get; set; }

        public PlanoComercialViewModel()
        {
            Custos = new CustosObservableCollection(new List<CustoViewModel>() { });

            Custos.planoComercial = this;

            Itens = new ItensDePlanoComercialObservableCollection(repositorioDePlanosComerciais, repositorioDeModelos);

            Itens.planoComercial = this;
        }

        public static PlanoComercialViewModel From(PlanoComercial planoComercial, IRepositorioDePlanosComerciais repositorioDePlanosComerciais, IRepositorioDeModelos repositorioDeModelos)
        {
            var custos = planoComercial.Custos.Select(p => CustoViewModel.From(p)).ToList();

            var custosObservableCollection = new CustosObservableCollection(custos);

            var itensDePlanoComercial = planoComercial.Itens.Select(p => ItemDePlanoComercialViewModel.From(p, repositorioDePlanosComerciais, repositorioDeModelos)).ToList();

            var itensDePlanoComercialObservableCollection = new ItensDePlanoComercialObservableCollection(repositorioDePlanosComerciais, repositorioDeModelos);

            var viewModel = new PlanoComercialViewModel
            {
                model = planoComercial as PlanoComercial,
                //Id = planoComercial.Id,
                nome = planoComercial.Nome,
                RendaBrutaMensal = planoComercial.RendaBrutaMensal,
                Custos = custosObservableCollection,
                Itens = itensDePlanoComercialObservableCollection
            };

            custosObservableCollection.planoComercial = viewModel;

            itensDePlanoComercialObservableCollection.planoComercial = viewModel;

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

            nome = model.Nome;

            //margemPercentual = model.MargemPercentual.ToString();
        }
    }
}
