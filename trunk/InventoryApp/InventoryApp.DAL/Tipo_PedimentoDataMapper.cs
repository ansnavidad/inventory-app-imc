using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.DAL
{
    class Tipo_PedimentoDataMapper : IDataMapper
    {
        public object getElements()
        {
            object o = null;
            using (var Entity = new TAE2Entities())
            {
                var provs = from p in Entity.TIPO_PEDIMENTO
                            select p;

                if (provs != null)
                    o = (object)provs;

                return o;
            }
        }

        public object getElement(object element)
        {
            object o = null;
            if (element != null)
            {
                TIPO_PEDIMENTO Eprov = (TIPO_PEDIMENTO)element;

                using (var Entity = new TAE2Entities())
                {
                    var provs = from p in Entity.TIPO_PEDIMENTO
                                where p.UNID_TIPO_PEDIMENTO == Eprov.UNID_TIPO_PEDIMENTO
                                select p;

                    if (provs != null)
                        o = (object)provs;
                }
            }
            return o;
        }

        public void udpateElement(object element)
        {
            if (element != null)
            {
                TIPO_PEDIMENTO Eprov = (TIPO_PEDIMENTO)element;

                using (var Entity = new TAE2Entities())
                {
                    var query = from p in Entity.TIPO_PEDIMENTO
                                where p.UNID_TIPO_PEDIMENTO == Eprov.UNID_TIPO_PEDIMENTO
                                select p;

                    var prov = query.First();

                    prov.TIPO_PEDIMENTO_NAME = Eprov.TIPO_PEDIMENTO_NAME;
                    prov.CLAVE = Eprov.CLAVE;
                    prov.REGIMEN = Eprov.REGIMEN;
                    prov.NOTA = Eprov.NOTA;

                    Entity.SaveChanges();
                }
            }
        }

        public void insertElement(object element)
        {
            if (element != null)
            {

                using (var Entity = new TAE2Entities())
                {

                    TIPO_PEDIMENTO Eprov = (TIPO_PEDIMENTO)element;
                    TIPO_PEDIMENTO prov = new TIPO_PEDIMENTO();

                    prov.UNID_TIPO_PEDIMENTO = UNID.getNewUNID();
                    prov.TIPO_PEDIMENTO_NAME = Eprov.TIPO_PEDIMENTO_NAME;
                    prov.CLAVE = Eprov.CLAVE;
                    prov.REGIMEN = Eprov.REGIMEN;
                    prov.NOTA = Eprov.NOTA;

                    Entity.TIPO_PEDIMENTO.AddObject(prov);

                    Entity.SaveChanges();
                }
            }
        }

        public void deleteElement(object element)
        {
            if (element != null)
            {
                using (var Entity = new TAE2Entities())
                {

                    TIPO_PEDIMENTO Eprov = (TIPO_PEDIMENTO)element;

                    var query = from p in Entity.TIPO_PEDIMENTO
                                where p.UNID_TIPO_PEDIMENTO == Eprov.UNID_TIPO_PEDIMENTO
                                select p;

                    var provs = query.First();

                    if (provs != null)
                    {

                        TIPO_PEDIMENTO aux = (TIPO_PEDIMENTO)provs;

                        Entity.TIPO_PEDIMENTO.DeleteObject(aux);
                    }
                }
            }
        }
    }
}
