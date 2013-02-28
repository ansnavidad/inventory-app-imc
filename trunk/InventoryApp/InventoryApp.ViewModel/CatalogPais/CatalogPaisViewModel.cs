using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;
using System.Windows.Input;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.ViewModel.CatalogPais
{
    public class CatalogPaisViewModel : IPageViewModel
    {
        private RelayCommand _deletePaisCommand;

        private CatalogPaisModel _catalogPaisModel;
        public USUARIO ActualUser;

        public ICommand DeletePaisCommand
        {
            get
            {
                if (_deletePaisCommand == null)
                {
                    _deletePaisCommand = new RelayCommand(p => this.AttempDeletePais(), p => this.CanAttempDeletePais());
                }
                return _deletePaisCommand;
            }
        }

        public CatalogPaisViewModel()
        {
            try
            {
                IDataMapper dataMapper = new CiudadDataMapper();
                this._catalogPaisModel = new CatalogPaisModel(dataMapper);
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

        public CatalogPaisViewModel(USUARIO u)
        {
            try
            {
                IDataMapper dataMapper = new CiudadDataMapper();
                this._catalogPaisModel = new CatalogPaisModel(dataMapper);
                this.ActualUser = u;
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

        public CatalogPaisModel CatalogPaisModel
        {
            get
            {
                return _catalogPaisModel;
            }
            set
            {
                _catalogPaisModel = value;
            }
        }

        public void loadItems()
        {
            this._catalogPaisModel.loadItems();
        }

        /// <summary>
        /// Crea una nueva instancia de addItemStatus y se pasa asi mismo como parámetro
        /// Referenca pag 232 del libro MVVM
        /// </summary>
        /// <returns></returns>
        public AddPaisViewModel CreateAddPaisViewModel()
        {
            return new AddPaisViewModel(this);
        }

        /// <summary>
        /// Crea una nueva instancia de ModifyItemStatus y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyPaisViewModel CreateModifyPaisViewModel()
        {
            PaisModel paisModel = new PaisModel(new PaisDataMapper());
            if (this._catalogPaisModel != null && this._catalogPaisModel.SelectedPais != null)
            {
                paisModel.UnidPais = this._catalogPaisModel.SelectedPais.UNID_PAIS;
                paisModel.Pais = this._catalogPaisModel.SelectedPais.PAIS;
                paisModel.Iso = this._catalogPaisModel.SelectedPais.ISO;
            }
            return new ModifyPaisViewModel(this, paisModel);
        }

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempDeletePais()
        {
            bool _canDeletePais = false;
            foreach (DeletePais d in this._catalogPaisModel.Pais)
            {
                if (d.IsChecked == true)
                {
                    _canDeletePais = true;
                }
            }

            return _canDeletePais;
        }

        public void AttempDeletePais()
        {
            this._catalogPaisModel.deletePais();

            if (this._catalogPaisModel != null)
            {
                this._catalogPaisModel.loadItems();
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
