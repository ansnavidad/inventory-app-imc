using InventoryApp.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using InventoryApp.DAL.POCOS;

namespace DALTestProject
{
    
    
    /// <summary>
    ///Se trata de una clase de prueba para MarcaDataMapperTest y se pretende que
    ///contenga todas las pruebas unitarias MarcaDataMapperTest.
    ///</summary>
    [TestClass()]
    public class MarcaDataMapperTest
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
        ///Una prueba de insertElement
        ///</summary>
        [TestMethod()]
        public void insertElementTest()
        {
            MarcaDataMapper target = new MarcaDataMapper(); // TODO: Inicializar en un valor adecuado
            object element = null; // TODO: Inicializar en un valor adecuado
            MARCA marca = new MARCA();
            marca.MARCA_NAME = "Apple";
            target.insertElement(marca);
            element = target.getElements();
            Assert.Inconclusive("Un método que no devuelve ningún valor no se puede comprobar.");
        }

        /// <summary>
        ///Una prueba de GetJsonMarca
        ///</summary>
        [TestMethod()]
        public void GetJsonMarcaTest()
        {
            MarcaDataMapper target = new MarcaDataMapper(); // TODO: Inicializar en un valor adecuado
            string expected = string.Empty; // TODO: Inicializar en un valor adecuado
            string actual;
            actual = target.GetJsonMarca();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }
    }
}
