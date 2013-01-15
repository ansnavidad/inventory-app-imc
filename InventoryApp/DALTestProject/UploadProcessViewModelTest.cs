using InventoryApp.ViewModel.Sync;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DALTestProject
{
    
    
    /// <summary>
    ///Se trata de una clase de prueba para UploadProcessViewModelTest y se pretende que
    ///contenga todas las pruebas unitarias UploadProcessViewModelTest.
    ///</summary>
    [TestClass()]
    public class UploadProcessViewModelTest
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
        ///Una prueba de UploadData
        ///</summary>
        [TestMethod()]
        public void UploadDataTest()
        {
            UploadProcessViewModel target = new UploadProcessViewModel(); // TODO: Inicializar en un valor adecuado
            target.UploadData();
            Assert.Inconclusive("Un método que no devuelve ningún valor no se puede comprobar.");
        }

        /// <summary>
        ///Una prueba de CallServiceMovimiento
        ///</summary>
        [TestMethod()]
        public void CallServiceMovimientoTest()
        {
            UploadProcessViewModel target = new UploadProcessViewModel(); // TODO: Inicializar en un valor adecuado
            bool expected = false; // TODO: Inicializar en un valor adecuado
            bool actual;
            actual = target.CallServiceMovimiento();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }

        /// <summary>
        ///Una prueba de CallDownloadServiceMaxMin
        ///</summary>
        [TestMethod()]
        public void CallDownloadServiceMaxMinTest()
        {
            UploadProcessViewModel target = new UploadProcessViewModel(); // TODO: Inicializar en un valor adecuado
            long serverDate = 0; // TODO: Inicializar en un valor adecuado
            bool expected = false; // TODO: Inicializar en un valor adecuado
            bool actual;
            actual = target.CallDownloadServiceMaxMin(serverDate);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }
    }
}
