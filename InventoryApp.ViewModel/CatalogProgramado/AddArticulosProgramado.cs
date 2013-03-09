using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using System.Collections.ObjectModel;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.ViewModel.CatalogProgramado
{
    public class AddArticulosProgramado : ViewModelBase
    {
        private RelayCommand _addArticulosCommad;
        private ProgramadoModel _addProgramado;
        private AddProgramadoViewModel _addProgramadoViewModel;
        private ModifyProgramadoViewModel _modiProgramadoViewModel;

        public ProgramadoModel AddProgramado
        {
            get
            {
                return _addProgramado;
            }
            set
            {
                _addProgramado = value;
            }
        }

        public ICommand AddArticulosCommad
        {
            get
            {
                if (_addArticulosCommad == null)
                {
                    _addArticulosCommad = new RelayCommand(p => this.AttemptAddArticulo(), p => this.CanAttemptAddArticulo());
                }
                return _addArticulosCommad;
            }
        }


        //filta por categoria el combo de articulo
        public CategoriaModel SelectedCategoria
        {
            get { return _SelectedCategoria; }
            set
            {
                if (_SelectedCategoria != value)
                {
                    _SelectedCategoria = value;
                    OnPropertyChanged(SelectedCategoriaPropertyName);

                    //Cambiar el combo de articulo
                    this.Articulos = this.GetArticulosByCategoria(_SelectedCategoria);
                }
            }
        }
        private CategoriaModel _SelectedCategoria;
        public const string SelectedCategoriaPropertyName = "SelectedCategoria";

        public ObservableCollection<CategoriaModel> Categorias
        {
            get { return _Categorias; }
            set
            {
                if (_Categorias != value)
                {
                    _Categorias = value;
                    OnPropertyChanged(CategoriasPropertyName);
                }
            }
        }
        private ObservableCollection<CategoriaModel> _Categorias;
        public const string CategoriasPropertyName = "Categorias";

        //Articulos
        public ObservableCollection<ArticuloModel> Articulos
        {
            get { return _Articulos; }
            set
            {
                if (_Articulos != value)
                {
                    _Articulos = value;
                    OnPropertyChanged(ArticulosPropertyName);
                }
            }
        }
        private ObservableCollection<ArticuloModel> _Articulos;
        public const string ArticulosPropertyName = "Articulos";

        public ArticuloModel SelectedArticulo
        {
            get { return _SelectedArticulo; }
            set
            {
                if (_SelectedArticulo != value)
                {
                    _SelectedArticulo = value;
                    OnPropertyChanged(SelectedArticuloPropertyName);
                }
            }
        }
        private ArticuloModel _SelectedArticulo;
        public const string SelectedArticuloPropertyName = "SelectedArticulo";

        //Unidades
        public ObservableCollection<UnidadModel> Unidades
        {
            get { return _Unidades; }
            set
            {
                if (_Unidades != value)
                {
                    _Unidades = value;
                    OnPropertyChanged(UnidadesPropertyName);
                }
            }
        }
        private ObservableCollection<UnidadModel> _Unidades;
        public const string UnidadesPropertyName = "Unidades";

        public UnidadModel SelectedUnidad
        {
            get { return _SelectedUnidad; }
            set
            {
                if (_SelectedUnidad != value)
                {
                    _SelectedUnidad = value;
                    OnPropertyChanged(SelectedUnidadPropertyName);
                }
            }
        }
        private UnidadModel _SelectedUnidad;
        public const string SelectedUnidadPropertyName = "SelectedUnidad";

        #region Constructor

        public AddArticulosProgramado() 
        {
            this.init();
        }

        //sobrecarga para agregar articulos
        public AddArticulosProgramado(AddProgramadoViewModel addProgramadoViewModel)
        {
            this.init();
            this._addProgramadoViewModel = new AddProgramadoViewModel();
            this._addProgramadoViewModel = addProgramadoViewModel;
            
        }

        //sobrecarga para modificar articulos
        public AddArticulosProgramado(ModifyProgramadoViewModel modifyProgramadoViewModel)
        {
            this.init();
            this._modiProgramadoViewModel = new ModifyProgramadoViewModel();
            this._modiProgramadoViewModel = modifyProgramadoViewModel;
        }
        #endregion

        #region Methods
        public void init()
        {
            this._Categorias = this.GetCategorias();
            this._Articulos = new ObservableCollection<ArticuloModel>();
            this._Unidades = this.GetUnidades();
        }

        private ObservableCollection<ArticuloModel> GetArticulosByCategoria(CategoriaModel categoria)
        {
            ObservableCollection<ArticuloModel> articuloModels = new ObservableCollection<ArticuloModel>();
            try
            {
                ArticuloDataMapper artDataMapper = new ArticuloDataMapper();
                List<ARTICULO> articulos = (List<ARTICULO>)artDataMapper.getElementsByCategoria(new CATEGORIA() { UNID_CATEGORIA = categoria.UnidCategoria });

                articulos.ForEach(o => articuloModels.Add(new ArticuloModel()
                {
                    UnidArticulo = o.UNID_ARTICULO
                    ,
                    ArticuloName = o.ARTICULO1
                    ,
                    EquipoModel = new EquipoModel(new EquipoDataMapper())
                    {
                        UnidEquipo = o.EQUIPO.UNID_EQUIPO
                        ,
                        EquipoName = o.EQUIPO.EQUIPO_NAME
                    }
                    ,
                    Categoria = new CATEGORIA()
                    {
                        CATEGORIA_NAME = o.CATEGORIA.CATEGORIA_NAME
                        ,
                        UNID_CATEGORIA = o.CATEGORIA.UNID_CATEGORIA
                    }
                    ,
                    Marca = new MARCA()
                    {
                        UNID_MARCA = o.MARCA.UNID_MARCA
                        ,
                        MARCA_NAME = o.MARCA.MARCA_NAME
                    },
                    Modelo = new MODELO()
                    {
                        UNID_MODELO = o.MODELO.UNID_MODELO
                        ,
                        MODELO_NAME = o.MODELO.MODELO_NAME
                    }
                }));
            }
            catch (Exception)
            {
                ;
            }

            return articuloModels;
        }

        private ObservableCollection<CategoriaModel> GetCategorias()
        {
            ObservableCollection<CategoriaModel> categorias = new ObservableCollection<CategoriaModel>();

            try
            {
                CategoriaDataMapper catDataMapper = new CategoriaDataMapper();
                List<CATEGORIA> categoriaResult = (List<CATEGORIA>)catDataMapper.getElements();
                categoriaResult.ForEach(o => categorias.Add(new CategoriaModel(catDataMapper)
                {
                    UnidCategoria = o.UNID_CATEGORIA,
                    CategoriaName = o.CATEGORIA_NAME
                }));
            }
            catch (Exception)
            {
                //Validar excepción
            }

            return categorias;
        }

        private ObservableCollection<UnidadModel> GetUnidades()
        {
            ObservableCollection<UnidadModel> unidades = new ObservableCollection<UnidadModel>();

            try
            {
                UnidadDataMapper uniDataMapper = new UnidadDataMapper();
                uniDataMapper.getListElements().ForEach(o => unidades.Add(new UnidadModel(uniDataMapper)
                {
                    UnidUnidad = o.UNID_UNIDAD
                    ,
                    UnidadName = o.UNIDAD1
                }));
            }
            catch (Exception)
            {
                throw;
            }

            return unidades;
        }

        public void AttemptAddArticulo()
        {

            ProgramadoModel programadoModel = new ProgramadoModel()
            {
                Categoria = this.SelectedArticulo.Categoria,
                EquipoModel = this.SelectedArticulo.EquipoModel,
                Marca = this.SelectedArticulo.Marca,
                Modelo = this.SelectedArticulo.Modelo,
                Articulo = new ARTICULO()
                {
                    ARTICULO1 = this.SelectedArticulo.ArticuloName,
                    CATEGORIA = this.SelectedArticulo.Categoria,
                    //EQUIPO=this.SelectedArticulo.EquipoModel,
                    MARCA = this.SelectedArticulo.Marca,
                    MODELO = this.SelectedArticulo.Modelo,
                    UNID_ARTICULO = this.SelectedArticulo.UnidArticulo,
                    UNID_CATEGORIA = this.SelectedArticulo.Categoria.UNID_CATEGORIA,
                    UNID_EQUIPO = this.SelectedArticulo.EquipoModel.UnidEquipo,
                    UNID_MARCA = this.SelectedArticulo.Marca.UNID_MARCA,
                    UNID_MODELO = this.SelectedArticulo.Modelo.UNID_MODELO
                },
                 Programado=0,
                IsChecked = false
            };
            if (this._addProgramadoViewModel != null)
            {
                this._addProgramadoViewModel.AddArticulos.Add(programadoModel);
            }
            if (_modiProgramadoViewModel != null)
            {
                this._modiProgramadoViewModel.ModiArticulos.Add(programadoModel);
            }
        }

        public bool CanAttemptAddArticulo()
        {
            bool canAddDetalle = false;

            if (this.SelectedCategoria != null && this.SelectedArticulo != null && this.SelectedUnidad != null)
            {
                canAddDetalle = true;
            }

            return canAddDetalle;
        }
        #endregion

    }
}
