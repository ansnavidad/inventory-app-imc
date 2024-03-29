﻿using InventoryApp.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using InventoryApp.DAL.POCOS;
using System.Collections.Generic;

namespace DALTestProject
{
    
    
    /// <summary>
    ///Se trata de una clase de prueba para ReciboDataMapperTest y se pretende que
    ///contenga todas las pruebas unitarias ReciboDataMapperTest.
    ///</summary>
    [TestClass()]
    public class ReciboDataMapperTest
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
        ///Una prueba de GetListMovimiento
        ///</summary>
        [TestMethod()]
        public void GetListMovimientoTest()
        {
            ReciboDataMapper target = new ReciboDataMapper(); // TODO: Inicializar en un valor adecuado
            RECIBO recibo = new RECIBO() { UNID_RECIBO = 20121203022523751 }; // TODO: Inicializar en un valor adecuado
            List<MOVIMENTO> expected = null; // TODO: Inicializar en un valor adecuado
            List<MOVIMENTO> actual;
            actual = target.GetListMovimiento(recibo);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }

        /// <summary>
        ///Una prueba de GetJsonRecibo
        ///</summary>
        [TestMethod()]
        public void GetJsonReciboTest()
        {
            ReciboDataMapper target = new ReciboDataMapper(); // TODO: Inicializar en un valor adecuado
            string expected = string.Empty; // TODO: Inicializar en un valor adecuado
            string actual;
            actual = target.GetJsonRecibo();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }
    }
}
