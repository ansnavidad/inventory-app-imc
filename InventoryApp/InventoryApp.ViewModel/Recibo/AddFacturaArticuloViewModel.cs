﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.Model.Recibo;
using InventoryApp.DAL;
using System.Collections.ObjectModel;
using InventoryApp.DAL.POCOS;
using System.Windows.Input;
using InventoryApp.ViewModel.CatalogItem;

namespace InventoryApp.ViewModel.Recibo
{
    public class AddFacturaArticuloViewModel : ViewModelBase, IFacturaArticuloViewModel
    {
        private AgregarItemViewModel VM;
        private IFacturaViewModel _AddFacturaViewModel;
        private RelayCommand _AddDetalle;

        public ICommand AddMonedaCommand
        {
            get
            {
                if (_AddDetalle == null)
                {
                    _AddDetalle = new RelayCommand(p => this.AttemptAddDetalle(), p => this.CanAttemptAddDetalle());
                }
                return _AddDetalle;
            }
        }

        public ProveedorModel Proveedor
        {
            get { return _Proveedor; }
            set
            {
                if (_Proveedor != value)
                {
                    _Proveedor = value;
                    OnPropertyChanged(ProveedorPropertyName);
                }
            }
        }
        private ProveedorModel _Proveedor;
        public const string ProveedorPropertyName = "Proveedor";

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

                    if (this._Articulos != null && this._Articulos.Count > 1)
                        this.SelectedArticulo = this._Articulos[1];
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

        public int Cantidad
        {
            get { return _Cantidad; }
            set
            {
                if (_Cantidad != value)
                {
                    _Cantidad = value;
                    OnPropertyChanged(CantidadPropertyName);
                    OnPropertyChanged(PrecioFinalPropertyName);
                }
            }
        }
        private int _Cantidad;
        public const string CantidadPropertyName = "Cantidad";

        public double PrecioUnitario
        {
            get { return _PrecioUnitario; }
            set
            {
                if (_PrecioUnitario != value)
                {
                    _PrecioUnitario = value;
                    OnPropertyChanged(PrecioUnitarioPropertyName);
                }
            }
        }
        private double _PrecioUnitario;
        public const string PrecioUnitarioPropertyName = "PrecioUnitario";

        public double ImpuestoUnitario
        {
            get {
                if (_AddFacturaViewModel != null)
                {
                    double aux = this._AddFacturaViewModel.PorIva;
                    return aux;
                }
                else {
                    return 0;
                }
            }
        }
        public const string ImpuestoUnitarioPropertyName = "ImpuestoUnitario";

        public double CostoUnitario
        {
            get { return _CostoUnitario; }
            set
            {
                if (_CostoUnitario != value)
                {
                    _CostoUnitario = value;
                    OnPropertyChanged(CostoUnitarioPropertyName);
                    OnPropertyChanged(PrecioFinalPropertyName);
                }
            }
        }
        private double _CostoUnitario;
        public const string CostoUnitarioPropertyName = "CostoUnitario";
        

        public double PrecioFinal
        {
            get 
            {   double aux = this.Cantidad* (this.CostoUnitario * (((this.ImpuestoUnitario/100))+1));
                //aux *= 100;
                //int aux2 = (int)aux;
                //aux = aux2;
                //aux /= 100;
                return aux;
            }
        }
        public const string PrecioFinalPropertyName = "PrecioFinal";

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

        public FacturaCompraDetalleModel FacturaDetalle
        {
            get { return _FacturaDetalle; }
            set
            {
                if (_FacturaDetalle != value)
                {
                    _FacturaDetalle = value;
                    OnPropertyChanged(FacturaDetallePropertyName);
                }
            }
        }
        private FacturaCompraDetalleModel _FacturaDetalle;
        public const string FacturaDetallePropertyName = "FacturaDetalle";


        #region Constructor
        public AddFacturaArticuloViewModel()
            : this(null)
        {
        }

        public AddFacturaArticuloViewModel(IFacturaViewModel factura)
        {
            if (factura != null && factura.SelectedProveedor != null)
            {
                this._Proveedor = factura.SelectedProveedor;
            }
            this._AddFacturaViewModel = factura;
            this.init(factura);
        }

        public AddFacturaArticuloViewModel(AgregarItemViewModel vm, bool b)
        {
            this.VM = vm;

            ProveedorModel p = new ProveedorModel(null);
            p.UnidProveedor = VM.ItemModel.Proveedor.UNID_PROVEEDOR;
            
            this._Proveedor = p;
            this.init();
        }

        #endregion

        #region Methods
        public void init(IFacturaViewModel factura)
        {
            this._AddFacturaViewModel = factura;
            this._Categorias = this.GetCategoriasByProveedor();
            this._Articulos = new ObservableCollection<ArticuloModel>();
            this._Unidades = this.GetUnidades();
            this._FacturaDetalle = new FacturaCompraDetalleModel();

            if (this._Categorias != null && this._Categorias.Count > 1)
                this.SelectedCategoria = this._Categorias[1];
            if (this._Articulos != null && this._Articulos.Count > 1)
                this.SelectedArticulo = this._Articulos[1];
            if (this._Unidades != null && this._Unidades.Count > 1)
                this.SelectedUnidad = this._Unidades[1];
        }

