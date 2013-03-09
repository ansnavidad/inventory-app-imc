using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public class InventarioFisicoDetalleDataMapper : IDataMapper
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


        public void Upsert(List<POCOS.INVENTARIO_FISICO_DET> inventarioFisicoDetList)
        {
            if (inventarioFisicoDetList != null)
            {
                using (var Entity = new TAE2Entities())
                {
                    foreach (POCOS.INVENTARIO_FISICO_DET inventarioFisicoDet in inventarioFisicoDetList)
                    {
                        POCOS.INVENTARIO_FISICO_DET invFisicoDet;

                        invFisicoDet = Entity.INVENTARIO_FISICO_DET.FirstOrDefault(o => o.UNID_INVENTARIO_FISICO_DET == inventarioFisicoDet.UNID_INVENTARIO_FISICO_DET);
                        if (invFisicoDet != null)
                        {
                            invFisicoDet.UNID_INVENTARIO_FISICO_DET = inventarioFisicoDet.UNID_INVENTARIO_FISICO_DET;
                            invFisicoDet.UNID_ITEM = inventarioFisicoDet.UNID_ITEM;
                            invFisicoDet.UNID_INVENTARIO_FISICO = inventarioFisicoDet.UNID_INVENTARIO_FISICO;
                            invFisicoDet.IS_ACTIVE = true;
                            invFisicoDet.IS_MODIFIED = true;
                            invFisicoDet.LAST_MODIFIED_DATE = UNID.getNewUNID();
                            invFisicoDet.CANTIDAD = inventarioFisicoDet.CANTIDAD;
                            invFisicoDet.NUMERO_SERIE = inventarioFisicoDet.NUMERO_SERIE;
                            invFisicoDet.SKU = inventarioFisicoDet.SKU;
                            var modifiedSync = Entity.SYNCs.FirstOrDefault(p => p.UNID_SYNC == 20120101000000000);
                            if (modifiedSync != null)
                            {
                                modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                            }
                        }
                        else
                        {
                            Entity.INVENTARIO_FISICO_DET.AddObject(inventarioFisicoDet);
                        }
                    }
                    Entity.SaveChanges();
                }

            }
        }

        public void Delete(List<POCOS.INVENTARIO_FISICO_DET> inventarioFisicoDetList)
        {
            if (inventarioFisicoDetList != null)
            {
                using (var Entity = new TAE2Entities())
                {
                    foreach (POCOS.INVENTARIO_FISICO_DET inventarioFisicoDet in inventarioFisicoDetList)
                    {
                        POCOS.INVENTARIO_FISICO_DET invFisicoDet;

                        invFisicoDet = Entity.INVENTARIO_FISICO_DET.FirstOrDefault(o => o.UNID_INVENTARIO_FISICO_DET == inventarioFisicoDet.UNID_INVENTARIO_FISICO_DET);
                        if (invFisicoDet != null)
                        {
                            inventarioFisicoDet.IS_ACTIVE = false;
                            invFisicoDet.IS_MODIFIED = true;
                            invFisicoDet.LAST_MODIFIED_DATE = UNID.getNewUNID();
                            var modifiedSync = Entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                            modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                            Entity.INVENTARIO_FISICO_DET.AddObject(inventarioFisicoDet);
                        }
                    }
                    Entity.SaveChanges();
                }

            }
        }
    }
}

