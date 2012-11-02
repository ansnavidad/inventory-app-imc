using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL;
using System.ComponentModel;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.Model
{
    public class InsertUpdateItemStatusModel : INotifyPropertyChanged
    {
        private IDataMapper _dataMapper;


        private ITEM_STATUS _newStaus;

        public ITEM_STATUS NewStaus
        {
            get
            {
                return _newStaus;
            }
            set
            {

                if (_newStaus != value)
                {
                    if (!_newStaus.Equals(""))
                    {
                        _newStaus = value;
                        if (PropertyChanged != null)
                        {
                            PropertyChanged(this, new PropertyChangedEventArgs("NewStaus"));
                        }
                    }
                }
            }
        }
        
        public InsertUpdateItemStatusModel(IDataMapper dataMapper)
        {

            this._dataMapper = new EstatusDataMapper();
            
               // this.insert();      
        }
        public void insert()
        {
           // ITEM_STATUS i = new ITEM_STATUS();
           // i.ITEM_STATUS_NAME = ITEM_STATUS_NAME;
           //this._dataMapper.insertElement((object)i);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
