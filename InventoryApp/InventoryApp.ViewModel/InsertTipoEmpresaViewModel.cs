using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL;
using InventoryApp.Model;

namespace InventoryApp.ViewModel
{
    public class InsertTipoEmpresaViewModel
    {
        private InsertTipoEmpresaModel _insertTipoEmpresaModel;

        public InsertTipoEmpresaViewModel()
        {

            try
            {
                this._insertTipoEmpresaModel = new InsertTipoEmpresaModel();
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




        public void InsertItem()
        {
            //this._insertTipoEmpresaModel = new InsertTipoEmpresaModel();
            this._insertTipoEmpresaModel.insertItems();
           
        }
        public InsertTipoEmpresaModel InsertTipoEmpresaModel
        {
            get
            {
                return _insertTipoEmpresaModel;
            }
            set
            {
                _insertTipoEmpresaModel = value;
            }
        }
    }
}
