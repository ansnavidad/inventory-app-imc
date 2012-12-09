using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class TransporteDataMapper : IDataMapper
    {
        /// <summary>
        /// Obtiene todos los elementos en la tabla transporte
        /// </summary>
        /// <returns></returns>
        public object getElements()
        {
            object res = null;
            using (var entity = new TAE2Entities())
            {
                res = (from transporte in entity.TRANSPORTEs
                       where transporte.IS_ACTIVE==true
                       select transporte).ToList();

                foreach (TRANSPORTE trans in ((List<TRANSPORTE>)res))
                {
                    trans.TIPO_EMPRESA = trans.TIPO_EMPRESA;
                }


                return res;
            }
        }

        public object getElement(object element)
        {
            object res = null;

            using (var entity = new TAE2Entities())
            {
                TRANSPORTE Etra = (TRANSPORTE)element;

                res = (from cust in entity.TRANSPORTEs
                       where cust.UNID_TRANSPORTE == Etra.UNID_TRANSPORTE
                       select cust).ToList<TRANSPORTE>();

                foreach (TRANSPORTE trans in ((List<TRANSPORTE>)res))
                {
                    trans.TIPO_EMPRESA = trans.TIPO_EMPRESA;
                }

                return res;
            }
        }

        public void udpateElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    TRANSPORTE transporte = (TRANSPORTE)element;
                    var modifiedItemStatus = entity.TRANSPORTEs.First(p => p.UNID_TRANSPORTE == transporte.UNID_TRANSPORTE);
                    modifiedItemStatus.TRANSPORTE_NAME = transporte.TRANSPORTE_NAME;
                    modifiedItemStatus.UNID_TIPO_EMPRESA = transporte.UNID_TIPO_EMPRESA;
                    //Sync
                    modifiedItemStatus.IS_MODIFIED = true;
                    modifiedItemStatus.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID(); 
                    entity.SaveChanges();
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
                    TRANSPORTE tra = (TRANSPORTE)element;

                    var validacion = (from cust in entity.TRANSPORTEs
                                      where cust.TRANSPORTE_NAME == tra.TRANSPORTE_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        tra.UNID_TRANSPORTE = UNID.getNewUNID();
                        //Sync
                        tra.IS_MODIFIED = true;
                        tra.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID(); 
                        entity.SaveChanges();
                        //
                        entity.TRANSPORTEs.AddObject(tra);
                        entity.SaveChanges();
                    }
                }
            }
        }

        public void deleteElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    TRANSPORTE transporte = (TRANSPORTE)element;

                    var deleteTransporte = entity.TRANSPORTEs.First(p => p.UNID_TRANSPORTE == transporte.UNID_TRANSPORTE);

                    deleteTransporte.IS_ACTIVE = false;
                    //Sync
                    deleteTransporte.IS_MODIFIED = true;
                    deleteTransporte.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID(); 
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Método que serializa una List<TRANSPORTE> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de TRANSPORTE</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonTransporte()
        {
            string res = null;
            List<TRANSPORTE> listTransporte = new List<TRANSPORTE>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.TRANSPORTEs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listTransporte.Add(new TRANSPORTE
                     {
                         UNID_TRANSPORTE=row.UNID_TRANSPORTE,
                         TRANSPORTE_NAME=row.TRANSPORTE_NAME,
                         UNID_TIPO_EMPRESA=row.UNID_TIPO_EMPRESA,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listTransporte.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listTransporte);
                }
                return res;
            }
        }
    }
}
