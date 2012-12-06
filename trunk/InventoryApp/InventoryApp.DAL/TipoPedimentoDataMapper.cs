﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.DAL
{
    public class TipoPedimentoDataMapper : IDataMapper
    {
        public object getElements()
        {
            object o = null;
            FixupCollection<TIPO_PEDIMENTO> tp = new FixupCollection<TIPO_PEDIMENTO>();
            using (var Entity = new TAE2Entities())
            {
                var query = (from p in Entity.TIPO_PEDIMENTO
                             where p.IS_ACTIVE == true
                             select p).ToList();

                if (query.Count > 0)
                {
                    o = query;
                }

                return o;
            }
        }

        public object getElement(object element)
        {
            object o = null;
            if (element != null)
            {
                TIPO_PEDIMENTO Eprov = (TIPO_PEDIMENTO)element;
                FixupCollection<TIPO_PEDIMENTO> tp = new FixupCollection<TIPO_PEDIMENTO>();

                using (var Entity = new TAE2Entities())
                {
                    var query = (from p in Entity.TIPO_PEDIMENTO
                                 where p.UNID_TIPO_PEDIMENTO == Eprov.UNID_TIPO_PEDIMENTO
                                 select p).ToList();

                    if (query.Count > 0)
                    {
                        o = query;
                    }
                }
            }
            return o;
        }

        public void udpateElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    TIPO_PEDIMENTO tipoPedimento = (TIPO_PEDIMENTO)element;
                    var modifiedTipoPedimento = entity.TIPO_PEDIMENTO.First(p => p.UNID_TIPO_PEDIMENTO == tipoPedimento.UNID_TIPO_PEDIMENTO);
                    modifiedTipoPedimento.TIPO_PEDIMENTO_NAME = tipoPedimento.TIPO_PEDIMENTO_NAME;
                    modifiedTipoPedimento.REGIMEN = tipoPedimento.REGIMEN;
                    modifiedTipoPedimento.NOTA = tipoPedimento.NOTA;
                    modifiedTipoPedimento.CLAVE = tipoPedimento.CLAVE;
                    //Sync
                    modifiedTipoPedimento.IS_MODIFIED = true;
                    modifiedTipoPedimento.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    TIPO_PEDIMENTO tipoPedimento = (TIPO_PEDIMENTO)element;

                    var validacion = (from cust in entity.TIPO_PEDIMENTO
                                      where cust.TIPO_PEDIMENTO_NAME == tipoPedimento.TIPO_PEDIMENTO_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        tipoPedimento.UNID_TIPO_PEDIMENTO = UNID.getNewUNID();
                        //Sync
                        tipoPedimento.IS_MODIFIED = true;
                        tipoPedimento.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID(); 
                        entity.SaveChanges();
                        //
                        entity.TIPO_PEDIMENTO.AddObject(tipoPedimento);
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
                    TIPO_PEDIMENTO tipoPedimento = (TIPO_PEDIMENTO)element;

                    var deleteTipoPedimento = entity.TIPO_PEDIMENTO.First(p => p.UNID_TIPO_PEDIMENTO == tipoPedimento.UNID_TIPO_PEDIMENTO);

                    deleteTipoPedimento.IS_ACTIVE = false;
                    //Sync
                    deleteTipoPedimento.IS_MODIFIED = true;
                    deleteTipoPedimento.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID(); 
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        public List<TIPO_PEDIMENTO> getListElements()
        {
            List<TIPO_PEDIMENTO> tipoPedimentos = new List<TIPO_PEDIMENTO>();

            try
            {
                using (var Entity = new TAE2Entities())
                {
                    (from p in Entity.TIPO_PEDIMENTO
                     select p).ToList<TIPO_PEDIMENTO>().ForEach(o => tipoPedimentos.Add(o));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return tipoPedimentos;
        }
    }
}
