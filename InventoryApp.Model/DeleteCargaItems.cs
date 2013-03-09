using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class DeleteCargaItems : BATCH_LOAD_CARGAMOV, INotifyPropertyChanged
    {
        public DeleteCargaItems( BATCH_LOAD_CARGAMOV batch)
        {
            this.END_EXEC_DATE = batch.END_EXEC_DATE;
            this.ID_BATCH = batch.ID_BATCH;
            this.START_EXEC_DATE = batch.START_EXEC_DATE;

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
