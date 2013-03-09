using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;

namespace InventoryApp.Model.Seguridad
{
    public class Usuario : USUARIO
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            var eh = this.PropertyChanged;
            if (eh != null)
            {
                eh(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Constructors

        public Usuario(USUARIO u) {

            this.UNID_USUARIO = u.UNID_USUARIO;
            this.ACTIVATION = u.ACTIVATION;
            this.FLAG = u.FLAG;
            this.FLAG_PASS = u.FLAG_PASS;
            this.IS_ACTIVE = u.IS_ACTIVE;
            this.IS_MODIFIED = u.IS_MODIFIED;
            this.LAST_MODIFIED_DATE = u.LAST_MODIFIED_DATE;
            this.NUEVO_USUARIO = u.NUEVO_USUARIO;
            this.USUARIO_MAIL = u.USUARIO_MAIL;
            this.USUARIO_PWD = u.USUARIO_PWD;
        }

        #endregion
    }
}
