using InventoryApp.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using InventoryApp.DAL.POCOS;
using System.Linq;

namespace DALTestProject
{
    
    
    /// <summary>
    ///Se trata de una clase de prueba para ProyectoDataMapperTest y se pretende que
    ///contenga todas las pruebas unitarias ProyectoDataMapperTest.
    ///</summary>
    [TestClass()]
    public class ProyectoDataMapperTest
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
            ProyectoDataMapper target = new ProyectoDataMapper(); // TODO: Inicializar en un valor adecuado
            object actual;
            actual = target.getElements();
            Assert.AreEqual(2,((List<PROYECTO>)actual).Count);
            //Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }

        /// <summary>
        ///Una prueba de getElement
        ///</summary>
        [TestMethod()]
        public void getElementTest()
        {
            PROYECTO item = new PROYECTO() { UNID_PROYECTO = 12346545354, PROYECTO_NAME="PROYECTO 1" };
            ProyectoDataMapper target = new ProyectoDataMapper(); // TODO: Inicializar en un valor adecuado
            object element = (object)item; // TODO: Inicializar en un valor adecuado
            object actual;
            actual = target.getElement(element);
            Assert.AreEqual(1,((List<PROYECTO>)actual).Count);
            Assert.AreEqual(item.UNID_PROYECTO, ((List<PROYECTO>)actual)[0].UNID_PROYECTO);
            Assert.AreEqual(item.PROYECTO_NAME, ((List<PROYECTO>)actual)[0].PROYECTO_NAME);
            
        }

        /// <summary>
        ///Una prueba de udpateElement
        ///</summary>
        [TestMethod()]
        public void udpateElementTest()
        {
            PROYECTO item = new PROYECTO() { UNID_PROYECTO = 12346545354, PROYECTO_NAME = "PROYECTO PRUEBA" };
            ProyectoDataMapper target = new ProyectoDataMapper(); // TODO: Inicializar en un valor adecuado
            object element = (object)item; // TODO: Inicializar en un valor adecuado
            object actual;
            target.udpateElement(element);
            actual = target.getElement(element);
            Assert.AreEqual(item.UNID_PROYECTO, ((List<PROYECTO>)actual)[0].UNID_PROYECTO);
            Assert.AreEqual(item.PROYECTO_NAME, ((List<PROYECTO>)actual)[0].PROYECTO_NAME);

        }

        /// <summary>
        ///Una prueba de insertElement
        ///</summary>
        [TestMethod()]
        public void insertElementTest()
        {
            PROYECTO item = new PROYECTO() { PROYECTO_NAME = "PROYECTO PRUEBA insert 2" };
            ProyectoDataMapper target = new ProyectoDataMapper(); // TODO: Inicializar en un valor adecuado
            object element = (object)item; // TODO: Inicializar en un valor adecuado
            target.insertElement(element);

            var entity = new TAE2Entities();

            PROYECTO EPro = (PROYECTO)element;

            var query = (from cust in entity.PROYECTOes
                         where cust.PROYECTO_NAME == EPro.PROYECTO_NAME
                         select cust).ToList();

            object actual = (object)query;

            Assert.AreEqual(item.PROYECTO_NAME, ((List<PROYECTO>)actual)[0].PROYECTO_NAME);
        }
    }
}
