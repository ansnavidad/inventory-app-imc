using InventoryApp.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using InventoryApp.DAL.POCOS;

namespace DALTestProject
{
    
    
    /// <summary>
    ///Se trata de una clase de prueba para UltimoMovimientoDataMapperTest y se pretende que
    ///contenga todas las pruebas unitarias UltimoMovimientoDataMapperTest.
    ///</summary>
    [TestClass()]
    public class UltimoMovimientoDataMapperTest
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
        ///Una prueba de GetJsonUltimoMovimiento
        ///</summary>
        [TestMethod()]
        public void GetJsonUltimoMovimientoTest()
        {
            UltimoMovimientoDataMapper target = new UltimoMovimientoDataMapper(); // TODO: Inicializar en un valor adecuado
            string expected = string.Empty; // TODO: Inicializar en un valor adecuado
            string actual;
            actual = target.GetJsonUltimoMovimiento();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }

        /// <summary>
        ///Una prueba de getCantidadItems
        ///</summary>




        /// <summary>
        ///Una prueba de getCantidadItems
        ///</summary>
        [TestMethod()]
        public void getCantidadItemsTest()
        {
            UltimoMovimientoDataMapper target = new UltimoMovimientoDataMapper(); // TODO: Inicializar en un valor adecuado
            ITEM item = new ITEM(); // TODO: Inicializar en un valor adecuado
            INFRAESTRUCTURA infraestructura = new INFRAESTRUCTURA(); // TODO: Inicializar en un valor adecuado
            item.UNID_ITEM = 20130103181604156;
            infraestructura.UNID_INFRAESTRUCTURA = 2;
            ULTIMO_MOVIMIENTO expected = new ULTIMO_MOVIMIENTO();
            expected.CANTIDAD = 100;// TODO: Inicializar en un valor adecuado
            ULTIMO_MOVIMIENTO actual;
            actual = target.getCantidadItems(item, infraestructura);
            Assert.AreEqual(expected.CANTIDAD, actual.CANTIDAD);
            
        }
    }
}
