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


        ///// <summary>
        /////Una prueba de getItems
        /////</summary>
        //[TestMethod()]
        //public void getItemsTest()
        //{
        //    ItemDataMapper target = new ItemDataMapper(); // TODO: Inicializar en un valor adecuado

        //    Item item = null;//new Item(new Articulo(3, "", 0, "", new Categoria(3, "")), "", "", 0, 0);  // TODO: Inicializar en un valor adecuado
            
        //    ItemCollection actual;
        //    actual = target.getItems(item);
        //    Assert.AreEqual(6, actual.Count);
        //}

        /// <summary>
        ///Una prueba de insertItems
        ///</summary>
        //[TestMethod()]
        //public void insertItemsTest()
        //{
        //    ItemDataMapper target = new ItemDataMapper(); // TODO: Inicializar en un valor adecuado
        //    Articulo art = new Articulo(1, "LAPTOP", 20.7f, "NEGRO", new Categoria(4, "COMPUTO"));
        //    Item expected = new Item(art, 7, "1234", "4321", 11000f, 16.0f);
        //    target.insertItems(expected);
        //}


        ///// <summary>
        /////Una prueba de getItems
        /////</summary>
        //[TestMethod()]
        //public void getItemsTest1()
        //{
        //    ItemDataMapper target = new ItemDataMapper(); // TODO: Inicializar en un valor adecuado
        //    Articulo articulo = new Articulo(3, "", 0, "", new Categoria(3, "")); // TODO: Inicializar en un valor adecuado            
        //    ItemCollection actual;
        //    actual = target.getItems(articulo);
        //    Assert.AreEqual(6, actual.Count);
        //    Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        //}

        /// <summary>
        ///Una prueba de updateItems
        ///</summary>
        //[TestMethod()]
        //public void updateItemsTest()
        //{
        //    ItemDataMapper target = new ItemDataMapper(); // TODO: Inicializar en un valor adecuado
        //    Articulo art=new Articulo(3,"IPHONE",0.8f,"BLANCO",new Categoria(3,"TELEFONIA"));
        //    Item expected = new Item(art, 6, "777", "777", 777f, 777f);
        //    target.updateItems(expected);

        //    Item actual = target.getItems(expected);
        //    Assert.AreEqual(expected.Sku,actual.Sku);
        //    Assert.AreEqual(expected.SerialNbr, actual.SerialNbr);
        //    Assert.AreEqual(expected.Precio, actual.Precio);
        //    Assert.AreEqual(expected.Impuesto, actual.Impuesto);
            
        //}

        /// <summary>
        ///Una prueba de getItems
        ///</summary>
        //[TestMethod()]
        //public void TestGetSingleItem()
        //{
        //    ItemDataMapper target = new ItemDataMapper(); // TODO: Inicializar en un valor adecuado

        //    Articulo artExpected = new Articulo(3, "IPHONE", 0.8f, "BLANCO", new Categoria(3, "TELEFONIA"));
        //    Item expected = new Item(artExpected, 6, "666", "92884933", 666f, 666f);

        //    Item actual = null; // TODO: Inicializar en un valor adecuado
        //    Articulo artActual = new Articulo(3, "IPHONE", 0.8f, "BLANCO", new Categoria(3, "TELEFONIA"));
        //    Item item = new Item(artActual, 6, "", "", 0f, 0f);
        //    actual = target.getItems(item);

        //    Assert.AreEqual(expected.SerialNbr, actual.SerialNbr);
        //    //Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        //}
    }
}
