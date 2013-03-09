using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class RolModel : INotifyPropertyChanged
    {
        #region Fields
        private long _unidRol;
        private string _rolName;
        private AppRolDataMapper _dataMapper;
        #endregion

        #region Props
        public long UnidRol
        {
            get
            {
                return _unidRol;
            }
            set
            {
                if (_unidRol != value)
                {
                    _unidRol = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidRol"));
                    }
                }
            }
        }

        public string RolName
        {
            get
            {
                return _rolName;
            }
            set
            {
                if (_rolName != value)
                {
                    _rolName = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("RolName"));
                    }
                }
            }
        }
        #endregion

        #region Constructors
        public RolModel(AppRolDataMapper dataMapper)
        {
            if ((dataMapper as AppRolDataMapper) != null)
            {
                this._dataMapper = dataMapper as AppRolDataMapper;
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
