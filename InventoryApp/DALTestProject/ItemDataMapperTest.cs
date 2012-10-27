using InventoryApp.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DALTestProject
{
    
    
    /// <summary>
    ///Se trata de una clase de prueba para ItemDataMapperTest y se pretende que
    ///contenga todas las pruebas unitarias ItemDataMapperTest.
    ///</summary>
    [TestClass()]
    public class ItemDataMapperTest
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
        ///Una prueba de getItems
        ///</summary>
        [TestMethod()]
        public void getItemsTest()
        {
            ItemDataMapper target = new ItemDataMapper(); // TODO: Inicializar en un valor adecuado
            
            Item item = new Item(new Articulo(3, "", 0, "", new Categoria(3, "")), "", "", 0, 0);  // TODO: Inicializar en un valor adecuado
            
            ItemCollection actual;
            actual = target.getItems(item);
            Assert.AreEqual(6, actual.Count);
        }

        /// <summary>
        ///Una prueba de insertItems
        ///</summary>
        [TestMethod()]
        public void insertItemsTest()
        {
            ItemDataMapper target = new ItemDataMapper(); // TODO: Inicializar en un valor adecuado
            Item item = null; // TODO: Inicializar en un valor adecuado
            target.insertItems(item);
            Assert.Inconclusive("Un método que no devuelve ningún valor no se puede comprobar.");
        }


        /// <summary>
        ///Una prueba de getItems
        ///</summary>
        [TestMethod()]
        public void getItemsTest1()
        {
            ItemDataMapper target = new ItemDataMapper(); // TODO: Inicializar en un valor adecuado
            Articulo articulo = new Articulo(3, "", 0, "", new Categoria(3, "")); // TODO: Inicializar en un valor adecuado            
            ItemCollection actual;
            actual = target.getItems(articulo);
            Assert.AreEqual(6, actual.Count);
            Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }

        /// <summary>
        ///Una prueba de updateItems
        ///</summary>
        [TestMethod()]
        public void updateItemsTest()
        {
            ItemDataMapper target = new ItemDataMapper(); // TODO: Inicializar en un valor adecuado
            Item item = new Item(new Articulo(3, "", 0, "", new Categoria(3, "")), "", "", 0, 0);
            target.updateItems(item);
            Assert.Inconclusive("Un método que no devuelve ningún valor no se puede comprobar.");
        }
    }
}
