using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL;
using InventoryApp.DAL.Recibo;
using System.Collections.ObjectModel;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;

namespace InventoryApp.Model.Historial
{
    public class HistorialModel : ModelBase
    {
        #region Properties

        public bool IsChecked
        {
            get { return _IsChecked; }
            set
            {
                if (_IsChecked != value)
                {
                    _IsChecked = value;
                    OnPropertyChanged(IsCheckedPropertyName);
                }
            }
        }
        private bool _IsChecked;
        public const string IsCheckedPropertyName = "IsChecked";

        public string Tipo
        {
            get { return _Tipo; }
            set
            {
                if (_Tipo != value)
                {
                    _Tipo = value;
                    OnPropertyChanged(TipoPropertyName);
                }
            }
        }
        private string _Tipo;
        public const string TipoPropertyName = "Tipo";

        public string Roles
        {
            get { return _Roles; }
            set
            {
                if (_Roles != value)
                {
                    _Roles = value;
                    OnPropertyChanged(RolesPropertyName);
                }
            }
        }
        private string _Roles;
        public const string RolesPropertyName = "Roles";
                
        public DateTime Fecha
        {
            get { return _Fecha; }
            set
            {
                if (_Fecha != value)
                {
                    _Fecha = value;
                    OnPropertyChanged(FechaPropertyName);
                }
            }
        }
        private DateTime _Fecha;
        public const string FechaPropertyName = "Fecha";

        public USUARIO User
        {
            get { return _User; }
            set
            {
                if (_User != value)
                {
                    _User = value;
                    OnPropertyChanged(UserPropertyName);
                }
            }
        }
        private USUARIO _User;
        public const string UserPropertyName = "User";

        #endregion

        #region Constructors

        public HistorialModel()
        {            

        }

        #endregion

        #region Methods

        #endregion
    }
}
