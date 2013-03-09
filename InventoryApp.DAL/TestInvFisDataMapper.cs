using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public class TestInvFisDataMapper:IDataMapper
    {
        public object getElements()
        {
            return new List<INVENTARIO_FISICO>()
            {
                new INVENTARIO_FISICO()
                {
                    ALMACEN=new ALMACEN()
                    {
                        ALMACEN_NAME="Test",
                        UNID_ALMACEN=123123
                    },
                    FECHA_FIN=null,
                    FECHA_INICIO=DateTime.Now,
                    UNID_INVENTARIO_FISICO=123123,
                    IS_FINALIZADO=false
                },
                new INVENTARIO_FISICO()
                {
                    ALMACEN=new ALMACEN()
                    {
                        ALMACEN_NAME="test2",
                        UNID_ALMACEN=234234
                    },
                    FECHA_FIN=null,
                    FECHA_INICIO=DateTime.Now,
                    UNID_INVENTARIO_FISICO=234234,
                    IS_FINALIZADO=true
                }
            };
        }

        public object getElement(object element)
        {
            throw new NotImplementedException();
        }

        public void udpateElement(object element)
        {
            throw new NotImplementedException();
        }

        public void insertElement(object element)
        {
            throw new NotImplementedException();
        }

        public void deleteElement(object element, POCOS.USUARIO u)
        {
            throw new NotImplementedException();
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}
