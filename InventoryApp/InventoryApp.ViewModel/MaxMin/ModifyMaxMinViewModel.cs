using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;
using System.Collections.ObjectModel;
using InventoryApp.DAL.POCOS;
using InventoryApp.ViewModel.Historial;

namespace InventoryApp.ViewModel.MaxMin
{
    public class ModifyMaxMinViewModel : ViewModelBase
    {
        #region Fields
        private MaxMinModel _modiMaxMin;
        private RelayCommand _modifyMaxMinCommand;
        private RelayCommand _deleteArticuloCommand;
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

        public ICommand DeleteArticuloCommand
        {
            get
            {
                if (_deleteArticuloCommand == null)
                {
                    _deleteArticuloCommand = new RelayCommand(p => this.AttemptDeleteArticuloCommand(), p => this.CanAttemptDeleteArticuloCommand());
                }
                return _deleteArticuloCommand;
            }
        }
        #endregion

        public ObservableCollection<MaxMinModel> ModiArticulos
        {
            get { return _ModiArticulos; }
            set
            {
                if (_ModiArticulos != value)
                {
                    _ModiArticulos = value;
                    OnPropertyChanged(ArticulosPropertyName);
                }
            }
        }
        private ObservableCollection<MaxMinModel> _ModiArticulos;
        public const string ArticulosPropertyName = "ModiArticulos";

        public ObservableCollection<MaxMinModel> DeleteArticulos
        {
            get { return _DeleteArticulos; }
            set
            {
                if (_DeleteArticulos != value)
                {
                    _DeleteArticulos = value;
                    OnPropertyChanged(DeleteArticulosPropertyName);
                }
            }
        }
        private ObservableCollection<MaxMinModel> _DeleteArticulos;
        public const string DeleteArticulosPropertyName = "DeleteArticulos";

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public ModifyMaxMinViewModel(MaxMinViewModel maxMinViewModel, MaxMinModel selectedMaxMinModel)
        {
            this._modiMaxMin = new MaxMinModel(new MaxMinDataMapper(), maxMinViewModel.ActualUser);
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
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            this.init();

        }
        public ModifyMaxMinViewModel(){}
        #endregion

        #region Methods

        public HistorialViewModel CreateHistorialViewModel()
        {
            HistorialViewModel historialViewModel = new HistorialViewModel(this.ModiMaxMin);
            return historialViewModel;
        }

        public void init()
        {
            this._ModiArticulos = new ObservableCollection<MaxMinModel>();
            this._DeleteArticulos = new ObservableCollection<MaxMinModel>();
            this._ModiArticulos = GetArticulos(this._modiMaxMin);    
        }

        private ObservableCollection<MaxMinModel> GetArticulos(MaxMinModel maxMinArticulos)
        {
            ObservableCollection<MaxMinModel> articuloModels = new ObservableCollection<MaxMinModel>();
            try
            {
                MaxMinDataMapper artDataMapper = new MaxMinDataMapper();
                List<MAX_MIN> articulos = (List<MAX_MIN>)artDataMapper.getElementArticulos(new MAX_MIN() { UNID_ALMACEN = maxMinArticulos.Almacen.UNID_ALMACEN });

                articulos.ForEach(o => articuloModels.Add(new MaxMinModel()
                {
                     Articulo=o.ARTICULO,

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
                    },
                    Max= o.MAX,
                    Min=o.MIN,
                    UnidMaxMin=o.UNID_MAX_MIN
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
            bool _canAddMaxMin = false;
            if (this.ModiArticulos.Count != 0)
            {
                foreach (var item in this.ModiArticulos)
                {
                    if (item.Max >= 0 && item.Min >= 0 && item.Max >= item.Min)
                    {
                        _canAddMaxMin = true;
                    }
                    else
                    {
                        _canAddMaxMin = false;
                        break;
                    }
                }
            }
            return _canAddMaxMin;
        }

        public void AttempModifyMaxMin()
        {
            //valida si hay un elemento a eliminar
            if (this.DeleteArticulos.Count!=0)
            {
                foreach (var item in this.DeleteArticulos)
                {
                    if (item.UnidMaxMin!=0)
                    {
                        //eliminacion logiga
                        this._modiMaxMin.DeleteMaxMin();    
                    }
                }
            }
            //actualiza si hubo cambios
            foreach (var item in this.ModiArticulos)
            {
                //si el articulo existe actualiza si no inserta
                this._modiMaxMin.UnidMaxMin = item.UnidMaxMin;
                this._modiMaxMin.Articulo = item.Articulo;
                this._modiMaxMin.Max = item.Max;
                this._modiMaxMin.Min = item.Min;
                if (item.UnidMaxMin!=0)
                {
                    this._modiMaxMin.updateMaxMin();    
                }
                else
                {
                    this._modiMaxMin.saveMaxMin();
                }
                
            }
          
            //Puede ser que para pruebas unitarias catalogProyectoViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._maxMinViewModel != null)
            {
                this._maxMinViewModel.loadMaxMin();
            }
        }

        public void AttemptDeleteArticuloCommand()
        {
            try
            {
                //agrega a un aux los elementos que van a ser eliminados
                var query= (from o in this.ModiArticulos
                where o.IsChecked == true
                select o).ToList();
                //valida que no venga vacia la el aux
                if (query.Count!=0)
                {
                    foreach (var item in query)
                    {
                        this.DeleteArticulos.Add(item);
                    }    
                }
                
                (from o in this.ModiArticulos
                 where o.IsChecked == true
                 select o).ToList().ForEach(o => this.ModiArticulos.Remove(o));
            }
            catch (Exception)
            {
                ;
            }
        }

        public bool CanAttemptDeleteArticuloCommand()
        {
            bool canDeleteArticulo = false;

            if (this.ModiArticulos != null && this.ModiArticulos.Count > 0)
            {
                int res = 0;
                try
                {
                    res = (from o in this.ModiArticulos
                           where o.IsChecked == true
                           select o).ToList().Count;
                }
                catch (Exception)
                {
                    res = 0;
                }

                if (res > 0)
                {
                    canDeleteArticulo = true;
                }
            }

            return canDeleteArticulo;
        }
        #endregion
    }
}
