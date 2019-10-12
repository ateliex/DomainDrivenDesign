using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Ateliex.Cadastro.Modelos
{
    public class ModeloViewModel : ObservableObject, INotifyPropertyChanged, IEditableObject
    {
        protected internal Modelo model;

        private string codigo;
        [Required(ErrorMessage = "Teste")]
        public string Codigo
        {
            get { return codigo; }
            set
            {
                codigo = value;

                OnPropertyChanged();

                try
                {
                    model.DefineCodigo(value);

                    ClearErrors("Codigo");
                }
                catch (Exception ex)
                {
                    RaiseErrorsChanged("Codigo", ex);
                }
            }
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

        public decimal CustoDeProducao
        {
            get { return model.CustoDeProducao; }
        }

        public RecursosObservableCollection Recursos { get; set; }

        public ModeloViewModel()
        {
            Recursos = new RecursosObservableCollection(new List<RecursoViewModel>() { });

            Recursos.modelo = this;
        }

        public static ModeloViewModel From(Modelo modelo)
        {
            var recursos = modelo.Recursos.Select(p => RecursoViewModel.From(p)).ToList();

            var recursosObservableCollection = new RecursosObservableCollection(recursos);

            var viewModel = new ModeloViewModel
            {
                model = modelo as Modelo,
                codigo = modelo.Codigo,
                nome = modelo.Nome,
                Recursos = recursosObservableCollection,
            };

            recursosObservableCollection.modelo = viewModel;

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
        }
    }

    public class RecursoViewModel : ObservableObject //, IEditableObject
    {
        protected internal RecursosObservableCollection collection;

        protected internal Recurso model;

        public string ModeloCodigo
        {
            get { return model.Modelo.Codigo; }
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
                    model.DefineTipo(value);

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
                    var value2 = Convert.ToDecimal(value);

                    model.DefineCusto(value2);

                    OnPropertyChanged("CustoPorUnidade");

                    collection.modelo.OnPropertyChanged("CustoDeProducao");

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
                    var value2 = Convert.ToInt32(value);

                    model.DefineUnidades(value2);

                    OnPropertyChanged("CustoPorUnidade");

                    collection.modelo.OnPropertyChanged("CustoDeProducao");

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
            get { return model.CustoPorUnidade; }
        }

        public RecursoViewModel()
        {

        }

        public static RecursoViewModel From(Recurso recurso)
        {
            var viewModel = new RecursoViewModel
            {
                model = recurso,
                tipo = recurso.Tipo,
                descricao = recurso.Descricao,
                custo = recurso.Custo.ToString(),
                unidades = recurso.Unidades.ToString(),
            };

            return viewModel;
        }
    }

    public class ModelosObservableCollection : ExtendedObservableCollection<ModeloViewModel>
    {
        private readonly ModelosLocalService planosComerciaisLocalService;

        //private readonly IConsultaDeModelos consultaDeModelos;

        //private readonly IPlanejamentoComercial planejamentoComercial;

        public ModelosObservableCollection()
            : base()
        {

        }

        public ModelosObservableCollection(
            ModelosLocalService planosComerciaisLocalService,
            //IConsultaDeModelos consultaDeModelos,
            //IPlanejamentoComercial planejamentoComercial,
            IList<ModeloViewModel> list
        )
            : base(list)
        {
            this.planosComerciaisLocalService = planosComerciaisLocalService;

            //this.consultaDeModelos = consultaDeModelos;

            //this.planejamentoComercial = planejamentoComercial;
        }

        public ModelosObservableCollection(IList<ModeloViewModel> list)
            : base(list)
        {

        }

        //protected override object AddNewCore()
        //{
        //    var model = new Modelo(
        //        Guid.NewGuid().ToString(),
        //        null,
        //        6000,
        //        20
        //    );

        //    var viewModel = ModeloViewModel.From(model);

        //    OnAddNew(viewModel);

        //    return viewModel;
        //}

        protected override async void OnAddNew(ModeloViewModel viewModel)
        {
            //item.BindingList = this;

            var model = new Modelo(
                            Guid.NewGuid().ToString(),
                            "Modelo #"
                        );

            viewModel.model = model;

            await planosComerciaisLocalService.Add(model);

            //viewModel.Itens.planoComercial = viewModel;

            base.OnAddNew(viewModel);
        }

        protected override async void OnRemoveItem(ModeloViewModel viewModel)
        {
            await planosComerciaisLocalService.Remove(viewModel.model);

            base.OnRemoveItem(viewModel);
        }

        public override async Task SaveChanges()
        {
            try
            {
                await planosComerciaisLocalService.SaveChanges();

                SetStatus($"Modelo salvo com sucesso.");
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

            //        SetStatus($"Modelo '{modifiedItem.Id}' atualizado com sucesso.");
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

            //        SetStatus($"Modelo '{deletedItem.Id}' excluído com sucesso.");
            //    }
            //    catch (Exception ex)
            //    {
            //        SetStatus(ex.Message);
            //    }
            //}
        }
    }

    public class RecursosObservableCollection : ExtendedObservableCollection<RecursoViewModel>
    {
        protected internal ModeloViewModel modelo;

        public RecursosObservableCollection(IList<RecursoViewModel> list)
            : base(list)
        {
            foreach (var item in list)
            {
                item.collection = this;
            }
        }

        protected override void OnAddNew(RecursoViewModel viewModel)
        {
            var model = modelo.model.AdicionaRecurso(TipoDeRecurso.Material, "Custo #", 100, 1);

            viewModel.collection = this;

            viewModel.model = model;

            //viewModel.ModeloId = planoComercial.Id;

            base.OnAddNew(viewModel);
        }

        protected override void OnRemoveItem(RecursoViewModel viewModel)
        {
            modelo.model.RemoveRecurso(viewModel.model);

            base.OnRemoveItem(viewModel);
        }
    }
}
