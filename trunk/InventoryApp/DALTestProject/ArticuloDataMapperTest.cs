using InventoryApp.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DALTestProject
{
    
    
    /// <summary>
    ///Se trata de una clase de prueba para ArticuloDataMapperTest y se pretende que
    ///contenga todas las pruebas unitarias ArticuloDataMapperTest.
    ///</summary>
    [TestClass()]
    public class ArticuloDataMapperTest
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


        /// <summary |   
        ///Una prueba de GetArticulos
        ///</summary>
        [TestMethod()]
        public void GetArticulosTest() 
        {
            ArticuloDataMapper target = new ArticuloDataMapper(); // TODO: Inicializar en un valor adecuado
            Categoria categoria = new Categoria(4, "COMPUTO"); // TODO: Inicializar en un valor adecuado
            //ArticuloCollection expected = null; // TODO: Inicializar en un valor adecuado
            ArticuloCollection actual;
            actual = target.GetArticulos(categoria);
            Assert.AreEqual(2, actual.Count);
          //  Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }

        /// <summary>
        ///Una prueba de GetArticulos
        ///</summary>
        [TestMethod()]
        public void TestGetArticulosByNonExistingCategory()
        {
            ArticuloDataMapper target = new ArticuloDataMapper(); // TODO: Inicializar en un valor adecuado
            Categoria categoria = new Categoria(0, null); // TODO: Inicializar en un valor adecuado
            //ArticuloCollection expected = null; // TODO: Inicializar en un valor adecuado
            ArticuloCollection actual;
            actual = target.GetArticulos(categoria);
            Assert.AreEqual(0, actual.Count);
            //  Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }
    }
}
