using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public class AppUsuario : IDataMapper
    {
        public void loadSync(object element)
        {

            if (element != null)
            {
                USUARIO poco = (USUARIO)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.USUARIOs
                                 where poco.UNID_USUARIO == cust.UNID_USUARIO
                                 select cust).ToList();

                    //Actualización
                    if (query.Count > 0)
                    {
                        udpateElement((object)poco);
                    }
                    //Inserción
                    else
                    {
                        insertElement((object)poco);
                    }
                }
            }
        }

        public object getElements()
        {
            throw new NotImplementedException();
        }

        public object getElement(object element)
        {
            throw new NotImplementedException();
        }

        public void udpateElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    USUARIO usuario = (USUARIO)element;
                    var modifiedUsuario = entity.USUARIOs.First(p => p.UNID_USUARIO == usuario.UNID_USUARIO);
                    modifiedUsuario.USUARIO_MAIL = usuario.USUARIO_MAIL;
                    modifiedUsuario.USUARIO_PWD = usuario.USUARIO_PWD;                    
                    //Sync
                    modifiedUsuario.IS_MODIFIED = true;
                    modifiedUsuario.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    //
                    entity.SaveChanges();
                }
            }
        }

        public void insertElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    USUARIO usuario = (USUARIO)element;

                    var validacion = (from cust in entity.USUARIOs
                                      where cust.UNID_USUARIO == usuario.UNID_USUARIO
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        usuario.UNID_USUARIO = UNID.getNewUNID();
                        //Sync
                        usuario.IS_MODIFIED = true;
                        usuario.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.USUARIOs.AddObject(usuario);
                        entity.SaveChanges();
                    }
                }
            }
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}
