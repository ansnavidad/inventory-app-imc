using InventoryApp.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DALTestProject
{
    
    
    /// <summary>
    ///Se trata de una clase de prueba para SolicitanteDataMapperTest y se pretende que
    ///contenga todas las pruebas unitarias SolicitanteDataMapperTest.
    ///</summary>
    [TestClass()]
    public class SolicitanteDataMapperTest
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


        ///// <summary>
        /////Una prueba de insertElement
        /////</summary>
        //[TestMethod()]
        //public void insertElementTest()
        //{
        //    SolicitanteDataMapper target = new SolicitanteDataMapper(); // TODO: Inicializar en un valor adecuado
        //    object element = new InventoryApp.DAL.POCOS.SOLICITANTE() 
        //    { 
        //        UNID_SOLICITANTE = 12345 
        //        ,SOLICITANTE_NAME = "Rafa"
        //        ,UNID_EMPRESA = 12345
        //        ,UNID_DEPARTAMENTO = 12345
        //        ,EMAIL = "rafaelgp87@gmail.com"
        //        ,VALIDADOR="Test" 
        //    }; // TODO: Inicializar en un valor adecuado
        //    target.insertElement(element);
        //    Assert.Inconclusive("Un método que no devuelve ningún valor no se puede comprobar.");
        //}

        ///// <summary>
        /////Una prueba de getElements
        /////</summary>
        //[TestMethod()]
        //public void getElementsTest()
        //{
        //    SolicitanteDataMapper target = new SolicitanteDataMapper(); // TODO: Inicializar en un valor adecuado
        //    object expected = null; // TODO: Inicializar en un valor adecuado
        //    object actual;
        //    actual = target.getElements();
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        //}

        ///// <summary>
        /////Una prueba de getElement
        /////</summary>
        //[TestMethod()]
        //public void getElementTest()
        //{
        //    SolicitanteDataMapper target = new SolicitanteDataMapper(); // TODO: Inicializar en un valor adecuado
        //    object element = null; // TODO: Inicializar en un valor adecuado
        //    object expected = null; // TODO: Inicializar en un valor adecuado
        //    object actual;
        //    actual = target.getElement(element);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        //}

        /// <summary>
        ///Una prueba de getElements
        ///</summary>
        [TestMethod()]
        public void getElementsTest()
        {
            SolicitanteDataMapper target = new SolicitanteDataMapper(); // TODO: Inicializar en un valor adecuado
            object expected = null; // TODO: Inicializar en un valor adecuado
            object actual;
            actual = target.getElements();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }
    }
}
