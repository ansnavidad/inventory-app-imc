using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;
using System.Windows.Input;

namespace InventoryApp.ViewModel.CatalogMedioEnvio
{
    public class CatalogMedioEnvioViewModel : IPageViewModel
    {
        private RelayCommand _deleteMedioEnvioCommand;

        private CatalogMedioEnvioModel _medioEnvioModel;

        public ICommand DeleteMedioEnvioCommand
        {
            get
            {
                if (_deleteMedioEnvioCommand == null)
                {
                    _deleteMedioEnvioCommand = new RelayCommand(p => this.AttempDeleteMedioEnvio(), p => this.CanAttempDeleteMedioEnvio());
                }
                return _deleteMedioEnvioCommand;
            }
        }

        public CatalogMedioEnvioViewModel()
        {
            
            try
            {
                IDataMapper dataMapper = new MedioEnvioDataMapper();
                this._medioEnvioModel = new CatalogMedioEnvioModel(dataMapper);   
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

        public CatalogMedioEnvioModel MedioEnvioModel
        {
            get
            {
                return _medioEnvioModel;
            }
            set
            {
                _medioEnvioModel = value;
            }
        }

        public void loadItems()
        {
            this._medioEnvioModel.loadItems();
        }

        /// <summary>
        /// Crea una nueva instancia de addItemStatus y se pasa asi mismo como parámetro
        /// Referenca pag 232 del libro MVVM
        /// </summary>
        /// <returns></returns>
        public AddMedioEnvioViewModel CreateAddMedioEnvioViewModel()
        {
            return new AddMedioEnvioViewModel(this);
        }

        /// <summary>
        /// Crea una nueva instancia de ModifyItemStatus y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyMedioEnvioViewModel CreateModifyMedioEnvioViewModel()
        {
            MedioEnvioModel medioEnvioModel = new MedioEnvioModel(new MedioEnvioDataMapper());
            if (this._medioEnvioModel != null && this._medioEnvioModel.SelectedMedioEnvio != null)
            {
                medioEnvioModel.MedioEnvioName = this._medioEnvioModel.SelectedMedioEnvio.MEDIO_ENVIO_NAME;
                medioEnvioModel.UnidMedioEnvio = this._medioEnvioModel.SelectedMedioEnvio.UNID_MEDIO_ENVIO;
            }
            return new ModifyMedioEnvioViewModel(this, medioEnvioModel);
        }

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempDeleteMedioEnvio()
        {
            bool _canDeleteMedioEnvio = false;
            foreach (DeleteMedioEnvio d in this._medioEnvioModel.MedioEnvio)
            {
                if (d.IsChecked == true)
                {
                    _canDeleteMedioEnvio = true;
                }
            }

            return _canDeleteMedioEnvio;
        }

        public void AttempDeleteMedioEnvio()
        {
            this._medioEnvioModel.deleteMedioEnvio();

            if (this._medioEnvioModel != null)
            {
                this._medioEnvioModel.loadItems();
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
