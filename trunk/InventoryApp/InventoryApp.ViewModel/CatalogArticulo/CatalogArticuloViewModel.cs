using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;
using System.Windows.Input;

namespace InventoryApp.ViewModel.CatalogArticulo
{
    public class CatalogArticuloViewModel
    {
         private RelayCommand _deleteArticuloCommand;
         private CatalogArticuloModel _catalogArticuloModel;

         public ICommand DeleteArticuloCommand
         {
             get
             {
                 if (_deleteArticuloCommand == null)
                 {
                     _deleteArticuloCommand = new RelayCommand(p => this.AttempDeleteArticulo(), p => this.CanAttempDeleteArticulo());
                 }
                 return _deleteArticuloCommand;
             }
         }

        public CatalogArticuloViewModel()
        {
            try
            {
                IDataMapper dataMapper = new ArticuloDataMapper();
                this._catalogArticuloModel = new CatalogArticuloModel(dataMapper);
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

        public CatalogArticuloModel CatalogArticuloModel
        {
            get
            {
                return _catalogArticuloModel;
            }
            set
            {
                _catalogArticuloModel = value;
            }
        }

        public void loadItems()
        {
            this._catalogArticuloModel.loadItems();
        }

        public AddArticuloViewModel CreateAddArticuloViewModel()
        {
            AddArticuloViewModel p;
            try
            {
                p = new AddArticuloViewModel(this);
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            return p;
        }

        public ModifyArticuloViewModel CreateModifyArticuloViewModel()
        {
            ArticuloModel articuloModel = new ArticuloModel(new ArticuloDataMapper());
            if (this._catalogArticuloModel != null && this._catalogArticuloModel.SelectedArticulo != null)
            {
                articuloModel.ArticuloName = this._catalogArticuloModel.SelectedArticulo.ARTICULO1;
                articuloModel.UnidArticulo = this._catalogArticuloModel.SelectedArticulo.UNID_ARTICULO;
                articuloModel.Categoria = this._catalogArticuloModel.SelectedArticulo.CATEGORIA;
                articuloModel.Marca = this._catalogArticuloModel.SelectedArticulo.MARCA;
                articuloModel.Modelo = this._catalogArticuloModel.SelectedArticulo.MODELO;
                articuloModel.Equipo = this._catalogArticuloModel.SelectedArticulo.EQUIPO;

            }
            return new ModifyArticuloViewModel(this, articuloModel);
        }

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempDeleteArticulo()
        {
            bool _canDeleteArticulo = false;
            foreach (DeleteArticulo d in this._catalogArticuloModel.Articulos)
            {
                if (d.IsChecked == true)
                {
                    _canDeleteArticulo = true;
                }
            }

            return _canDeleteArticulo;
        }
        public void AttempDeleteArticulo()
        {
            this._catalogArticuloModel.deleteArticulos();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya quef
            if (this._catalogArticuloModel != null)
            {
                this._catalogArticuloModel.loadItems();
            }
        }
        #endregion
    }
}