        public void init()
        {   
            this._Categorias = this.GetCategoriasByProveedor2();
            this._Articulos = new ObservableCollection<ArticuloModel>();
            this._Unidades = this.GetUnidades();
            this._FacturaDetalle = new FacturaCompraDetalleModel();

            if (this._Categorias != null && this._Categorias.Count > 1)
                this.SelectedCategoria = this._Categorias[1];
            if (this._Articulos != null && this._Articulos.Count > 1)
                this.SelectedArticulo = this._Articulos[1];
            if (this._Unidades != null && this._Unidades.Count > 1)
                this.SelectedUnidad = this._Unidades[1];
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
                        UNID_MODELO=o.MODELO.UNID_MODELO
                        ,MODELO_NAME=o.MODELO.MODELO_NAME
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

        private ObservableCollection<CategoriaModel> GetCategoriasByProveedor()
        {
            ObservableCollection<CategoriaModel> categorias = new ObservableCollection<CategoriaModel>();

            try
            {
                CategoriaDataMapper catDataMapper = new CategoriaDataMapper();
                List<CATEGORIA> categoriaResult = catDataMapper.getElementsByProveedor(new PROVEEDOR()
                {
                    UNID_PROVEEDOR = this._AddFacturaViewModel.SelectedProveedor.UnidProveedor
                });
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
        
        private ObservableCollection<CategoriaModel> GetCategoriasByProveedor2()
        {
            ObservableCollection<CategoriaModel> categorias = new ObservableCollection<CategoriaModel>();

            try
            {
                CategoriaDataMapper catDataMapper = new CategoriaDataMapper();
                List<CATEGORIA> categoriaResult = catDataMapper.getElementsByProveedor(new PROVEEDOR()
                {
                    UNID_PROVEEDOR = VM.ItemModel.Proveedor.UNID_PROVEEDOR
                });
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
                ;
            }

            return unidades;
        }

        public void AttemptAddDetalle()
        {
            if (VM == null)
            {
                FacturaCompraDetalleModel facturaDetalle = new FacturaCompraDetalleModel()
                {
                    UnidFacturaCompraDetalle = DAL.UNID.getNewUNID()
                    ,
                    Articulo = this._SelectedArticulo
                    ,
                    Cantidad = this._Cantidad
                    ,
                    CostoUnitario = this.CostoUnitario
                    ,
                    Unidad = this._SelectedUnidad
                    ,
                    ImpuestoUnitario = this._AddFacturaViewModel.PorIva,
                    Factura = new FacturaCompraModel()
                    {
                        UnidFactura = this._AddFacturaViewModel.UnidFactura,
                        NumeroFactura = this._AddFacturaViewModel.NumeroFactura,
                        NumeroPedimento = this._AddFacturaViewModel.NumeroPedimento,
                        PorIva = this._AddFacturaViewModel.PorIva,
                        Proveedor = this._AddFacturaViewModel.SelectedProveedor
                    }
                };
                this._AddFacturaViewModel.FacturaDetalles.Add(facturaDetalle);
            }
            else {

                VM.ItemModel.Articulo = new ARTICULO()
                {
                    ARTICULO1 = this._SelectedArticulo.ArticuloName,
                    UNID_ARTICULO = this._SelectedArticulo.UnidArticulo
                };
                VM.ItemModel.Categoria = new CATEGORIA()
                {
                    CATEGORIA_NAME = this.SelectedArticulo.Categoria.CATEGORIA_NAME,
                    UNID_CATEGORIA = this.SelectedArticulo.Categoria.UNID_CATEGORIA
                };
                VM.ItemModel.Equipo = new EQUIPO()
                {
                    EQUIPO_NAME = this.SelectedArticulo.EquipoModel.EquipoName,
                    UNID_EQUIPO = this.SelectedArticulo.EquipoModel.UnidEquipo
                };
                VM.ItemModel.CostoUnitario = this.CostoUnitario;
                VM.ItemModel.CantidadItem = this._Cantidad;
                VM.ItemModel.UnidUnidad = this._SelectedUnidad.UnidUnidad;

                VM.ItemModelCollection = new ObservableCollection<AgregarItemModel>();
                VM.ItemModelCollection.Add(VM.ItemModel);
                VM.FillWithItemDetallesAnterior = false;
                VM.FillWithItemDetalles = true;
            }
        }

        public bool CanAttemptAddDetalle()
        {
            bool canAddDetalle = false;

            if (this.SelectedCategoria != null && this.SelectedArticulo != null && this._Cantidad > 0 && this.SelectedUnidad!=null && this._CostoUnitario > 0
                )
            {
                canAddDetalle = true;
            }

            return canAddDetalle;
        }
        #endregion
    }
}
