using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel
{
    public class InsertUpdateItemStatusViewModel
    {
        private InsertUpdateItemStatusModel _insertUpdateItemStatusModel;
        
        public InsertUpdateItemStatusViewModel()
        {
            
            try
            {
                IDataMapper dataMapper = new EstatusDataMapper();

                this._insertUpdateItemStatusModel = new InsertUpdateItemStatusModel(dataMapper);   
            }
            catch (ArgumentException a)
            {

                ;
            }
            catch(Exception ex)
            {
                throw ex;
            }   
            
        }
        public InsertUpdateItemStatusModel InsertUpdateItemStatusModel
        {
            get
            {
                return _insertUpdateItemStatusModel;
            }
            set
            {
                _insertUpdateItemStatusModel = value;
            }
        }
    }
}
