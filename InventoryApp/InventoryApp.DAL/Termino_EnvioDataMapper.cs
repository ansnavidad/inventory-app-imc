using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.DAL 
{
    class Termino_EnvioDataMapper : IDataMapper
    {
        public object getElements()
        {
            object o = null;
            using (var oAWEntities = new TAE2Entities())
            {
                var provs = from p in oAWEntities.TERMINO_ENVIO
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
                TERMINO_ENVIO Eprov = (TERMINO_ENVIO)element;

                using (var oAWEntities = new TAE2Entities())
                {
                    var provs = from p in oAWEntities.TERMINO_ENVIO
                                where p.UNID_TERMINO_ENVIO == Eprov.UNID_TERMINO_ENVIO
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
                TERMINO_ENVIO Eprov = (TERMINO_ENVIO)element;

                using (var oAWEntities = new TAE2Entities())
                {
                    var query = from p in oAWEntities.TERMINO_ENVIO
                                where p.UNID_TERMINO_ENVIO == Eprov.UNID_TERMINO_ENVIO
                                select p;

                    var prov = query.First();

                    prov.CLAVE = Eprov.CLAVE;                    
                    prov.GENERA_LOTES = Eprov.GENERA_LOTES;
                    prov.SIGNIFICADO = Eprov.SIGNIFICADO;
                    prov.TERMINO = Eprov.TERMINO;

                    oAWEntities.SaveChanges();
                }
            }
        }

        public void insertElement(object element)
        {
            if (element != null)
            {

                using (var oAWEntities = new TAE2Entities())
                {

                    TERMINO_ENVIO Eprov = (TERMINO_ENVIO)element;
                    TERMINO_ENVIO prov = new TERMINO_ENVIO();

                    prov.UNID_TERMINO_ENVIO = UNID.getNewUNID();
                    prov.CLAVE = Eprov.CLAVE;                    
                    prov.GENERA_LOTES = Eprov.GENERA_LOTES;
                    prov.SIGNIFICADO = Eprov.SIGNIFICADO;
                    prov.TERMINO = Eprov.TERMINO;

                    oAWEntities.TERMINO_ENVIO.AddObject(prov);

                    oAWEntities.SaveChanges();
                }
            }
        }

        public void deleteElement(object element)
        {
            if (element != null)
            {
                using (var oAWEntities = new TAE2Entities())
                {

                    MEDIO_ENVIO Eprov = (MEDIO_ENVIO)element;

                    var query = from p in oAWEntities.MEDIO_ENVIO
                                where p.UNID_MEDIO_ENVIO == Eprov.UNID_MEDIO_ENVIO
                                select p;

                    var provs = query.First();

                    if (provs != null)
                    {

                        MEDIO_ENVIO aux = (MEDIO_ENVIO)provs;

                        oAWEntities.MEDIO_ENVIO.DeleteObject(aux);
                    }
                }
            }
        }
    }
}
