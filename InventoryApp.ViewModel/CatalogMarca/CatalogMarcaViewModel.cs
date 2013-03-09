using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;
using System.Windows.Input;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.ViewModel.CatalogMarca
{
    public class CatalogMarcaViewModel : IPageViewModel
    {
        private RelayCommand _deleteMarcaCommand;

        private CatalogMarcaModel _catalogMarcaModel;
        public USUARIO ActualUser;

        public ICommand DeleteMarcaCommand
        {
            get
            {
                if (_deleteMarcaCommand == null)
                {
                    _deleteMarcaCommand = new RelayCommand(p => this.AttempDeleteMarca(), p => this.CanAttempDeleteMarca());
                }
                return _deleteMarcaCommand;
            }
        }

        public CatalogMarcaViewModel()
        {
            try
            {
                IDataMapper dataMapper = new MarcaDataMapper();
                this._catalogMarcaModel = new CatalogMarcaModel(dataMapper);
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

        public CatalogMarcaViewModel(USUARIO u)
        {
            try
            {
                IDataMapper dataMapper = new MarcaDataMapper();
                this._catalogMarcaModel = new CatalogMarcaModel(dataMapper);
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

        public CatalogMarcaModel CatalogMarcaModel
        {
            get
            {
                return _catalogMarcaModel;
            }
            set
            {
                _catalogMarcaModel = value;
            }
        }

        public void loadItems()
        {
            this._catalogMarcaModel.loadItems();
        }

        /// <summary>
        /// Crea una nueva instancia de addItemStatus y se pasa asi mismo como parámetro
        /// Referenca pag 232 del libro MVVM
        /// </summary>
        /// <returns></returns>
        public AddMarcaViewModel CreateAddMarcaViewModel()
        {
            return new AddMarcaViewModel(this);
        }

        /// <summary>
        /// Crea una nueva instancia de ModifyItemStatus y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyMarcaViewModel CreateModifyMarcaViewModel()
        {
            MarcaModel marcaModel=new MarcaModel(new MarcaDataMapper(), this.ActualUser);
            if (this._catalogMarcaModel != null && this._catalogMarcaModel.SelectedMarca != null)
            {
                marcaModel.MarcaName = this._catalogMarcaModel.SelectedMarca.MARCA_NAME;
                marcaModel.UnidMarca = this._catalogMarcaModel.SelectedMarca.UNID_MARCA;
            }
            return new ModifyMarcaViewModel(this, marcaModel);
        }

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempDeleteMarca()
        {
            bool _canDeleteMarca = false;
            foreach (DeleteMarca d in this._catalogMarcaModel.Marcas)
            {
                if (d.IsChecked == true)
                {
                    _canDeleteMarca = true;
                }
            }
            
            return _canDeleteMarca;
        }

        public void AttempDeleteMarca()
        {
            this._catalogMarcaModel.deleteMarca(this.ActualUser);

            if (this._catalogMarcaModel != null)
            {
                this._catalogMarcaModel.loadItems();
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
