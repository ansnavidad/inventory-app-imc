﻿using InventoryApp.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using InventoryApp.DAL.POCOS;
using System.Collections.Generic;
using System.Linq;

namespace DALTestProject
{
    
    
    /// <summary>
    ///Se trata de una clase de prueba para TipoCotizacionDataMapperTest y se pretende que
    ///contenga todas las pruebas unitarias TipoCotizacionDataMapperTest.
    ///</summary>
    [TestClass()]
    public class TipoCotizacionDataMapperTest
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
            TipoCotizacionDataMapper target = new TipoCotizacionDataMapper(); // TODO: Inicializar en un valor adecuado            
            object actual;
            actual = target.getElements();
            Assert.AreEqual(2, ((List<TIPO_COTIZACION>)actual).Count);
            //Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }

        /// <summary>
        ///Una prueba de getElement
        ///</summary>
        [TestMethod()]
        public void getElementTest()
        {
            TIPO_COTIZACION item = new TIPO_COTIZACION() { UNID_TIPO_COTIZACION = 2453689876, TIPO_COTIZACION_NAME = "TIPO 2" };

            TipoCotizacionDataMapper target = new TipoCotizacionDataMapper(); // TODO: Inicializar en un valor adecuado
            object element = (object)item; // TODO: Inicializar en un valor adecuado
            object actual;
            actual = target.getElement(element);

            Assert.AreEqual(1, ((List<TIPO_COTIZACION>)actual).Count);
            Assert.AreEqual(item.UNID_TIPO_COTIZACION, ((List<TIPO_COTIZACION>)actual)[0].UNID_TIPO_COTIZACION);
            Assert.AreEqual(item.TIPO_COTIZACION_NAME, ((List<TIPO_COTIZACION>)actual)[0].TIPO_COTIZACION_NAME);
            
        }

        /// <summary>
        ///Una prueba de udpateElement
        ///</summary>
        [TestMethod()]
        public void udpateElementTest()
        {
            TIPO_COTIZACION item = new TIPO_COTIZACION() { UNID_TIPO_COTIZACION = 2453689876, TIPO_COTIZACION_NAME = "TIPO prueba" };
            TipoCotizacionDataMapper target = new TipoCotizacionDataMapper(); // TODO: Inicializar en un valor adecuado
            object element = (object)item; // TODO: Inicializar en un valor adecuado
            object actual;
            target.udpateElement(element);
            actual = target.getElement(element);
            Assert.AreEqual(item.UNID_TIPO_COTIZACION, ((List<TIPO_COTIZACION>)actual)[0].UNID_TIPO_COTIZACION);
            Assert.AreEqual(item.TIPO_COTIZACION_NAME, ((List<TIPO_COTIZACION>)actual)[0].TIPO_COTIZACION_NAME);           
        }

        /// <summary>
        ///Una prueba de insertElement
        ///</summary>
        [TestMethod()]
        public void insertElementTest()
        {
            TIPO_COTIZACION item = new TIPO_COTIZACION() {TIPO_COTIZACION_NAME = "TIPO prueba insert 2" };

            TipoCotizacionDataMapper target = new TipoCotizacionDataMapper(); // TODO: Inicializar en un valor adecuado
            object element = (object)item; // TODO: Inicializar en un valor adecuado
            target.insertElement(element);

            var entity = new TAE2Entities();

            TIPO_COTIZACION ETipo = (TIPO_COTIZACION)element;

            var query = (from cust in entity.TIPO_COTIZACION
                         where cust.TIPO_COTIZACION_NAME == ETipo.TIPO_COTIZACION_NAME 
                         select cust).ToList();

            object actual = (object)query;

            Assert.AreEqual(item.TIPO_COTIZACION_NAME, ((List<TIPO_COTIZACION>)actual)[0].TIPO_COTIZACION_NAME);           
            
        }

        /// <summary>
        ///Una prueba de GetJsonTipoCotizacion
        ///</summary>
        [TestMethod()]
        public void GetJsonTipoCotizacionTest()
        {
            TipoCotizacionDataMapper target = new TipoCotizacionDataMapper(); // TODO: Inicializar en un valor adecuado
            string expected = string.Empty; // TODO: Inicializar en un valor adecuado
            string actual;
            actual = target.GetJsonTipoCotizacion();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }
    }
}
