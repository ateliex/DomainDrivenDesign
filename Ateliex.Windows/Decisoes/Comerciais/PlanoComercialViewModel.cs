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

            Itens = new ItensDePlanoComercialObservableCollection(new List<ItemDePlanoComercialViewModel>() { });

            Itens.planoComercial = this;
        }

        public static PlanoComercialViewModel From(PlanoComercial planoComercial)
        {
            var custos = planoComercial.Custos.Select(p => CustoViewModel.From(p)).ToList();

            var custosObservableCollection = new CustosObservableCollection(custos);

            var itensDePlanoComercial = planoComercial.Itens.Select(p => ItemDePlanoComercialViewModel.From(p)).ToList();

            var itensDePlanoComercialObservableCollection = new ItensDePlanoComercialObservableCollection(itensDePlanoComercial);

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

    public class ItemDePlanoComercialViewModel : ObservableObject //, IEditableObject
    {
        protected internal ItemDePlanoComercial model;

        public string PlanoComercialCodigo
        {
            get { return model.PlanoComercial.Codigo; }
        }

        private ModeloViewModel modeloViewModel;
        public ModeloViewModel Modelo
        {
            get { return modeloViewModel; }
        }

        public decimal CustoDeProducao
        {
            get { return model.CustoDeProducao; }
        }

        public decimal? CustoDeProducaoSugerido
        {
            get { return model.CustoDeProducaoSugerido; }
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
            get { return model.Margem; }
            set
            {
                model.DefineMargem(value);

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

                    model.DefineMargemPercentual(value2);

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
            get { return model.TaxaDeMarcacao; }
        }

        private string taxaDeMarcacaoSugerida;
        public string TaxaDeMarcacaoSugerida
        {
            get { return (model.TaxaDeMarcacaoSugerida.HasValue ? model.TaxaDeMarcacaoSugerida.Value.ToString() : null); }
            set
            {
                taxaDeMarcacaoSugerida = value;

                OnPropertyChanged();

                try
                {
                    var value2 = Convert.ToDecimal(value);

                    model.DefineMargemPercentual(value2);

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
            get { return model.PrecoDeVenda; }
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

                    model.DefinePrecoDeVendaDesejado(value2);

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

        public static ItemDePlanoComercialViewModel From(ItemDePlanoComercial itemDePlanoComercial)
        {
            var modeloViewModel = ModeloViewModel.From(itemDePlanoComercial.Modelo);

            var viewModel = new ItemDePlanoComercialViewModel
            {
                model = itemDePlanoComercial,
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

    public class PlanosComerciaisObservableCollection : ExtendedObservableCollection<PlanoComercialViewModel>
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IRepositorioDePlanosComerciais planosComerciaisLocalService;

        //private readonly IConsultaDePlanosComerciais consultaDePlanosComerciais;

        //private readonly IPlanejamentoComercial planejamentoComercial;

        public PlanosComerciaisObservableCollection()
            : base()
        {

        }

        public PlanosComerciaisObservableCollection(
            IUnitOfWork unitOfWork,
            IRepositorioDePlanosComerciais planosComerciaisLocalService,
            //IConsultaDePlanosComerciais consultaDePlanosComerciais,
            //IPlanejamentoComercial planejamentoComercial,
            IList<PlanoComercialViewModel> list
        )
            : base(list)
        {
            this.unitOfWork = unitOfWork;
            
            this.planosComerciaisLocalService = planosComerciaisLocalService;

            //this.consultaDePlanosComerciais = consultaDePlanosComerciais;

            //this.planejamentoComercial = planejamentoComercial;
        }

        public PlanosComerciaisObservableCollection(IList<PlanoComercialViewModel> list)
            : base(list)
        {

        }

        //protected override object AddNewCore()
        //{
        //    var model = new PlanoComercial(
        //        Guid.NewGuid().ToString(),
        //        null,
        //        6000,
        //        20
        //    );

        //    var viewModel = PlanoComercialViewModel.From(model);

        //    OnAddNew(viewModel);

        //    return viewModel;
        //}

        protected override async void OnAddNew(PlanoComercialViewModel viewModel)
        {
            //item.BindingList = this;

            var model = new PlanoComercial(
                            Guid.NewGuid().ToString(),
                            null,
                            6000
                        );

            viewModel.model = model;

            await planosComerciaisLocalService.Add(model);

            //viewModel.Itens.planoComercial = viewModel;

            base.OnAddNew(viewModel);
        }

        protected override async void OnRemoveItem(PlanoComercialViewModel viewModel)
        {
            await planosComerciaisLocalService.Remove(viewModel.model);

            base.OnRemoveItem(viewModel);
        }

        public override async Task SaveChanges()
        {
            try
            {
                await unitOfWork.Commit();

                SetStatus($"PlanoComercial salvo com sucesso.");
            }
            catch (Exception ex)
            {
                SetStatus(ex.Message);
            }

            //var newItems = GetItemsBy(ObjectState.New);

            //foreach (var newItem in newItems)
            //{
            //    try
            //    {
            //        await planosComerciaisLocalService.Add(newItem.model);

            //        SetStatus($"Novo planoComercial '{newItem.model.Id}' cadastrado com sucesso.");
            //    }
            //    catch (Exception ex)
            //    {
            //        SetStatus(ex.Message);
            //    }
            //}

            ////

            //var modifiedItems = GetItemsBy(ObjectState.Modified);

            //foreach (var modifiedItem in modifiedItems)
            //{
            //    try
            //    {
            //        await planosComerciaisLocalService.Update(modifiedItem.model);

            //        SetStatus($"PlanoComercial '{modifiedItem.Id}' atualizado com sucesso.");
            //    }
            //    catch (Exception ex)
            //    {
            //        SetStatus(ex.Message);
            //    }
            //}

            ////

            //var deletedItems = GetItemsBy(ObjectState.Deleted);

            //foreach (var deletedItem in deletedItems)
            //{
            //    try
            //    {
            //        await planosComerciaisLocalService.Remove(deletedItem.model);

            //        SetStatus($"PlanoComercial '{deletedItem.Id}' excluído com sucesso.");
            //    }
            //    catch (Exception ex)
            //    {
            //        SetStatus(ex.Message);
            //    }
            //}
        }
    }

    public class CustosObservableCollection : ExtendedObservableCollection<CustoViewModel>
    {
        protected internal PlanoComercialViewModel planoComercial;

        public CustosObservableCollection(IList<CustoViewModel> list)
            : base(list)
        {
            foreach (var item in list)
            {
                item.collection = this;
            }
        }

        protected override void OnAddNew(CustoViewModel viewModel)
        {
            var model = planoComercial.model.AdicionaCusto(TipoDeCusto.Fixo, "Custo #");

            viewModel.collection = this;

            viewModel.model = model;

            //viewModel.PlanoComercialId = planoComercial.Id;

            base.OnAddNew(viewModel);
        }

        protected override void OnRemoveItem(CustoViewModel viewModel)
        {
            planoComercial.model.RemoveCusto(viewModel.model);

            base.OnRemoveItem(viewModel);
        }
    }

    public class ItensDePlanoComercialObservableCollection : ExtendedObservableCollection<ItemDePlanoComercialViewModel>
    {
        protected internal PlanoComercialViewModel planoComercial;

        public ItensDePlanoComercialObservableCollection(IList<ItemDePlanoComercialViewModel> list)
            : base(list)
        {

        }

        public bool Contains(ModeloViewModel modelo)
        {
            var contains = planoComercial.model.ExisteItemDoModelo(modelo.model);

            return contains;
        }

        public ItemDePlanoComercialViewModel AdicionaItem(ModeloViewModel modelo)
        {
            var model = planoComercial.model.AdicionaItem(modelo.model);

            var viewModel = ItemDePlanoComercialViewModel.From(model);

            viewModel.model = model;

            Add(viewModel);

            return viewModel;
        }

        protected override void OnAddNew(ItemDePlanoComercialViewModel viewModel)
        {
            //var model = planoComercial.model.AdicionaItem(null);

            //viewModel.model = model;

            //viewModel.PlanoComercialId = planoComercial.Id;

            base.OnAddNew(viewModel);
        }

        protected override void OnRemoveItem(ItemDePlanoComercialViewModel viewModel)
        {
            planoComercial.model.RemoveItem(viewModel.model);

            base.OnRemoveItem(viewModel);
        }
    }
}
