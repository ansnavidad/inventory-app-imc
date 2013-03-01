using InventoryApp.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using InventoryApp.DAL.POCOS;
using System.Collections.Generic;

namespace DALTestProject
{
    
    
    /// <summary>
    ///Se trata de una clase de prueba para BatchLoadDataMapperTest y se pretende que
    ///contenga todas las pruebas unitarias BatchLoadDataMapperTest.
    ///</summary>
    [TestClass()]
    public class BatchLoadDataMapperTest
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
        ///Una prueba de GetBatchProcessRunning
        ///</summary>
        [TestMethod()]
        public void GetBatchProcessRunningTest()
        {
            BatchLoadDataMapper target = new BatchLoadDataMapper(); // TODO: Inicializar en un valor adecuado
            bool expected = false; // TODO: Inicializar en un valor adecuado
            bool actual;
            actual = target.GetBatchProcessRunning();
            Assert.AreEqual(expected, actual);
            
        }

/// <summary>
///Una prueba de GetJsonLogLoad
///</summary>
[TestMethod()]
public void GetJsonLogLoadTest()
{
BatchLoadDataMapper target = new BatchLoadDataMapper(); // TODO: Inicializar en un valor adecuado
Nullable<int> idBatch = new Nullable<int>(); // TODO: Inicializar en un valor adecuado
idBatch = 2;
string expected = string.Empty; // TODO: Inicializar en un valor adecuado
    string actual;
    actual = target.GetJsonLogLoad(idBatch);
    Assert.AreEqual(expected, actual);
    
}

/// <summary>
///Una prueba de GetDeserializeBatchLoad
///</summary>
[TestMethod()]
public void GetDeserializeBatchLoadTest()
{
    BatchLoadDataMapper target = new BatchLoadDataMapper(); // TODO: Inicializar en un valor adecuado
    string listPocos =@"[{'ID':33,'ID_BATCH':2,'GROUP_MSG':'MENSAJE DE PRUEBA 2','MSG':' PRUEBA 2','EXEC_DATE':'\/Date(1362095146947-0600)\/'},{'ID':34,'ID_BATCH':2,'GROUP_MSG':'MENSAJE DE PRUEBA 2','MSG':' PRUEBA 2','EXEC_DATE':'\/Date(1362095147433-0600)\/'}]" ; // TODO: Inicializar en un valor adecuado
    FixupCollection<BATCH_LOAD_CARGAMOV> expected = null; // TODO: Inicializar en un valor adecuado
    FixupCollection<BATCH_LOAD_CARGAMOV> actual;
    actual = target.GetDeserializeBatchLoad(listPocos);
    Assert.AreEqual(expected, actual);
    Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
}


    }
}
