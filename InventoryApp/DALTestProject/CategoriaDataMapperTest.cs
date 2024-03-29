﻿using InventoryApp.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using InventoryApp.DAL.POCOS;
using System.Collections.Generic;

namespace DALTestProject
{
    
    
    /// <summary>
    ///Se trata de una clase de prueba para CategoriaDataMapperTest y se pretende que
    ///contenga todas las pruebas unitarias CategoriaDataMapperTest.
    ///</summary>
    [TestClass()]
    public class CategoriaDataMapperTest
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
        ///Una prueba de GetCategorias
        ///</summary>
        //[TestMethod()]
        //public void GetCategoriasTest()
        //{
        //    CategoriaDataMapper target = new CategoriaDataMapper(); // TODO: Inicializar en un valor adecuado
        //    //CategoriaCollection expected = null; // TODO: Inicializar en un valor adecuado
        //    CategoriaCollection actual;
        //    actual = target.GetCategorias();
        //    Assert.AreEqual(4, actual.Count);
        //    //Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        //}

        /// <summary>
        ///Una prueba de getElementsByProveedor
        ///</summary>
        [TestMethod()]
        public void getElementsByProveedorTest()
        {
            CategoriaDataMapper target = new CategoriaDataMapper(); // TODO: Inicializar en un valor adecuado
            PROVEEDOR proveedor = new PROVEEDOR() { UNID_PROVEEDOR = 20121113012552944 }; // TODO: Inicializar en un valor adecuado
            List<CATEGORIA> expected = null; // TODO: Inicializar en un valor adecuado
            List<CATEGORIA> actual;
            actual = target.getElementsByProveedor(proveedor);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }

        /// <summary>
        ///Una prueba de GetJsonCategoria
        ///</summary>
        [TestMethod()]
        public void GetJsonCategoriaTest()
        {
            CategoriaDataMapper target = new CategoriaDataMapper(); // TODO: Inicializar en un valor adecuado
            string expected = string.Empty; // TODO: Inicializar en un valor adecuado
            string actual;
            actual = target.GetJsonCategoria();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }

        /// <summary>
        ///Una prueba de LastModifiedDate
        ///</summary>
        [TestMethod()]
        public void LastModifiedDateTest()
        {
            CategoriaDataMapper target = new CategoriaDataMapper(); // TODO: Inicializar en un valor adecuado
            Nullable<long> expected = new Nullable<long>(); // TODO: Inicializar en un valor adecuado
            Nullable<long> actual;
            actual = target.LastModifiedDate();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Compruebe la exactitud de este método de prueba.");
        }
    }
}
