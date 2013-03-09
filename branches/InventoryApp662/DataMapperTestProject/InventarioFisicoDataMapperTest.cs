using InventoryApp.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using InventoryApp.DAL.POCOS;
using System.Collections.Generic;

namespace DataMapperTestProject
{
    
    
    /// <summary>
    ///Se trata de una clase de prueba para InventarioFisicoDataMapperTest y se pretende que
    ///contenga todas las pruebas unitarias InventarioFisicoDataMapperTest.
    ///</summary>
    [TestClass()]
    public class InventarioFisicoDataMapperTest
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
        ///Una prueba de Insert
        ///</summary>
        [TestMethod()]
        public void InsertTest()
        {
            InventarioFisicoDataMapper target = new InventarioFisicoDataMapper(); // TODO: Inicializar en un valor adecuado
            long unidParent = UNID.getNewUNID();
            INVENTARIO_FISICO inventarioFisico = new INVENTARIO_FISICO()
                {
                    UNID_ALMACEN = 20121128133251894,
                    FECHA_FIN = DateTime.Now,
                    FECHA_INICIO = DateTime.Now,
                    IS_FINALIZADO = true,
                    UNID_INVENTARIO_FISICO =unidParent,
                    INVENTARIO_FISICO_DET = new FixupCollection<INVENTARIO_FISICO_DET>()
                    {
                        new INVENTARIO_FISICO_DET()
                        {
                             CANTIDAD=1,
                              UNID_ITEM=20130214095947037,
                              UNID_INVENTARIO_FISICO_DET=UNID.getNewUNID(),
                              UNID_INVENTARIO_FISICO=unidParent
                        },
                        new INVENTARIO_FISICO_DET()
                        {
                             CANTIDAD=2,
                              UNID_ITEM=20130214095947037,
                              UNID_INVENTARIO_FISICO_DET=UNID.getNewUNID(),
                              UNID_INVENTARIO_FISICO=unidParent
                        }
                    }
                }; // TODO: Inicializar en un valor adecuado
            List<INVENTARIO_FISICO_DET> detalles = new List<INVENTARIO_FISICO_DET>();



            target.Insert(inventarioFisico);
            Assert.Inconclusive("Un método que no devuelve ningún valor no se puede comprobar.");
        }
    }
}
