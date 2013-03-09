using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;
using InventoryApp.DAL;

namespace InventoryApp.Model
{
    public class UsuarioModel: INotifyPropertyChanged
    {
        #region Fields
        private long _unidUsuario;
        private string _userMail;
        private string _password;
        private string _newPassword;
        private string _confirmeNewpassword;
        private bool _enviarMail;
        private FixupCollection<DeleteRol> _rol;
        private string _mensajeError;
        private AppUsuario _dataMapper;
        private AppUsuarioRol _userRolDataMapper;
        #endregion

        #region Props

        public long UnidUsuario
        {
            get
            {
                return _unidUsuario;
            }
            set
            {
                if (_unidUsuario != value)
                {
                    _unidUsuario = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidUsuario"));
                    }
                }
            }
        }

        public string UserMail
        {
            get
            {
                return _userMail;
            }
            set
            {
                if (_userMail != value)
                {
                    _userMail = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UserMail"));
                    }
                }
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Password"));
                    }
                }
            }
        }

        public string NewPassword
        {
            get
            {
                return _newPassword;
            }
            set
            {
                if (_newPassword != value)
                {
                    _newPassword = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("NewPassword"));
                    }
                }
            }
        }

        public string ConfirmeNewpassword
        {
            get
            {
                return _confirmeNewpassword;
            }
            set
            {
                if (_confirmeNewpassword != value)
                {
                    _confirmeNewpassword = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ConfirmeNewpassword"));
                    }
                }
            }
        }

        public string MensajeError
        {
            get
            {
                return _mensajeError;
            }
            set
            {
                if (_mensajeError != value)
                {
                    _mensajeError = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("MensajeError"));
                    }
                }
            }
        }

        public bool EnviarMail
        {
            get
            {
                return _enviarMail;
            }
            set
            {
                if (_enviarMail != value)
                {
                    _enviarMail = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("EnviarMail"));
                    }
                }
            }
        }

        public FixupCollection<DeleteRol> Rol
        {
            get
            {
                return _rol;
            }
            set
            {
                if (_rol != value)
                {
                    _rol = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Rol"));
                    }
                }
            }
        }
        #endregion

        public void updateUser()
        {
            _dataMapper.udpateElement(new USUARIO() { IS_ACTIVE = true, UNID_USUARIO = this._unidUsuario, USUARIO_MAIL = this._userMail,  USUARIO_PWD = this._password});
        }

        public void updateUserNewCotraseña()
        {
            _dataMapper.udpateElementContraseña(new USUARIO() { UNID_USUARIO = this._unidUsuario, USUARIO_PWD = this._newPassword, FLAG_PASS=this._enviarMail});
        }

        public void updateUserSameCotraseña()
        {
            _dataMapper.udpateElementContraseña(new USUARIO() { UNID_USUARIO = this._unidUsuario, USUARIO_PWD = this._password, FLAG_PASS=this._enviarMail });
        }

        public void DeleteRelationUsuarioRol(long unidRol, long unidUser)
        {
            this._userRolDataMapper = new AppUsuarioRol();

            if (_dataMapper != null)
            {
               this._userRolDataMapper.deleteElement(new USUARIO_ROL() { UNID_USUARIO = unidUser, UNID_ROL = unidRol },new USUARIO());
            }
        }

        public object GetUsuarioRol(long unidUser)
        {
            this._userRolDataMapper = new AppUsuarioRol();
            object element = this._userRolDataMapper.getElementByUserRol(unidUser);
            return element;
        }

        public object GetRolMenu(long unidRol)
        {
            this._userRolDataMapper = new AppUsuarioRol();

            object element = this._userRolDataMapper.getElementByRolMenu(unidRol);
            return element;
        }

        public void insertRolMenu(long unidRol, long unidMenu)
        {
            this._userRolDataMapper = new AppUsuarioRol();

            if (_dataMapper != null)
            {
                this._userRolDataMapper.udpateRolMenu(new ROL_MENU() { UNID_MENU = unidMenu, UNID_ROL = unidRol });
            }
            
        }

        public void insertUsuarioRol(long unidRol, long unidUser)
        {
            this._userRolDataMapper = new AppUsuarioRol();

            if (_dataMapper != null)
            {
                this._userRolDataMapper.udpateUsuarioRol(new USUARIO_ROL() { UNID_USUARIO = unidUser, UNID_ROL = unidRol });
            }
        }

        #region Constructors
        public UsuarioModel(IDataMapper dataMapper)
        {
            if ((dataMapper as AppUsuario) != null)
            {
                this._dataMapper = dataMapper as AppUsuario;
                this._userRolDataMapper = dataMapper as AppUsuarioRol;
            }


        }

        public UsuarioModel()
        {
            this._dataMapper = new AppUsuario();
            this._userRolDataMapper = new AppUsuarioRol();
        }
        
        
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
