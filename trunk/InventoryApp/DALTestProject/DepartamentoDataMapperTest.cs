using InventoryApp.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DALTestProject
{
    
    
    /// <summary>
    ///Se trata de una clase de prueba para DepartamentoDataMapperTest y se pretende que
    ///contenga todas las pruebas unitarias DepartamentoDataMapperTest.
    ///</summary>
    [TestClass()]
    public class DepartamentoDataMapperTest
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
        ///Una prueba de insertElement
        ///</summary>
        [TestMethod()]
        public void insertElementTest()
        {
            DepartamentoDataMapper target = new DepartamentoDataMapper(); // TODO: Inicializar en un valor adecuado
            object element = new InventoryApp.DAL.POCOS.DEPARTAMENTO() { 
                UNID_DEPARTAMENTO = 12345
                ,DEPARTAMENTO_NAME = "Finanzas"
            }; // TODO: Inicializar en un valor adecuado
            target.insertElement(element);
            Assert.Inconclusive("Un método que no devuelve ningún valor no se puede comprobar.");
        }


        /// <summary>
        ///Una prueba de getElement
        ///</summary>
        [TestMethod()]
        public void getElementTest()
        {
            DepartamentoDataMapper target = new DepartamentoDataMapper(); // TODO: Inicializar en un valor adecuado
            object element = null; // TODO: Inicializar en un valor adecuado
            object expected = null; // TODO: Inicializar en un valor adecuado
            object actual;
            actual = target.getElement(element);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }

        /// <summary>
        ///Una prueba de GetJsonDepartamento
        ///</summary>
        [TestMethod()]
        public void GetJsonDepartamentoTest()
        {
            DepartamentoDataMapper target = new DepartamentoDataMapper(); // TODO: Inicializar en un valor adecuado
            string expected = string.Empty; // TODO: Inicializar en un valor adecuado
            string actual;
            actual = target.GetJsonDepartamento();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }
    }
}
