using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;
using InventoryApp.DAL;

namespace InventoryApp.Model
{
    public class DeleteUsuarios:USUARIO, INotifyPropertyChanged
    {
        private bool _isChecked;
        private string _usuariosRoles; 
        private FixupCollection<DeleteRol> _rol;
        private AppRolDataMapper _dataMapper;

        public FixupCollection<DeleteRol> Roles
        {
            get { return this._rol; }
            set
            {
                if (value != this._rol)
                {
                    this._rol = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Roles"));
                }
            }
        }
        public bool IsChecked
        {
            get { return this._isChecked; }
            set
            {
                if (value != this._isChecked)
                {
                    this._isChecked = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("IsChecked"));
                }
            }
        }

        public string UsuariosRoles
        {
            get { return this._usuariosRoles; }
            set
            {
                if (value != this._usuariosRoles)
                {
                    this._usuariosRoles = value;
                    if (this.PropertyChanged != null)
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UsuariosRoles"));
                }
            }
        }
        public DeleteUsuarios(USUARIO usuario)
        {
            this._dataMapper = new AppRolDataMapper();

            this.UNID_USUARIO = usuario.UNID_USUARIO;
            this.NUEVO_USUARIO = usuario.NUEVO_USUARIO;
            this.USUARIO_MAIL = usuario.USUARIO_MAIL;
            this.USUARIO_PWD = usuario.USUARIO_PWD;
            this.USUARIO_ROL = usuario.USUARIO_ROL;
            this.ACTIVATION = usuario.ACTIVATION;
            this.FLAG = usuario.FLAG;
            this.FLAG_PASS = usuario.FLAG_PASS;
            this.IS_ACTIVE = usuario.IS_ACTIVE;
            this.IsChecked = false;
            loadRoles();
            
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void loadRoles()
        {
            
            object element = this._dataMapper.getElementRoles(this.UNID_USUARIO);

            FixupCollection<DeleteRol> ic = new FixupCollection<DeleteRol>();
            string ru = null;
            if (element != null)
            {
                if (((List<ROL>)element).Count > 0)
                {
                    foreach (ROL item in (List<ROL>)element)
                    {
                        if (item.UNID_ROL !=1)
                        {
                            DeleteRol aux = new DeleteRol(item);
                            ic.Add(aux);   
                            ru += item.ROL_NAME+ " ; ";
                        }
                        
                    }
                }
            }
            this.Roles = ic;
            this.UsuariosRoles ="Roles actuales : "+ ru;
            
        }
    }
}
