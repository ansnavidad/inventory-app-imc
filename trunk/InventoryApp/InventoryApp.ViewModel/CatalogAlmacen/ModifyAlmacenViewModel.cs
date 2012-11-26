using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.ViewModel.CatalogAlmacen
{
    public class ModifyAlmacenViewModel
    {
        #region Fields
        private AlmacenModel _modiAlmacen;
        private RelayCommand _modifyAlmacenCommand;
        private CatalogAlmacenViewModel _catalogAlmacenViewModel;
        private CatalogCiudadModel _catalogCiudadModel;
        private CatalogTecnicoModel _catalogTecnicoModel;
        #endregion

        //Exponer la propiedad almacen y cuidad
        #region Props
        public AlmacenModel ModiAlmacen
        {
            get
            {
                return _modiAlmacen;
            }
            set
            {
                _modiAlmacen = value;
            }
        }
        public CatalogTecnicoModel CatalogTecnicoModel
        {
            get
            {
                return _catalogTecnicoModel;
            }
            set
            {
                _catalogTecnicoModel = value;
            }
        }
        public CatalogCiudadModel CatalogCiudadModel
        {
            get
            {
                return _catalogCiudadModel;
            }
            set
            {
                _catalogCiudadModel = value;
            }
        }
        
        public ICommand ModifyAlmacenCommand
        {
            get
            {
                if (_modifyAlmacenCommand == null)
                {
                    _modifyAlmacenCommand = new RelayCommand(p => this.AttempModifyAlmacen(), p => this.CanAttempModifyAlmacen());
                }
                return _modifyAlmacenCommand;
            }
        }
        #endregion

         #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public ModifyAlmacenViewModel(CatalogAlmacenViewModel catalogAlmacenViewModel, AlmacenModel selectedAlmacenModel)
        {
            this._modiAlmacen = new AlmacenModel(new AlmacenDataMapper());
            this._catalogAlmacenViewModel = catalogAlmacenViewModel;
            this._modiAlmacen.UnidAlmacen = selectedAlmacenModel.UnidAlmacen;
            this._modiAlmacen.AlmacenName = selectedAlmacenModel.AlmacenName;            
            this._modiAlmacen.Contacto = selectedAlmacenModel.Contacto;
            this._modiAlmacen.Direccion = selectedAlmacenModel.Direccion;
            this._modiAlmacen.Mail = selectedAlmacenModel.Mail;
            this._modiAlmacen.MailDefault = selectedAlmacenModel.MailDefault;

            try
            {
                this._catalogCiudadModel = new CatalogCiudadModel(new CiudadDataMapper());
            }
            catch (ArgumentException ae)
            {
                ;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {
                object ret = this._modiAlmacen.GetAlmacenCategoria(selectedAlmacenModel.UnidAlmacen);
                this._catalogTecnicoModel = new CatalogTecnicoModel(new TecnicoDataMapper());
                //muestra los valores de las tecnicos que estan relacionadas
                foreach (var item in this._catalogTecnicoModel.Tecnico)
                {
                    foreach (var ite in ((List<TECNICO>)ret))
                    {
                        if (item.UNID_TECNICO == ite.UNID_TECNICO)
                        {
                            item.IsChecked = true;
                            this._modiAlmacen._auxUnidsTecnicos.Add(ite.UNID_TECNICO);
                        }
                    }
                }
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
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempModifyAlmacen()
        {
            bool _canAddAlmacen = true;
            if (String.IsNullOrEmpty(this._modiAlmacen.AlmacenName) ||
                String.IsNullOrEmpty(this._modiAlmacen.Contacto) ||
                String.IsNullOrEmpty(this._modiAlmacen.Direccion) ||
                String.IsNullOrEmpty(this._modiAlmacen.Mail) ||
                String.IsNullOrEmpty(this._modiAlmacen.MailDefault))
                _canAddAlmacen = false;
            return _canAddAlmacen;
        }

        public void AttempModifyAlmacen()
        {
            //modificar para actualizar las relaciones proveedor categoria
            foreach (DeleteTecnico at in this._catalogTecnicoModel.Tecnico)
            {
                if (at.IsChecked == true)
                {
                    this._modiAlmacen._unidsTecnicos.Add(at.UNID_TECNICO);
                }
            }
            this._modiAlmacen.updateAlmacen();

            if (this._catalogAlmacenViewModel != null)
            {
                this._catalogAlmacenViewModel.loadAlmacen();
            }
        }
        #endregion
    }
}
