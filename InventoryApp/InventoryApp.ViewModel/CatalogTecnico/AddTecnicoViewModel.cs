using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;
using InventoryApp.ViewModel.CatalogAlmacen;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.ViewModel.CatalogTecnico
{
    public class AddTecnicoViewModel
    {
        #region Fields
        private TecnicoModel _addTecnico;
        private RelayCommand _addTecnicoCommand;
        private CatalogTecnicoViewModel _catalogTecnicoViewModel;
        private AddAlmacenViewModel _almData;
        private ModifyAlmacenViewModel _almDataM;
        private CatalogCiudadModel _catalogCiudadModel;
        public string _aux;
        #endregion
        //Exponer la propiedad de tipo de cotizacion
        #region Props
        public TecnicoModel AddTecnico
        {
            get
            {
                return _addTecnico;
            }
            set
            {
                _addTecnico = value;
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

        public ICommand AddTecnicoCommand
        {
            get
            {
                if (_addTecnicoCommand == null)
                {
                    _addTecnicoCommand = new RelayCommand(p => this.AttempAddTecnico(), p => this.CanAttempAddTecnico());
                }
                return _addTecnicoCommand;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public AddTecnicoViewModel(CatalogTecnicoViewModel catalogTecnicoViewModel)
        {
            this._addTecnico = new TecnicoModel(new TecnicoDataMapper());
            this._catalogTecnicoViewModel = catalogTecnicoViewModel;

            try{
                
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
        }

        public AddTecnicoViewModel(CatalogTecnicoViewModel catalogTecnicoViewModel, string unidAlmacen, AddAlmacenViewModel alData)
        {
            this._almData = alData;
            this._addTecnico = new TecnicoModel(new TecnicoDataMapper());
            this._catalogTecnicoViewModel = catalogTecnicoViewModel;
            _aux = unidAlmacen;
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
        }

        public AddTecnicoViewModel(CatalogTecnicoViewModel catalogTecnicoViewModel, ModifyAlmacenViewModel alData)
        {
            this._almDataM = alData;
            this._addTecnico = new TecnicoModel(new TecnicoDataMapper());
            this._catalogTecnicoViewModel = catalogTecnicoViewModel;            
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
        }

        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempAddTecnico()
        {
            bool _canAddTecnico = true;
            if (String.IsNullOrEmpty(this._addTecnico.TecnicoName) || String.IsNullOrEmpty(this._addTecnico.Mail))            
                _canAddTecnico = false;

            return _canAddTecnico;
        }

        public void AttempAddTecnico()
        {            
            TECNICO t = new TECNICO();
            t.UNID_TECNICO = DAL.UNID.getNewUNID();
            t.IS_ACTIVE = true;            
            t.IS_MODIFIED = true;             
            t.UNID_TECNICO = this._addTecnico.UnidTecnico;
            t.UNID_CIUDAD = this._addTecnico.Ciudad.UNID_CIUDAD;
            t.TECNICO_NAME = this._addTecnico.TecnicoName;
            t.MAIL = this._addTecnico.Mail;

            DeleteTecnico tecAux = new DeleteTecnico(t);
            tecAux.IsChecked = false;

            if (_almData != null)
                _almData.CatalogTecnicoModel.Tecnico.Add(tecAux);
            else
            {
                _almDataM.CatalogTecnicoModel.Tecnico.Add(tecAux);
                _almDataM.ModiAlmacen._unidsTecnicos.Add(tecAux.UNID_TECNICO);
            }
        }

        public void AttempAddTecnicoExternal(ALMACEN alm, CatalogTecnicoModel tec)
        {
            if (_almData != null)
            {
                for (int i = 0; i < tec.Tecnico.Count; i++)
                {

                    this._addTecnico.UnidTecnico = DAL.UNID.getNewUNID();
                    this._addTecnico.Ciudad = new CIUDAD { UNID_CIUDAD = (long)tec.Tecnico[i].UNID_CIUDAD };
                    this._addTecnico.Mail = tec.Tecnico[i].MAIL;
                    this._addTecnico.TecnicoName = tec.Tecnico[i].TECNICO_NAME;

                    this._addTecnico.saveTecnico();

                    AlmacenDataMapper a = new AlmacenDataMapper();
                    a.UpsertMixRelation(new DAL.POCOS.ALMACEN_TECNICO { UNID_ALMACEN = alm.UNID_ALMACEN, UNID_TECNICO = this._addTecnico.UnidTecnico });
                }
            }
            else {

                for (int i = 0; i < tec.Tecnico.Count; i++)
                {
                    this._addTecnico.UnidTecnico = DAL.UNID.getNewUNID();
                    this._addTecnico.Ciudad = new CIUDAD { UNID_CIUDAD = (long)tec.Tecnico[i].UNID_CIUDAD };
                    this._addTecnico.Mail = tec.Tecnico[i].MAIL;
                    this._addTecnico.TecnicoName = tec.Tecnico[i].TECNICO_NAME;

                    this._addTecnico.saveTecnico();

                    AlmacenDataMapper a = new AlmacenDataMapper();
                    a.UpsertMixRelation(new DAL.POCOS.ALMACEN_TECNICO { UNID_ALMACEN = alm.UNID_ALMACEN, UNID_TECNICO = this._addTecnico.UnidTecnico });
                }
            }
        }

        #endregion
    }
}
