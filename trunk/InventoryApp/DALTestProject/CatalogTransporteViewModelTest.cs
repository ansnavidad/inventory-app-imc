using InventoryApp.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using InventoryApp.Model;

namespace DALTestProject
{
    
    
    /// <summary>
    ///Se trata de una clase de prueba para CatalogTransporteViewModelTest y se pretende que
    ///contenga todas las pruebas unitarias CatalogTransporteViewModelTest.
    ///</summary>
    [TestClass()]
    public class CatalogTransporteViewModelTest
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
        ///Una prueba de CatalogTransporteModel
        ///</summary>
        [TestMethod()]
        public void CatalogTransporteModelTest()
        {
            CatalogTransporteViewModel target = new CatalogTransporteViewModel(); // TODO: Inicializar en un valor adecuado
            CatalogTransporteModel expected = null; // TODO: Inicializar en un valor adecuado
            CatalogTransporteModel actual;
            target.CatalogTransporteModel = expected;
            actual = target.CatalogTransporteModel;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }

        /// <summary>
        ///Una prueba de Constructor CatalogTransporteViewModel
        ///</summary>
        [TestMethod()]
        public void CatalogTransporteViewModelConstructorTest()
        {
            CatalogTransporteViewModel target = new CatalogTransporteViewModel();
            Assert.Inconclusive("TODO: Implementar código para comprobar el destino");
        }
    }
}
