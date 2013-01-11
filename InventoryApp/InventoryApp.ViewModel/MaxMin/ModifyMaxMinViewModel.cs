using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;
using System.Collections.ObjectModel;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.ViewModel.MaxMin
{
    public class ModifyMaxMinViewModel : ViewModelBase
    {
        #region Fields
        private MaxMinModel _modiMaxMin;
        private RelayCommand _modifyMaxMinCommand;
        private MaxMinViewModel _maxMinViewModel;
        private CatalogAlmacenModel _catalogAlmacenModel;
        #endregion

        //Exponer la propiedad MaxMin almacen y articulo
        #region Props
        public MaxMinModel ModiMaxMin
        {
            get
            {
                return _modiMaxMin;
            }
            set
            {
                _modiMaxMin = value;
            }
        }

        public CatalogAlmacenModel CatalogAlmacenModel
        {
            get
            {
                return _catalogAlmacenModel;
            }
            set
            {
                _catalogAlmacenModel = value;
            }
        }

        public ICommand ModifyMaxMinCommand
        {
            get
            {
                if (_modifyMaxMinCommand == null)
                {
                    _modifyMaxMinCommand = new RelayCommand(p => this.AttempModifyMaxMin(), p => this.CanAttempModifyMaxMin());
                }
                return _modifyMaxMinCommand;
            }
        }
        #endregion

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
        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public ModifyMaxMinViewModel(MaxMinViewModel maxMinViewModel, MaxMinModel selectedMaxMinModel)
        {
            this._modiMaxMin = new MaxMinModel(new MaxMinDataMapper());
            this._maxMinViewModel = maxMinViewModel;
            this._modiMaxMin.UnidMaxMin = selectedMaxMinModel.UnidMaxMin;
            this._modiMaxMin.Max = selectedMaxMinModel.Max;
            this._modiMaxMin.Min = selectedMaxMinModel.Min;
            this._modiMaxMin.Almacen = selectedMaxMinModel.Almacen;
            this._modiMaxMin.Articulo = selectedMaxMinModel.Articulo;
            try
            {

                this._catalogAlmacenModel = new CatalogAlmacenModel(new AlmacenDataMapper());
            }
            catch (ArgumentException ae)
            {
                ;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            this.init();

        }
        #endregion

        #region Methods
        public void init()
        {
            this._Articulos = new ObservableCollection<ArticuloModel>();
            this._Articulos = GetArticulos(this._modiMaxMin);
            
        }

        private ObservableCollection<ArticuloModel> GetArticulos(MaxMinModel maxMinArticulos)
        {
            ObservableCollection<ArticuloModel> articuloModels = new ObservableCollection<ArticuloModel>();
            try
            {
                MaxMinDataMapper artDataMapper = new MaxMinDataMapper();
                List<MAX_MIN> articulos = (List<MAX_MIN>)artDataMapper.getElementArticulos(new MAX_MIN() { UNID_ALMACEN = maxMinArticulos.Almacen.UNID_ALMACEN });

                articulos.ForEach(o => articuloModels.Add(new ArticuloModel()
                {
                    UnidArticulo = o.UNID_ARTICULO
                    ,
                    ArticuloName = o.ARTICULO.ARTICULO1
                    ,
                    EquipoModel = new EquipoModel(new EquipoDataMapper())
                    {
                        UnidEquipo = o.ARTICULO.EQUIPO.UNID_EQUIPO
                        ,
                        EquipoName = o.ARTICULO.EQUIPO.EQUIPO_NAME
                    }
                    ,
                    Categoria = new CATEGORIA()
                    {
                        CATEGORIA_NAME = o.ARTICULO.CATEGORIA.CATEGORIA_NAME
                        ,
                        UNID_CATEGORIA = o.ARTICULO.CATEGORIA.UNID_CATEGORIA
                    }
                    ,
                    Marca = new MARCA()
                    {
                        UNID_MARCA = o.ARTICULO.MARCA.UNID_MARCA
                        ,
                        MARCA_NAME = o.ARTICULO.MARCA.MARCA_NAME
                    },
                    Modelo = new MODELO()
                    {
                        UNID_MODELO = o.ARTICULO.MODELO.UNID_MODELO
                        ,
                        MODELO_NAME = o.ARTICULO.MODELO.MODELO_NAME
                    }
                }));
            }
            catch (Exception)
            {
                ;
            }

            return articuloModels;
        }
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempModifyMaxMin()
        {
            bool _canAddMaxMin = true;
            //if (this.CatalogArticuloModel.Articulos.Count != 0)
                _canAddMaxMin = false;

            return _canAddMaxMin;
        }

        public void AttempModifyMaxMin()
        {
            this._modiMaxMin.updateMaxMin();

            //Puede ser que para pruebas unitarias catalogProyectoViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._maxMinViewModel != null)
            {
                this._maxMinViewModel.loadMaxMin();
            }
        }
        #endregion
    }
}
