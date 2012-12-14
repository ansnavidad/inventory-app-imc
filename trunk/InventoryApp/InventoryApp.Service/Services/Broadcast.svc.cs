using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using InventoryApp.DAL;
using System.ServiceModel.Activation;

namespace InventoryApp.Service.Services
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    [DataContractFormat]
    public class Broadcast : IBroadcast
    {
        public long GetServerLast()
        {
            long mensaje = 0;
            ServerLastDataMapper server = new ServerLastDataMapper();

            if (server.GetServerLastFecha() != 0)
            {
                mensaje = server.GetServerLastFecha();
            }
            return mensaje;
        }

        public string downloadCategoria(long lastModifiedDate)
        {
            string respuesta = null;
            CategoriaDataMapper dataMapper = new CategoriaDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonCategoria(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonCategoria(lastModifiedDate);
            }
            return respuesta;
        }
    }
}
