using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public class InventarioFisicoDataMapper : IDataMapper   
    {
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
            throw new NotImplementedException();
        }

        public void insertElement(object element)
        {
            throw new NotImplementedException();
        }

        public void deleteElement(object element, POCOS.USUARIO u)
        {
            throw new NotImplementedException();
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }

        public void Insert(POCOS.INVENTARIO_FISICO inventarioFisico)
        {
            if (inventarioFisico != null)
            {
                POCOS.INVENTARIO_FISICO invFisico;
                using (var Entity = new TAE2Entities())
                {
                    invFisico = Entity.INVENTARIO_FISICO.FirstOrDefault(o => o.UNID_INVENTARIO_FISICO == inventarioFisico.UNID_INVENTARIO_FISICO);
                    if (invFisico != null)
                    {
                        invFisico.UNID_ALMACEN = inventarioFisico.UNID_ALMACEN;
                        invFisico.FECHA_FIN = inventarioFisico.FECHA_FIN;


                        //Sfisiync
                        invFisico.IS_MODIFIED = true;
                        //invFisico.IS_ACTIVE = true;
                        invFisico.LAST_MODIFIED_DATE = UNID.getNewUNID();

                        var modifiedSync = Entity.SYNCs.FirstOrDefault(p => p.UNID_SYNC == 20120101000000000);
                        if (modifiedSync != null)
                        {
                            modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        }

                        //
                        Entity.SaveChanges();
                        //UNID.Master(recibo, u, -1, "Modificación");
                    }
                    else
                    {
                        Entity.INVENTARIO_FISICO.AddObject(inventarioFisico);
                        Entity.SaveChanges();
                    }
                }
            }
        }
    }
}
