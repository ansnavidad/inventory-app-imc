using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;
using InventoryApp.ViewModel.CatalogTecnico;

namespace InventoryApp.ViewModel.CatalogAlmacen
{
    public class ModifyAlmacenViewModel : INotifyPropertyChanged
    {
        #region Fields
        private AlmacenModel _modiAlmacen;
        private RelayCommand _modifyAlmacenCommand;
        private RelayCommand _borrarTecCommand;

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
                if (_catalogTecnicoModel != value)
                {
                    _catalogTecnicoModel = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("CatalogTecnicoModel"));
                    }
                }
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
                if (_catalogCiudadModel != value)
                {
                    _catalogCiudadModel = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("CatalogCiudadModel"));
                    }
                }
            }
        }
        public ICommand BorrarTecCommand
        {
            get
            {
                if (_borrarTecCommand == null)
                {
                    _borrarTecCommand = new RelayCommand(p => this.AttempBorrarTec(), p => this.CanAttempBorrarTec());
                }
                return _borrarTecCommand;
            }
        }
        public ICommand ModifyAlmacennCommand
        {
            get
            {
                if (_modifyAlmacenCommand == null)
                {
                    _modifyAlmacenCommand = new RelayCommand(p => this.AttempModifyAlmacenn(), p => this.CanAttempModifyAlmacenn());
                }
                return _modifyAlmacenCommand;
            }
        }
        #endregion

        #region Constructors
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
                this._catalogTecnicoModel = new CatalogTecnicoModel(new TecnicoDataMapper(), "s");
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

                for (int i = 0; i < this._catalogTecnicoModel.Tecnico.Count; ) {

                    if (!this._catalogTecnicoModel.Tecnico[i].IsChecked)
                        this._catalogTecnicoModel.Tecnico.RemoveAt(i);
                    else
                        i++;
                }

                for (int i = 0; i < this._catalogTecnicoModel.Tecnico.Count; i++)
                {

                    if (this._catalogTecnicoModel.Tecnico[i].IsChecked)
                    {
                        this._catalogTecnicoModel.Tecnico[i].IsChecked = false;
                        this._modiAlmacen._unidsTecnicos.Add(this._catalogTecnicoModel.Tecnico[i].UNID_TECNICO);
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
        public bool CanAttempModifyAlmacenn()
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

        public void AttempModifyAlmacenn()
        {
            //modificar para actualizar las relaciones proveedor categoria
            foreach (DeleteTecnico item in this._catalogTecnicoModel.Tecnico)
            {
                if (item.IsChecked == true)
                {
                    this._modiAlmacen._unidsTecnicos.Add(item.UNID_TECNICO);
                }
            }

            this._modiAlmacen.updateAlmacen();
            AddTecnicoViewModel addTec = new AddTecnicoViewModel(new CatalogTecnicoViewModel());
            addTec.AttempAddTecnicoExternal(new ALMACEN { ALMACEN_NAME = this.ModiAlmacen.AlmacenName, CONTACTO = this.ModiAlmacen.Contacto, DIRECCION = this.ModiAlmacen.Direccion, MAIL = this.ModiAlmacen.Mail, MAIL_DEFAULT = this.ModiAlmacen.MailDefault, IS_MODIFIED = true, IS_ACTIVE = true, UNID_ALMACEN = this.ModiAlmacen.UnidAlmacen }, this._catalogTecnicoModel);

            if (this._catalogAlmacenViewModel != null)
            {
                this._catalogAlmacenViewModel.loadAlmacen();
            }
        }

        public bool CanAttempBorrarTec()
        {
            bool _canAddAlmacen = true;
            return _canAddAlmacen;
        }

        public void AttempBorrarTec()
        {
            for (int i = 0; i < this._catalogTecnicoModel.Tecnico.Count; ) {

                if (this._catalogTecnicoModel.Tecnico[i].IsChecked == true)
                {
                    this._catalogTecnicoModel.Tecnico.RemoveAt(i);
                    this._modiAlmacen._unidsTecnicos.RemoveAt(i);
                }
                else
                    i++;
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
