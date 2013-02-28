using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.ViewModel.CatalogInfraestructura
{
    public class CatalogInfraestructuraViewModel : IPageViewModel
    {
        private RelayCommand _deleteInfraestructuraCommand;
        private CatalogInfraestructuraModel _catalogInfraestructuraModel;
        public USUARIO ActualUser;

        public ICommand DeleteInfraestructuraCommand
        {
            get
            {
                if (_deleteInfraestructuraCommand == null)
                {
                    _deleteInfraestructuraCommand = new RelayCommand(p => this.AttempDeleteInfraestructura(), p => this.CanAttempDeleteInfraestructura());
                }
                return _deleteInfraestructuraCommand;
            }
        }

        public CatalogInfraestructuraViewModel()
        {
            try
            {
                IDataMapper dataMapper = new InfraestructuraDataMapper();
                this._catalogInfraestructuraModel = new CatalogInfraestructuraModel(dataMapper);
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

        public CatalogInfraestructuraViewModel(USUARIO u)
        {
            try
            {
                IDataMapper dataMapper = new InfraestructuraDataMapper();
                this._catalogInfraestructuraModel = new CatalogInfraestructuraModel(dataMapper);
                this.ActualUser = u;
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

        public CatalogInfraestructuraModel CatalogInfraestructuraModel
        {
            get
            {
                return _catalogInfraestructuraModel;
            }
            set
            {
                _catalogInfraestructuraModel = value;
            }
        }

        public void loadItems()
        {
            this._catalogInfraestructuraModel.loadItems();
        }

        public InsertInfraestructuraViewModel CreateInsertInfraestructuraViewModel()
        {
            return new InsertInfraestructuraViewModel(this);
        }

        public ModifyInfraestructuraViewModel CreateModifyInfraestructuraViewModel()
        {
            InfraestructuraModel infraestructuraModel = new InfraestructuraModel(new InfraestructuraDataMapper());
            if (this._catalogInfraestructuraModel != null && this._catalogInfraestructuraModel.SelectedInfraestructura != null)
            {
                infraestructuraModel.InfraestructuraName = this._catalogInfraestructuraModel.SelectedInfraestructura.INFRAESTRUCTURA_NAME;
                infraestructuraModel.UnidInfraestructura = this._catalogInfraestructuraModel.SelectedInfraestructura.UNID_INFRAESTRUCTURA;

            }
            return new ModifyInfraestructuraViewModel(this, infraestructuraModel);
        }

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempDeleteInfraestructura()
        {
            bool _canDeleteTipoEmpresa = false;
            foreach (DeleteInfraestructura d in this._catalogInfraestructuraModel.Infraestructuras)
            {
                if (d.IsChecked == true)
                {
                    _canDeleteTipoEmpresa = true;
                }
            }

            return _canDeleteTipoEmpresa;
        }

        public void AttempDeleteInfraestructura()
        {
            this._catalogInfraestructuraModel.deleteInfraestructura();

            if (this._catalogInfraestructuraModel != null)
            {
                this._catalogInfraestructuraModel.loadItems();
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
