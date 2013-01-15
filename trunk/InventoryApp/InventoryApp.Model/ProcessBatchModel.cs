using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL;
using System.ComponentModel;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.Model
{
    public class ProcessBatchModel : INotifyPropertyChanged
    {
        private FixupCollection<PROCESS_BATCH> _proccesBatch;        
        private IDataMapper _dataMapper;

        public FixupCollection<PROCESS_BATCH> ProccesBatch
        {
            get
            {
                return _proccesBatch;
            }
            set
            {
                if (_proccesBatch != value)
                {
                    _proccesBatch = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("ProccesBatch"));
                    }
                }
            }
        }
        
        public ProcessBatchModel(IDataMapper iDataMapper)
        {
            this._dataMapper = new ProcessBachDataMapper();
            this._proccesBatch = new FixupCollection<PROCESS_BATCH>();            
            this.loadItems();            
        }

        public void loadItems()
        {
            List<PROCESS_BATCH> l = new List<PROCESS_BATCH>();
            //JobViewModel j = new JobViewModel();
            //l = 
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
