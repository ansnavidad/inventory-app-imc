﻿using InventoryApp.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using InventoryApp.DAL.POCOS;
using System.Collections.Generic;

namespace DALTestProject
{
    
    
    /// <summary>
    ///Se trata de una clase de prueba para UploadLogDataMapperTest y se pretende que
    ///contenga todas las pruebas unitarias UploadLogDataMapperTest.
    ///</summary>
    [TestClass()]
    public class UploadLogDataMapperTest
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
        ///Una prueba de GetJsonUpLoadLog
        ///</summary>
        [TestMethod()]
        public void GetJsonUpLoadLogTest()
        {
            UPLOAD_LOG newUpLoad = new UPLOAD_LOG() { UNID_UPLOAD_LOG=34567, UNID_USUARIO=6543234567, PC_NAME="isaac-pc", IP_DIR="192.168.1.110"};
            UploadLogDataMapper target = new UploadLogDataMapper(); // TODO: Inicializar en un valor adecuado
            //UPLOAD_LOG upLoadLog = null; // TODO: Inicializar en un valor adecuado
            string expected = string.Empty; // TODO: Inicializar en un valor adecuado
            string actual;
            actual = target.GetJsonUpLoadLog(newUpLoad);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }

        /// <summary>
        ///Una prueba de GetJsonUpLoadLog
        ///</summary>
        [TestMethod()]
        public void GetJsonUpLoadLogTest1()
        {
            UPLOAD_LOG newUpLoad = new UPLOAD_LOG() { UNID_USUARIO = 6543234567, PC_NAME = "isaac-pc", IP_DIR = "192.168.1.110" };
            UploadLogDataMapper target = new UploadLogDataMapper(); // TODO: Inicializar en un valor adecuado
            //UPLOAD_LOG upLoadLog = null; // TODO: Inicializar en un valor adecuado
            string expected = string.Empty; // TODO: Inicializar en un valor adecuado
            string actual;
            actual = target.GetJsonUpLoadLog(newUpLoad);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }

        /// <summary>
        ///Una prueba de GetDeserializeUpLoadLog
        ///</summary>
        [TestMethod()]
        public void GetDeserializeUpLoadLogTest()
        {

            UploadLogDataMapper target = new UploadLogDataMapper(); // TODO: Inicializar en un valor adecuado
            string upLoadLog = "{'UNID_UPLOAD_LOG':0,'MSG':null,'IP_DIR':'192.168.1.110','PC_NAME':'isaac-pc','UNID_USUARIO':2345676543456,'USUARIO':null}"; // TODO: Inicializar en un valor adecuado
            List<UPLOAD_LOG> expected = null; // TODO: Inicializar en un valor adecuado
            List<UPLOAD_LOG> actual;
            actual = target.GetDeserializeUpLoadLog(upLoadLog);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }

        /// <summary>
        ///Una prueba de GetDeserializeUpLoadLog
        ///</summary>
        [TestMethod()]
        public void GetDeserializeUpLoadLogTest1()
        {
            UploadLogDataMapper target = new UploadLogDataMapper(); // TODO: Inicializar en un valor adecuado
            string upLoadLog = "{'UNID_UPLOAD_LOG':0,'MSG':null,'IP_DIR':'192.168.1.110','PC_NAME':'isaac-pc','UNID_USUARIO':2345676543456,'USUARIO':null}"; // TODO: Inicializar en un valor adecuado
            List<UPLOAD_LOG> expected = null; // TODO: Inicializar en un valor adecuado
            List<UPLOAD_LOG> actual;
            actual = target.GetDeserializeUpLoadLog(upLoadLog);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }

        
    }
}