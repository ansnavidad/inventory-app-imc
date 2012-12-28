using InventoryApp.DAL.Recibo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using InventoryApp.DAL.POCOS;
using System.Collections.Generic;

namespace DALTestProject
{
    
    
    /// <summary>
    ///Se trata de una clase de prueba para FacturaCompraDetalleDataMapperTest y se pretende que
    ///contenga todas las pruebas unitarias FacturaCompraDetalleDataMapperTest.
    ///</summary>
    [TestClass()]
    public class FacturaCompraDetalleDataMapperTest
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
        ///Una prueba de GetJsonFacturaDetalle
        ///</summary>
        [TestMethod()]
        public void GetJsonFacturaDetalleTest()
        {
            FacturaCompraDetalleDataMapper target = new FacturaCompraDetalleDataMapper(); // TODO: Inicializar en un valor adecuado
            string expected = string.Empty; // TODO: Inicializar en un valor adecuado
            string actual;
            actual = target.GetJsonFacturaDetalle();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }

        /// <summary>
        ///Una prueba de GetDetallebyFactura
        ///</summary>
        [TestMethod()]
        public void GetDetallebyFacturaTest()
        {
            FacturaCompraDetalleDataMapper target = new FacturaCompraDetalleDataMapper(); // TODO: Inicializar en un valor adecuado
            FACTURA factura = new FACTURA(); // TODO: Inicializar en un valor adecuado
            factura.UNID_FACTURA = 20121202234231731;
            List<FACTURA_DETALLE> expected = null; // TODO: Inicializar en un valor adecuado
            List<FACTURA_DETALLE> actual;
            actual = target.GetDetallebyFactura(factura);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }
    }
}
