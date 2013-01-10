using InventoryApp.DAL.JSON;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using InventoryApp.DAL.POCOS;

namespace DALTestProject
{
    
    
    /// <summary>
    ///Se trata de una clase de prueba para SerializerJsonTest y se pretende que
    ///contenga todas las pruebas unitarias SerializerJsonTest.
    ///</summary>
    [TestClass()]
    public class SerializerJsonTest
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
        ///Una prueba de SerializeParametros
        ///</summary>
        [TestMethod()]
        public void SerializeParametrosTest()
        {
            List<MOVIMENTO> algo = new List<MOVIMENTO>();
            MOVIMENTO p1 = new MOVIMENTO();

            p1.UNID_MOVIMIENTO = 1111;
            p1.FECHA_MOVIMIENTO = DateTime.Now;
            p1.UNID_TIPO_MOVIMIENTO = 1;
            p1.IS_ACTIVE = true;
            p1.IS_MODIFIED = true;
            p1.LAST_MODIFIED_DATE = 10000000;
            p1.UNID_TECNICO_TRAS = 1;

            algo.Add(p1);

            object parametros = algo; // TODO: Inicializar en un valor adecuado
            string expected = string.Empty; // TODO: Inicializar en un valor adecuado
            string actual;
            actual = SerializerJson.SerializeParametros(parametros);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }
    }
}
