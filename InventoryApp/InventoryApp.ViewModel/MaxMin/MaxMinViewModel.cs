using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.MaxMin
{
    public class MaxMinViewModel : IPageViewModel
    {
        private RelayCommand _deleteMaxMinCommand;
        private CatalogMaxMinModel _catalogMaxMinModel;

        private MaxMinModel _addGridArticulos = new MaxMinModel();

        public ICommand DeleteMaxMinCommand
        {
            get
            {
                if (_deleteMaxMinCommand == null)
                {
                    _deleteMaxMinCommand = new RelayCommand(p => this.AttempDeleteMaxMin(), p => this.CanAttempDeleteMaxMin());
                }
                return _deleteMaxMinCommand;
            }
        }

        public MaxMinViewModel()
        {
            try
            {
                IDataMapper dataMapper = new MaxMinDataMapper();
                this._catalogMaxMinModel = new CatalogMaxMinModel(dataMapper);
            }
            catch (ArgumentException a)
            {

                ;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
            
        }

        public CatalogMaxMinModel CatalogMaxMinModel
        {
            get
            {
                return _catalogMaxMinModel;
            }
            set
            {
                _catalogMaxMinModel = value;
            }
        }

        public void loadMaxMin()
        {
            this._catalogMaxMinModel.loadMaxMin();
        }

        /// <summary>
        /// Crea una nueva instancia de de AddMaxMinViewModel y se pasa asi mismo como parámetro
        /// </summary>
        /// <returns></returns>
        public AddMaxMinViewModel CreateAddMaxMinViewModel()
        {
            return new AddMaxMinViewModel(this);
        }

        /// <summary>
        /// Crea una nueva instancia de ModifyMaxMinViewModel y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyMaxMinViewModel CreateModifyMaxMinViewModel()
        {
            MaxMinModel maxMinModel = new MaxMinModel(new MaxMinDataMapper());
            if (this._catalogMaxMinModel != null && this._catalogMaxMinModel.SelectedMaxMin != null)
            {
                maxMinModel.UnidMaxMin = this._catalogMaxMinModel.SelectedMaxMin.UNID_MAX_MIN;
                maxMinModel.Max = this._catalogMaxMinModel.SelectedMaxMin.MAX;
                maxMinModel.Min = this._catalogMaxMinModel.SelectedMaxMin.MIN;
                maxMinModel.Articulo = this._catalogMaxMinModel.SelectedMaxMin.Articulo;
                maxMinModel.Almacen = this._catalogMaxMinModel.SelectedMaxMin.Almacen;
            }
            return new ModifyMaxMinViewModel(this, maxMinModel);
        }

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempDeleteMaxMin()
        {
            bool _canDeleteMaxMin = false;
            foreach (DeleteMaxMin d in this._catalogMaxMinModel.MaxMin)
            {
                if (d.IsChecked == true)
                {
                    _canDeleteMaxMin = true;
                }
            }

            return _canDeleteMaxMin;
        }

        public void AttempDeleteMaxMin()
        {
            this._catalogMaxMinModel.deleteMaxMin();

            if (this._catalogMaxMinModel != null)
            {
                this._catalogMaxMinModel.loadMaxMin();
            }
        }
        #endregion

        public string PageName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
