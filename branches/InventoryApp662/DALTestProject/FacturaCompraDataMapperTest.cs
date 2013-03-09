using InventoryApp.DAL.Recibo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using InventoryApp.DAL.POCOS;
using System.Collections.Generic;

namespace DALTestProject
{
    
    
    /// <summary>
    ///Se trata de una clase de prueba para FacturaCompraDataMapperTest y se pretende que
    ///contenga todas las pruebas unitarias FacturaCompraDataMapperTest.
    ///</summary>
    [TestClass()]
    public class FacturaCompraDataMapperTest
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
        ///Una prueba de GetFacturaList
        ///</summary>
        [TestMethod()]
        public void GetFacturaListTest()
        {
            FacturaCompraDataMapper target = new FacturaCompraDataMapper(); // TODO: Inicializar en un valor adecuado
            RECIBO recibo = null; // TODO: Inicializar en un valor adecuado
            List<FACTURA> expected = null; // TODO: Inicializar en un valor adecuado
            List<FACTURA> actual;
            actual = target.GetFacturaList(new RECIBO() { UNID_RECIBO = 20121203012919716 });
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }

        /// <summary>
        ///Una prueba de GetJsonFactura
        ///</summary>
        [TestMethod()]
        public void GetJsonFacturaTest()
        {
            FacturaCompraDataMapper target = new FacturaCompraDataMapper(); // TODO: Inicializar en un valor adecuado
            string expected = string.Empty; // TODO: Inicializar en un valor adecuado
            string actual;
            actual = target.GetJsonFactura();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }

        /// <summary>
        ///Una prueba de getFacturabyProveedores
        ///</summary>
        [TestMethod()]
        public void getFacturabyProveedoresTest()
        {
            FacturaCompraDataMapper target = new FacturaCompraDataMapper(); // TODO: Inicializar en un valor adecuado
            List<PROVEEDOR> proveedores = new List<PROVEEDOR>(); // TODO: Inicializar en un valor adecuado
            PROVEEDOR prov = new PROVEEDOR();
            prov.UNID_PROVEEDOR = 20121128165223029;
            proveedores.Add(prov);
            List<FACTURA> expected = null; // TODO: Inicializar en un valor adecuado
            List<FACTURA> actual;
            actual = target.getFacturabyProveedores(proveedores);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }
    }
}
