using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public class TerminoEnvioDataMapper : IDataMapper
    {
        public object getElements()
        {
            object o = null;
            FixupCollection<TERMINO_ENVIO> tp = new FixupCollection<TERMINO_ENVIO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.TERMINO_ENVIO
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
                TERMINO_ENVIO Eprov = (TERMINO_ENVIO)element;
                FixupCollection<TERMINO_ENVIO> tp = new FixupCollection<TERMINO_ENVIO>();

                using (var Entity = new TAE2Entities())
                {
                    (from p in Entity.TERMINO_ENVIO
                     where p.UNID_TERMINO_ENVIO == Eprov.UNID_TERMINO_ENVIO
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
                    TERMINO_ENVIO terminoEnvio = (TERMINO_ENVIO)element;

                    var modifiedTerminoEnvio = entity.TERMINO_ENVIO.First(p => p.UNID_TERMINO_ENVIO == terminoEnvio.UNID_TERMINO_ENVIO);
                    modifiedTerminoEnvio.CLAVE = terminoEnvio.CLAVE;
                    modifiedTerminoEnvio.GENERA_LOTES = terminoEnvio.GENERA_LOTES;
                    modifiedTerminoEnvio.SIGNIFICADO = terminoEnvio.SIGNIFICADO;
                    modifiedTerminoEnvio.TERMINO = terminoEnvio.TERMINO;
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
                    TERMINO_ENVIO terminoEnvio = (TERMINO_ENVIO)element;

                    terminoEnvio.UNID_TERMINO_ENVIO = UNID.getNewUNID();

                    entity.TERMINO_ENVIO.AddObject(terminoEnvio);
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
