using InventoryApp.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using InventoryApp.DAL.POCOS;
using System.Collections.Generic;
using System.Linq;
namespace DALTestProject
{
    
    
    /// <summary>
    ///Se trata de una clase de prueba para EstatusDataMapperTest y se pretende que
    ///contenga todas las pruebas unitarias EstatusDataMapperTest.
    ///</summary>
    [TestClass()]
    public class EstatusDataMapperTest
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
            ItemStatusDataMapper target = new ItemStatusDataMapper(); // TODO: Inicializar en un valor adecuado
            //object expected = null; // TODO: Inicializar en un valor adecuado
            object actual;
            actual = target.getElements();
            Assert.AreEqual(5, ((FixupCollection<ITEM_STATUS>)actual).Count);
            //Assert.AreEqual(3,((List<ITEM_STATUS>)actual).Count);
            //Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }

        /// <summary>
        ///Una prueba de getElement
        ///</summary>
        [TestMethod()]
        public void getElementTest()
        {
            ITEM_STATUS item = new ITEM_STATUS() { ITEM_STATUS_NAME = "RECHAZADO", UNID_ITEM_STATUS = 87623567645 };
            ItemStatusDataMapper target = new ItemStatusDataMapper(); // TODO: Inicializar en un valor adecuado
            object element = (object)item;  // TODO: Inicializar en un valor adecuado
            //object expected = 1; // TODO: Inicializar en un valor adecuado
            object actual;
            actual = target.getElement(element);
            Assert.AreEqual(1, ((List<ITEM_STATUS>)actual).Count);
            Assert.AreEqual(item.UNID_ITEM_STATUS, ((List<ITEM_STATUS>)actual)[0].UNID_ITEM_STATUS);
            Assert.AreEqual(item.ITEM_STATUS_NAME, ((List<ITEM_STATUS>)actual)[0].ITEM_STATUS_NAME);

        }

        /// <summary>
        ///Una prueba de insertElement
        ///</summary>
        [TestMethod()]
        public void insertElementTest()
        {
            ITEM_STATUS item = new ITEM_STATUS() { ITEM_STATUS_NAME = "borrado insert "};

            ItemStatusDataMapper target = new ItemStatusDataMapper(); // TODO: Inicializar en un valor adecuado
            object element = (object)item; // TODO: Inicializar en un valor adecuado
            target.insertElement(element);

            var entity = new TAE2Entities();

            ITEM_STATUS EEst = (ITEM_STATUS)element;

            var query = (from cust in entity.ITEM_STATUS
                         where cust.ITEM_STATUS_NAME == EEst.ITEM_STATUS_NAME
                         select cust).ToList();

            object actual = (object)query;

            Assert.AreEqual(item.ITEM_STATUS_NAME, ((List<ITEM_STATUS>)actual)[0].ITEM_STATUS_NAME);
        }



        /// <summary>
        ///Una prueba de GetJsonItemStatus
        ///</summary>
        [TestMethod()]
        public void GetJsonItemStatusTest()
        {
            ItemStatusDataMapper target = new ItemStatusDataMapper(); // TODO: Inicializar en un valor adecuado
            string expected = string.Empty; // TODO: Inicializar en un valor adecuado
            string actual;
            actual = target.GetJsonItemStatus();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }
    }
}
