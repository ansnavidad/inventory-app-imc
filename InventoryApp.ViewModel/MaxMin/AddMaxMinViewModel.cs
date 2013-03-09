using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.ViewModel.CatalogArticulo;
using System.Windows.Input;
using InventoryApp.DAL;
using System.Collections.ObjectModel;

namespace InventoryApp.ViewModel.MaxMin
{
    public class AddMaxMinViewModel:ViewModelBase
    {
        #region Fields
        private MaxMinModel _addMaxMin;
        private RelayCommand _addMaxMinCommand;
        private RelayCommand _deleteArticuloCommand;
        private MaxMinViewModel _maxMinViewModel;
        private CatalogAlmacenModel _catalogAlmacenModel;
        #endregion

        //Exponer la propiedad maxMin articulo y almacen
        #region Props
        public MaxMinModel AddMaxMin
        {
            get
            {
                return _addMaxMin;
            }
            set
            {
                _addMaxMin = value;
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

        public ICommand AddMaxMinCommand
        {
            get
            {
                if (_addMaxMinCommand == null)
                {
                    _addMaxMinCommand = new RelayCommand(p => this.AttempAddMaxMin(), p => this.CanAttempAddMaxMin());
                }
                return _addMaxMinCommand;
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

        #region Coleccion en memoria de los articulos 
        public ObservableCollection<MaxMinModel> AddArticulos
        {
            get { return _AddArticulos; }
            set
            {
                if (_AddArticulos != value)
                {
                    _AddArticulos = value;
                    OnPropertyChanged(ArticulosPropertyName);
                }
            }
        }
        private ObservableCollection<MaxMinModel> _AddArticulos = new ObservableCollection<MaxMinModel>();
        public const string ArticulosPropertyName = "AddArticulos";
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public AddMaxMinViewModel(MaxMinViewModel maxMinViewModel)
        {
            this._addMaxMin = new MaxMinModel(new MaxMinDataMapper(), maxMinViewModel.ActualUser);
            this._maxMinViewModel = maxMinViewModel;
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
        }

        public AddMaxMinViewModel(){ }
        #endregion

        #region Methods

        public AddArticulosMaxMin CreateAddArticuloMaxMinViewModel()
        {
            return new AddArticulosMaxMin();
        }

        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempAddMaxMin()
        {
            bool _canAddMaxMin = false;
            if (this.AddArticulos.Count != 0)
            {
                foreach (var item in this.AddArticulos)
                {
                    this._addMaxMin.Max = item.Max;
                    this._addMaxMin.Min = item.Min;

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

                if (!_canAddMaxMin)
                    this._addMaxMin.MensajeError = "Favor de validar que Cantidad Máxima sea mayor o igual a cero. que la cantidad Mínima";
                else
                    this._addMaxMin.MensajeError = "";
                
            }
                
            return _canAddMaxMin;
        }

        public void AttempAddMaxMin()
        {
            foreach (var item in this.AddArticulos)
            {
                this._addMaxMin.Articulo = item.Articulo;
                this._addMaxMin.Max = item.Max;
                this._addMaxMin.Min = item.Min;
                this._addMaxMin.saveMaxMin();
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
                (from o in this.AddArticulos
                 where o.IsChecked == true
                 select o).ToList().ForEach(o => this.AddArticulos.Remove(o));
            }
            catch (Exception)
            {
                ;
            }
        }

        public bool CanAttemptDeleteArticuloCommand()
        {
            bool canDeleteArticulo = false;

            if (this.AddArticulos != null && this.AddArticulos.Count > 0)
            {
                int res = 0;
                try
                {
                    res = (from o in this.AddArticulos
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
