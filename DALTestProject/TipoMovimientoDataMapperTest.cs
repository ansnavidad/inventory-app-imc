using InventoryApp.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using InventoryApp.DAL.POCOS;
using System.Linq;

namespace DALTestProject
{
    
    
    /// <summary>
    ///Se trata de una clase de prueba para TipoMovimientoDataMapperTest y se pretende que
    ///contenga todas las pruebas unitarias TipoMovimientoDataMapperTest.
    ///</summary>
    [TestClass()]
    public class TipoMovimientoDataMapperTest
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
        ///Una prueba de getElements
        ///</summary>
        [TestMethod()]
        public void getElementsTest()
        {
            TipoMovimientoDataMapper target = new TipoMovimientoDataMapper(); // TODO: Inicializar en un valor adecuado
            object actual;
            actual = target.getElements();
            Assert.AreEqual(3, ((List<TIPO_MOVIMIENTO>)actual).Count);
            
        }

        /// <summary>
        ///Una prueba de getElement
        ///</summary>
        [TestMethod()]
        public void getElementTest()
        {
            TIPO_MOVIMIENTO item = new TIPO_MOVIMIENTO() { UNID_TIPO_MOVIMIENTO = 1234764532546, TIPO_MOVIMIENTO_NAME = "tipo 1", SIGNO_MOVIMIENTO = "A" };
            TipoMovimientoDataMapper target = new TipoMovimientoDataMapper(); // TODO: Inicializar en un valor adecuado
            object element =(object)item; // TODO: Inicializar en un valor adecuado
            object actual;
            actual = target.getElement(element);
            Assert.AreEqual(1, ((List<TIPO_MOVIMIENTO>)actual).Count);
            Assert.AreEqual(item.UNID_TIPO_MOVIMIENTO, ((List<TIPO_MOVIMIENTO>)actual)[0].UNID_TIPO_MOVIMIENTO);
            Assert.AreEqual(item.TIPO_MOVIMIENTO_NAME, ((List<TIPO_MOVIMIENTO>)actual)[0].TIPO_MOVIMIENTO_NAME);
            Assert.AreEqual(item.SIGNO_MOVIMIENTO, ((List<TIPO_MOVIMIENTO>)actual)[0].SIGNO_MOVIMIENTO);
            
        }

        /// <summary>
        ///Una prueba de udpateElement
        ///</summary>
        [TestMethod()]
        public void udpateElementTest()
        {
            TIPO_MOVIMIENTO item = new TIPO_MOVIMIENTO() { UNID_TIPO_MOVIMIENTO = 1234764532546, TIPO_MOVIMIENTO_NAME = "tipo prueba", SIGNO_MOVIMIENTO = "p" };
            TipoMovimientoDataMapper target = new TipoMovimientoDataMapper(); // TODO: Inicializar en un valor adecuado
            object element = (object)item; // TODO: Inicializar en un valor adecuado
            object actual;
            target.udpateElement(element);
            actual = target.getElement(element);
            Assert.AreEqual(item.UNID_TIPO_MOVIMIENTO, ((List<TIPO_MOVIMIENTO>)actual)[0].UNID_TIPO_MOVIMIENTO);
            Assert.AreEqual(item.TIPO_MOVIMIENTO_NAME, ((List<TIPO_MOVIMIENTO>)actual)[0].TIPO_MOVIMIENTO_NAME);
            Assert.AreEqual(item.SIGNO_MOVIMIENTO, ((List<TIPO_MOVIMIENTO>)actual)[0].SIGNO_MOVIMIENTO);
            
        }

        /// <summary>
        ///Una prueba de insertElement
        ///</summary>
        [TestMethod()]
        public void insertElementTest()
        {
            TIPO_MOVIMIENTO item = new TIPO_MOVIMIENTO() { TIPO_MOVIMIENTO_NAME = "tipo insert", SIGNO_MOVIMIENTO = "p" };
            TipoMovimientoDataMapper target = new TipoMovimientoDataMapper(); // TODO: Inicializar en un valor adecuado
            object element = (object)item; // TODO: Inicializar en un valor adecuado

            target.insertElement(element);

            var entity = new TAE2Entities();

                   TIPO_MOVIMIENTO ETipo = (TIPO_MOVIMIENTO)element;

                    var query = (from cust in entity.TIPO_MOVIMIENTO
                                 where cust.TIPO_MOVIMIENTO_NAME == ETipo.TIPO_MOVIMIENTO_NAME &&
                                       cust.SIGNO_MOVIMIENTO==ETipo.SIGNO_MOVIMIENTO
                                 select cust).ToList();

            object actual = (object)query;

            Assert.AreEqual(item.TIPO_MOVIMIENTO_NAME, ((List<TIPO_MOVIMIENTO>)actual)[0].TIPO_MOVIMIENTO_NAME);
            Assert.AreEqual(item.SIGNO_MOVIMIENTO, ((List<TIPO_MOVIMIENTO>)actual)[0].SIGNO_MOVIMIENTO);
            
        }

        /// <summary>
        ///Una prueba de GetJsonTipoMovimiento
        ///</summary>
        [TestMethod()]
        public void GetJsonTipoMovimientoTest()
        {
            TipoMovimientoDataMapper target = new TipoMovimientoDataMapper(); // TODO: Inicializar en un valor adecuado
            string expected = string.Empty; // TODO: Inicializar en un valor adecuado
            string actual;
            actual = target.GetJsonTipoMovimiento();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }
    }
}
