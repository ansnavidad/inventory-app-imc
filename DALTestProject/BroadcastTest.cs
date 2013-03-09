using InventoryApp.Service.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;

namespace DALTestProject
{
    
    
    /// <summary>
    ///Se trata de una clase de prueba para BroadcastTest y se pretende que
    ///contenga todas las pruebas unitarias BroadcastTest.
    ///</summary>
    [TestClass()]
    public class BroadcastTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Obtiene o establece el contexto de la prueba que proporciona
        ///la información y funcionalidad para la ejecución de pruebas actual.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Atributos de prueba adicionales
        // 
        //Puede utilizar los siguientes atributos adicionales mientras escribe sus pruebas:
        //
        //Use ClassInitialize para ejecutar código antes de ejecutar la primera prueba en la clase 
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup para ejecutar código después de haber ejecutado todas las pruebas en una clase
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize para ejecutar código antes de ejecutar cada prueba
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup para ejecutar código después de que se hayan ejecutado todas las pruebas
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///Una prueba de GetValidateNotExitProcessRunning
        ///</summary>
        // TODO: Asegúrese de que el atributo UrlToTest especifica una dirección URL de una página ASP.NET (por ejemplo, 
        // http://.../Default.aspx). Esto es necesario para ejecutar la prueba unitaria en el servidor web,
        // si va a probar una página, un servicio web o un servicio WCF.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\ISAAC\\Documents\\Visual Studio 2010\\Projects\\InventoryApp(6)\\InventoryApp\\InventoryApp.Service", "/")]
        [UrlToTest("http://192.168.0.116:2020/Services/Broadcast.svc")]
        public void GetValidateNotExitProcessRunningTest()
        {
            Broadcast target = new Broadcast(); // TODO: Inicializar en un valor adecuado
            bool expected = false; // TODO: Inicializar en un valor adecuado
            bool actual;
            actual = target.GetValidateNotExitProcessRunning();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }
    }
}
