using InventoryApp.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using InventoryApp.DAL.POCOS;
using InventoryApp.Model;
using System.Collections.Generic;

namespace DALTestProject
{
    
    
    /// <summary>
    ///Se trata de una clase de prueba para TransporteDataMapperTest y se pretende que
    ///contenga todas las pruebas unitarias TransporteDataMapperTest.
    ///</summary>
    [TestClass()]
    public class TransporteDataMapperTest
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
            TransporteDataMapper target = new TransporteDataMapper(); // TODO: Inicializar en un valor adecuado
            object expected = null; // TODO: Inicializar en un valor adecuado
            object actual;
            actual = target.getElements();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }

        /// <summary>
        ///Una prueba de getElement
        ///</summary>
        [TestMethod()]
        public void getElementTest()
        {
            TransporteDataMapper target = new TransporteDataMapper(); // TODO: Inicializar en un valor adecuado
            object element = null; // TODO: Inicializar en un valor adecuado
            object expected = null; // TODO: Inicializar en un valor adecuado
            object actual;
            actual = target.getElement(element);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }

        /// <summary>
        ///Una prueba de getElements
        ///</summary>
        [TestMethod()]
        public void getElementsTest1()
        {
            TransporteDataMapper target = new TransporteDataMapper(); // TODO: Inicializar en un valor adecuado
            //object expected = null; // TODO: Inicializar en un valor adecuado
            object actual;
            actual = target.getElements();
            Assert.AreEqual(4, ((List<TRANSPORTE>)actual).Count);
           // Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }

        /// <summary>
        ///Una prueba de getElement
        ///</summary>
        [TestMethod()]
        public void getElementTest1()
        {
            TransporteDataMapper target = new TransporteDataMapper(); // TODO: Inicializar en un valor adecuado
            object element = null; // TODO: Inicializar en un valor adecuado
            TRANSPORTE prueba = new TRANSPORTE();
            prueba.UNID_TIPO_EMPRESA = 1;
            prueba.UNID_TRANSPORTE= 2;
            element = prueba;
            //object expected = null; // TODO: Inicializar en un valor adecuado
            object actual;
            actual = target.getElement(element);
            Assert.AreEqual(1, ((List<TRANSPORTE>)actual).Count);
            //Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }

        /// <summary>
        ///Una prueba de insertElement
        ///</summary>
        [TestMethod()]
        public void insertElementTest()
        {
            TransporteDataMapper target = new TransporteDataMapper(); // TODO: Inicializar en un valor adecuado
            object element = null; // TODO: Inicializar en un valor adecuado
            TRANSPORTE prueba = new TRANSPORTE();
            prueba.UNID_TIPO_EMPRESA = 1;
            prueba.UNID_TRANSPORTE = 22345;
            prueba.TRANSPORTE_NAME = "TREN";
            element = prueba;
            target.insertElement(element);
           
            object actual;
            actual = target.getElement(element);

            Assert.AreEqual(((TRANSPORTE)element).UNID_TRANSPORTE, ((List<TRANSPORTE>)actual)[0].UNID_TRANSPORTE);
            //Assert.Inconclusive("Un método que no devuelve ningún valor no se puede comprobar.");
        }

        /// <summary>
        ///Una prueba de udpateElement
        ///</summary>
        [TestMethod()]
        public void udpateElementTest()
        {
            TransporteDataMapper target = new TransporteDataMapper(); // TODO: Inicializar en un valor adecuado
            object element = null; // TODO: Inicializar en un valor adecuado
            TRANSPORTE prueba = new TRANSPORTE();
            prueba.UNID_TIPO_EMPRESA = 1;
            prueba.UNID_TRANSPORTE = 22345;
            prueba.TRANSPORTE_NAME = "TRANVIA";
            element = prueba;
            target.udpateElement(element);
            object actual;
            actual = target.getElement(element);

            Assert.AreEqual(((TRANSPORTE)element).UNID_TRANSPORTE, ((List<TRANSPORTE>)actual)[0].UNID_TRANSPORTE);
          
        }

        /// <summary>
        ///Una prueba de GetJsonTransporte
        ///</summary>
        [TestMethod()]
        public void GetJsonTransporteTest()
        {
            TransporteDataMapper target = new TransporteDataMapper(); // TODO: Inicializar en un valor adecuado
            string expected = string.Empty; // TODO: Inicializar en un valor adecuado
            string actual;
            actual = target.GetJsonTransporte();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }
    }
}
