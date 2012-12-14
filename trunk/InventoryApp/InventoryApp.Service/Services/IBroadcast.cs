using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace InventoryApp.Service.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IBroadcast" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IBroadcast
    {
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        long GetServerLast();

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        string downloadCategoria(long lastModifiedDate);
    }
}
