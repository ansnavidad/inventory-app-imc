using InventoryApp.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using InventoryApp.DAL.POCOS;

namespace DALTestProject
{
    
    
    /// <summary>
    ///Se trata de una clase de prueba para EquipoDataMapperTest y se pretende que
    ///contenga todas las pruebas unitarias EquipoDataMapperTest.
    ///</summary>
    [TestClass()]
    public class EquipoDataMapperTest
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
            EquipoDataMapper target = new EquipoDataMapper(); // TODO: Inicializar en un valor adecuado
            object element = null;// TODO: Inicializar en un valor adecuado
            EQUIPO equipo = new EQUIPO();
            equipo.EQUIPO_NAME = "iPhone 4S Blanco";
            target.insertElement(equipo);
            element = target.getElements();
            Assert.Inconclusive("Un método que no devuelve ningún valor no se puede comprobar.");
        }

        /// <summary>
        ///Una prueba de GetArticuloEquipoByCategoria
        ///</summary>
        [TestMethod()]
        public void GetArticuloEquipoByCategoriaTest()
        {
            EquipoDataMapper target = new EquipoDataMapper(); // TODO: Inicializar en un valor adecuado
            CATEGORIA categoria = new CATEGORIA() { UNID_CATEGORIA = 20121112172549418 }; // TODO: Inicializar en un valor adecuado
            FixupCollection<EQUIPO> expected = null; // TODO: Inicializar en un valor adecuado
            FixupCollection<EQUIPO> actual;
            actual = target.GetArticuloEquipoByCategoria(categoria);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }
    }
}
