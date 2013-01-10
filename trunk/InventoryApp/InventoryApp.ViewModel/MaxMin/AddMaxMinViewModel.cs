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
        #endregion

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

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public AddMaxMinViewModel(MaxMinViewModel maxMinViewModel)
        {
            this._addMaxMin = new MaxMinModel(new MaxMinDataMapper());
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

            //try
            //{

            //    this._catalogArticuloModel = new CatalogArticuloModel(new ArticuloDataMapper());
            //}
            //catch (ArgumentException ae)
            //{
            //    ;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
        public AddMaxMinViewModel()
        { }
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
            if(this.AddArticulos.Count!=0)
                _canAddMaxMin = true;
            return _canAddMaxMin;
        }

        public void AttempAddMaxMin()
        {
            //this._addMaxMin=this.AddArticulos;
            
            foreach (var item in this.AddArticulos)
            {
                this._addMaxMin.Articulo = item.Articulo;
                //item.Articulo = this._addMaxMin.Articulo;
                //item.Categoria = this._addMaxMin.Categoria;
                //item.EquipoModel = this._addMaxMin.EquipoModel;
                //item.Marca = this._addMaxMin.Marca;
                //item.Modelo = this._addMaxMin.Modelo;
                this._addMaxMin.saveMaxMin();
            }
            

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
