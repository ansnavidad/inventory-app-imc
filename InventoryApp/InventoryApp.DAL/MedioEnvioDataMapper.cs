using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public class MedioEnvioDataMapper :  IDataMapper
    {
        public object getElements()
        {
            object o = null;
            FixupCollection<MEDIO_ENVIO> tp = new FixupCollection<MEDIO_ENVIO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.MEDIO_ENVIO
                 select p).ToList().ForEach(d => { tp.Add(d); });

                if (tp.Count > 0)
                {                    
                    o = (object)tp;
                }

                return o;
            }
        }

        public object getElement(object element)
        {
            object o = null;
            if (element != null)
            {
                MEDIO_ENVIO Eprov = (MEDIO_ENVIO)element;
                FixupCollection<MEDIO_ENVIO> tp = new FixupCollection<MEDIO_ENVIO>();

                using (var Entity = new TAE2Entities())
                {
                    (from p in Entity.MEDIO_ENVIO
                     where p.UNID_MEDIO_ENVIO == Eprov.UNID_MEDIO_ENVIO
                     select p).ToList().ForEach(d => { tp.Add(d); });

                    if (tp.Count > 0)
                    {
                        o = (object)tp;
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
                    MEDIO_ENVIO medioEnvio = (MEDIO_ENVIO)element;

                    var modifiedMedioEnvio = entity.MEDIO_ENVIO.First(p => p.UNID_MEDIO_ENVIO == medioEnvio.UNID_MEDIO_ENVIO);
                    modifiedMedioEnvio.MEDIO_ENVIO_NAME = medioEnvio.MEDIO_ENVIO_NAME;
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
                    MEDIO_ENVIO medioEnvio = (MEDIO_ENVIO)element;

                    medioEnvio.UNID_MEDIO_ENVIO = UNID.getNewUNID();

                    entity.MEDIO_ENVIO.AddObject(medioEnvio);
                    entity.SaveChanges();
                }
            }
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}
