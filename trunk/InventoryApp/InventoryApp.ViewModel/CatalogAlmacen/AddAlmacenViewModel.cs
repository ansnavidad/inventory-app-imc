using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;

namespace InventoryApp.ViewModel.CatalogAlmacen
{
    public class AddAlmacenViewModel : INotifyPropertyChanged
    {
        #region Fields
        private AlmacenModel _addAlmacen;
        private RelayCommand _addAlmacenCommand;
        private RelayCommand _borrarTecCommand;

        private CatalogAlmacenViewModel _catalogAlmacenViewModel;
        private CatalogCiudadModel _catalogCiudadModel;
        private CatalogTecnicoModel _catalogTecnicoModel;
        #endregion

        //Exponer la propiedad almacen cuidad
        #region Props
        public AlmacenModel AddAlmacen
        {
            get
            {
                return _addAlmacen;
            }
            set
            {
                if (_addAlmacen != value)
                {
                    _addAlmacen = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("AddAlmacen"));
                    }
                }
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
        public ICommand AddAlmacenCommand
        {
            get
            {
                if (_addAlmacenCommand == null)
                {
                    _addAlmacenCommand = new RelayCommand(p => this.AttempAddAlmacen(), p => this.CanAttempAddAlmacen());
                }
                return _addAlmacenCommand;
            }
        }
        #endregion

        #region Constructors

        public AddAlmacenViewModel(CatalogAlmacenViewModel catalogAlmacenViewModel)
        {               
            this._addAlmacen = new AlmacenModel(new AlmacenDataMapper());
            this.AddAlmacen.UnidAlmacen = DAL.UNID.getNewUNID();            
            this._catalogAlmacenViewModel = catalogAlmacenViewModel;
            try
            {
                this._catalogCiudadModel = new CatalogCiudadModel(new CiudadDataMapper());
            }
            catch (ArgumentException ae) { ;}
            catch (Exception ex) { }          

            try
            {                
                this._catalogTecnicoModel = new CatalogTecnicoModel(new TecnicoDataMapper());              
            }
            catch (ArgumentException ae) {;}
            catch (Exception ex) { }
        }
        #endregion

        #region Methods

        public bool CanAttempBorrarTec()
        {
            bool _canAddAlmacen = true;
            return _canAddAlmacen;
        }

        public void AttempBorrarTec()
        {
            for (int i = 0; i < this._catalogTecnicoModel.Tecnico.Count; ) {

                if (this._catalogTecnicoModel.Tecnico[i].IsChecked == true)
                    this._catalogTecnicoModel.Tecnico.RemoveAt(i);
                else
                    i++;
            }
        }

        public bool CanAttempAddAlmacen()
        {
            bool _canAddAlmacen = true;
            if (String.IsNullOrEmpty(this._addAlmacen.AlmacenName) ||
                String.IsNullOrEmpty(this._addAlmacen.Contacto) ||
                String.IsNullOrEmpty(this._addAlmacen.Direccion) ||
                String.IsNullOrEmpty(this._addAlmacen.Mail) ||
                String.IsNullOrEmpty(this._addAlmacen.MailDefault))
                _canAddAlmacen = false;
            return _canAddAlmacen;
        }

        public void AttempAddAlmacen()
        {
            foreach (DeleteTecnico item in this._catalogTecnicoModel.Tecnico)
            {
                if (item.IsChecked == true)
                {
                    this._addAlmacen._unidsTecnicos.Add(item.UNID_TECNICO);
                }
            }

            this._addAlmacen.saveAlmacen();

            if (this._catalogAlmacenViewModel != null)
            {
                this._catalogAlmacenViewModel.loadAlmacen();
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
