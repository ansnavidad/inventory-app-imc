﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;
using System.Windows.Input;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.ViewModel.CatalogTransporte
{
    public class CatalogTransporteViewModel : IPageViewModel
    {
        private RelayCommand _deleteTransporteCommand;
        private CatalogTransporteModel _catalogTransporteModel;
        public USUARIO ActualUser;

        public ICommand DeleteTransporteCommand
        {
            get
            {
                if (_deleteTransporteCommand == null)
                {
                    _deleteTransporteCommand = new RelayCommand(p => this.AttempDeleteTransporte(), p => this.CanAttempDeleteTransporte());
                }
                return _deleteTransporteCommand;
            }
        }

        public CatalogTransporteViewModel()
        {
            try
            {
                IDataMapper dataMapper = new TransporteDataMapper();
                this._catalogTransporteModel = new CatalogTransporteModel(dataMapper);
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

        public CatalogTransporteViewModel(USUARIO u)
        {
            try
            {
                IDataMapper dataMapper = new TransporteDataMapper();
                this._catalogTransporteModel = new CatalogTransporteModel(dataMapper);
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

        public CatalogTransporteModel CatalogTransporteModel
        {
            get
            {
                return _catalogTransporteModel;
            }
            set
            {
                _catalogTransporteModel = value;
            }
        }

        public void loadItems()
        {
            this._catalogTransporteModel.loadItems();
        }

        /// <summary>
        /// Crea una nueva instancia de addItemStatus y se pasa asi mismo como parámetro
        /// Referenca pag 232 del libro MVVM
        /// </summary>
        /// <returns></returns>
        public AddTransporteViewModel CreateAddTransporteViewModel()
        {
            return new AddTransporteViewModel(this);
        }
        //public InsertTransporteViewModel CreateInsertTransporteViewModel()
        //{
        //    return new InsertTransporteViewModel(this);
        //}

        public ModifyTransporteViewModel CreateModifyTransporteViewModel()
        {
            InsertTransporteModel transporteModel = new InsertTransporteModel(new TransporteDataMapper(), this.ActualUser);
            if (this._catalogTransporteModel != null && this._catalogTransporteModel.SelectedTransporte != null)
            {
                transporteModel.TransporteName = this._catalogTransporteModel.SelectedTransporte.TRANSPORTE_NAME;
                transporteModel.UnidTransporte = this._catalogTransporteModel.SelectedTransporte.UNID_TRANSPORTE;
                transporteModel.TipoEmpresa = this._catalogTransporteModel.SelectedTransporte.TipoEmpresa;
                

            }
            return new ModifyTransporteViewModel(this, transporteModel);
        }

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempDeleteTransporte()
        {
            bool _canDeleteTransporte = false;
            foreach (DeleteTransporte d in this._catalogTransporteModel.Transporte)
            {
                if (d.IsChecked == true)
                {
                    _canDeleteTransporte = true;
                }
            }

            return _canDeleteTransporte;
        }

        public void AttempDeleteTransporte()
        {
            this._catalogTransporteModel.deleteTransporte(this.ActualUser);

            if (this._catalogTransporteModel != null)
            {
                this._catalogTransporteModel.loadItems();
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
