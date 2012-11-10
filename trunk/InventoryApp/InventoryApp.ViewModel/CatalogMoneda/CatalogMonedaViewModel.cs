using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;
using System.Windows.Input;

namespace InventoryApp.ViewModel.CatalogMoneda
{
    public class CatalogMonedaViewModel
    {
        private RelayCommand _deleteMonedaCommand;

        private CatalogMonedaModel _catalogMonedaModel;

        public ICommand DeleteMonedaCommand
        {
            get
            {
                if (_deleteMonedaCommand == null)
                {
                    _deleteMonedaCommand = new RelayCommand(p => this.AttempDeleteMoneda(), p => this.CanAttempDeleteMoneda());
                }
                return _deleteMonedaCommand;
            }
        }

        public CatalogMonedaViewModel()
        {
            
            try
            {
                IDataMapper dataMapper = new MonedaDataMapper();
                this._catalogMonedaModel = new CatalogMonedaModel(dataMapper);   
            }
            catch (ArgumentException a)
            {

                ;
            }
            catch(Exception ex)
            {
                throw ex;
            }   
            
        }

        public CatalogMonedaModel CatalogMonedaModel
        {
            get
            {
                return _catalogMonedaModel;
            }
            set
            {
                _catalogMonedaModel = value;
            }
        }

        public void loadMonedas()
        {
            this._catalogMonedaModel.loadMonedas();
        }

        /// <summary>
        /// Crea una nueva instancia de addItemStatus y se pasa asi mismo como parámetro
        /// Referenca pag 232 del libro MVVM
        /// </summary>
        /// <returns></returns>
        public AddMonedaViewModel CreateAddMonedaViewModel()
        {
            return new AddMonedaViewModel(this);
        }

        /// <summary>
        /// Crea una nueva instancia de ModifyItemStatus y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyMonedaViewModel CreateModifyMonedaViewModel()
        {
            MonedaModel monedaModel = new MonedaModel(new MonedaDataMapper());
            if (this._catalogMonedaModel != null && this._catalogMonedaModel.SelectedMoneda != null)
            {
                monedaModel.UnidMoneda = this._catalogMonedaModel.SelectedMoneda.UNID_MONEDA;
                monedaModel.MonedaName = this._catalogMonedaModel.SelectedMoneda.MONEDA_NAME;
                monedaModel.MonedaAbr = this._catalogMonedaModel.SelectedMoneda.MONEDA_ABR;

            }
            return new ModifyMonedaViewModel(this,monedaModel);
        }

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempDeleteMoneda()
        {
            bool _canDeleteMoneda = false;
            foreach (DeleteMoneda d in this._catalogMonedaModel.Moneda)
            {
                if (d.IsChecked == true)
                {
                    _canDeleteMoneda = true;
                }
            }

            return _canDeleteMoneda;
        }

        public void AttempDeleteMoneda()
        {
            this._catalogMonedaModel.deleteMoneda();

            if (this._catalogMonedaModel != null)
            {
                this._catalogMonedaModel.loadMonedas();
            }
        }
        #endregion
    }
}
