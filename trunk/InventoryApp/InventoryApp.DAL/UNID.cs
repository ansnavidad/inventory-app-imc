using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public static class UNID
    {
        public static long getNewUNID(){

            string res = "";
            string aux = "";

            res += DateTime.Now.Year.ToString();

            aux = DateTime.Now.Month.ToString();
            if(aux.Length == 1)
                res += "0";
            res += aux;

            aux = DateTime.Now.Day.ToString();
            if(aux.Length == 1)
                res += "0";
            res += aux;

            aux = DateTime.Now.Hour.ToString();
            if(aux.Length == 1)
                res += "0";
            res += aux;

            aux = DateTime.Now.Minute.ToString();
            if(aux.Length == 1)
                res += "0";
            res += aux;

            aux = DateTime.Now.Second.ToString();
            if(aux.Length == 1)
                res += "0";
            res += aux;

            aux = DateTime.Now.Millisecond.ToString();
            if(aux.Length == 1)
                res += "00";
            else if(aux.Length == 2)
                res += "0";
            res += aux;

            long aux2 = long.Parse(res);

            return aux2;
        }

        //Función para determinar la fecha más grande
        public static bool compareUNIDS(long UNIDactual, long UNIDenviada) {

            int auxActual = 0;
            int auxEnviado = 0;

            //Comparar años
            auxActual = Int16.Parse(UNIDactual.ToString().Substring(0, 4));
            auxEnviado = Int16.Parse(UNIDenviada.ToString().Substring(0, 4));
            if (auxActual > auxEnviado)
                return false;
            if (auxActual < auxEnviado)
                return true;
            
            //Comparar meses
            auxActual = Int16.Parse(UNIDactual.ToString().Substring(4, 2));
            auxEnviado = Int16.Parse(UNIDenviada.ToString().Substring(4, 2));
            if (auxActual > auxEnviado)
                return false;
            if (auxActual < auxEnviado)
                return true;

            //Comparar dias
            auxActual = Int16.Parse(UNIDactual.ToString().Substring(6, 2));
            auxEnviado = Int16.Parse(UNIDenviada.ToString().Substring(6, 2));
            if (auxActual > auxEnviado)
                return false;
            if (auxActual < auxEnviado)
                return true;

            //Comparar horas
            auxActual = Int16.Parse(UNIDactual.ToString().Substring(8, 2));
            auxEnviado = Int16.Parse(UNIDenviada.ToString().Substring(8, 2));
            if (auxActual > auxEnviado)
                return false;
            if (auxActual < auxEnviado)
                return true;

            //Comparar minutos
            auxActual = Int16.Parse(UNIDactual.ToString().Substring(10, 2));
            auxEnviado = Int16.Parse(UNIDenviada.ToString().Substring(10, 2));
            if (auxActual > auxEnviado)
                return false;
            if (auxActual < auxEnviado)
                return true;

            //Comparar segundos
            auxActual = Int16.Parse(UNIDactual.ToString().Substring(12, 2));
            auxEnviado = Int16.Parse(UNIDenviada.ToString().Substring(12, 2));
            if (auxActual > auxEnviado)
                return false;
            if (auxActual < auxEnviado)
                return true;

            //Comparar milisegundos
            auxActual = Int16.Parse(UNIDactual.ToString().Substring(14, 3));
            auxEnviado = Int16.Parse(UNIDenviada.ToString().Substring(14, 3));
            if (auxActual > auxEnviado)
                return false;
            if (auxActual < auxEnviado)
                return true;
        
            return false;
        }
        public static bool compareUNIDS(long? UNIDactual, long? UNIDenviada)
        {

            int auxActual = 0;
            int auxEnviado = 0;

            //Comparar años
            auxActual = Int16.Parse(UNIDactual.ToString().Substring(0, 4));
            auxEnviado = Int16.Parse(UNIDenviada.ToString().Substring(0, 4));
            if (auxActual > auxEnviado)
                return false;
            if (auxActual < auxEnviado)
                return true;

            //Comparar meses
            auxActual = Int16.Parse(UNIDactual.ToString().Substring(4, 2));
            auxEnviado = Int16.Parse(UNIDenviada.ToString().Substring(4, 2));
            if (auxActual > auxEnviado)
                return false;
            if (auxActual < auxEnviado)
                return true;

            //Comparar dias
            auxActual = Int16.Parse(UNIDactual.ToString().Substring(6, 2));
            auxEnviado = Int16.Parse(UNIDenviada.ToString().Substring(6, 2));
            if (auxActual > auxEnviado)
                return false;
            if (auxActual < auxEnviado)
                return true;

            //Comparar horas
            auxActual = Int16.Parse(UNIDactual.ToString().Substring(8, 2));
            auxEnviado = Int16.Parse(UNIDenviada.ToString().Substring(8, 2));
            if (auxActual > auxEnviado)
                return false;
            if (auxActual < auxEnviado)
                return true;

            //Comparar minutos
            auxActual = Int16.Parse(UNIDactual.ToString().Substring(10, 2));
            auxEnviado = Int16.Parse(UNIDenviada.ToString().Substring(10, 2));
            if (auxActual > auxEnviado)
                return false;
            if (auxActual < auxEnviado)
                return true;

            //Comparar segundos
            auxActual = Int16.Parse(UNIDactual.ToString().Substring(12, 2));
            auxEnviado = Int16.Parse(UNIDenviada.ToString().Substring(12, 2));
            if (auxActual > auxEnviado)
                return false;
            if (auxActual < auxEnviado)
                return true;

            //Comparar milisegundos
            auxActual = Int16.Parse(UNIDactual.ToString().Substring(14, 3));
            auxEnviado = Int16.Parse(UNIDenviada.ToString().Substring(14, 3));
            if (auxActual > auxEnviado)
                return false;
            if (auxActual < auxEnviado)
                return true;

            return false;
        }

        //Función para almanecar usuarios con las modificaciones
        public static void Master(object o, USUARIO u, int num, string control)
        {
            try
            {
                BANCO poco = (BANCO)o;
                Master_BANCO(poco, u, num, control);
            }
            catch {

                try
                {
                    CATEGORIA poco = (CATEGORIA)o;
                    Master_CATEGORIA(poco, u, num, control);
                }
                catch
                {
                    try
                    {
                        CIUDAD poco = (CIUDAD)o;
                        Master_CIUDAD(poco, u, num, control);
                    }
                    catch
                    {
                        try
                        {
                            CLIENTE poco = (CLIENTE)o;
                            Master_CLIENTE(poco, u, num, control);
                        }
                        catch
                        {
                            try
                            {
                                DEPARTAMENTO poco = (DEPARTAMENTO)o;
                                Master_DEPARTAMENTO(poco, u, num, control);
                            }
                            catch
                            {
                                try
                                {
                                    EMPRESA poco = (EMPRESA)o;
                                    Master_EMPRESA(poco, u, num, control);
                                }
                                catch 
                                {                                    
                                    try
                                    {
                                        INFRAESTRUCTURA poco = (INFRAESTRUCTURA)o;
                                        Master_INFRAESTRUCTURA(poco, u, num, control);
                                    }
                                    catch
                                    {
                                        try
                                        {
                                            ITEM_STATUS poco = (ITEM_STATUS)o;
                                            Master_ITEMSTATUS(poco, u, num, control);
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                MEDIO_ENVIO poco = (MEDIO_ENVIO)o;
                                                Master_MEDIOENVIO(poco, u, num, control);
                                            }
                                            catch
                                            {
                                                try
                                                {
                                                    MONEDA poco = (MONEDA)o;
                                                    Master_MONEDA(poco, u, num, control);
                                                }
                                                catch
                                                {
                                                    try
                                                    {
                                                        PAI poco = (PAI)o;
                                                        Master_PAIS(poco, u, num, control);
                                                    }
                                                    catch
                                                    {
                                                        try
                                                        {
                                                            PROYECTO poco = (PROYECTO)o;
                                                            Master_PROYECTO(poco, u, num, control);
                                                        }
                                                        catch
                                                        {
                                                            try
                                                            {
                                                                SERVICIO poco = (SERVICIO)o;
                                                                Master_SERVICIO(poco, u, num, control);
                                                            }
                                                            catch
                                                            {
                                                                try
                                                                {
                                                                    SOLICITANTE poco = (SOLICITANTE)o;
                                                                    Master_SOLICITANTE(poco, u, num, control);
                                                                }
                                                                catch
                                                                {
                                                                    try
                                                                    {
                                                                        TERMINO_ENVIO poco = (TERMINO_ENVIO)o;
                                                                        Master_TERMINOENVIO(poco, u, num, control);
                                                                    }
                                                                    catch
                                                                    {
                                                                        try
                                                                        {
                                                                            TIPO_COTIZACION poco = (TIPO_COTIZACION)o;
                                                                            Master_TIPOCOTIZACION(poco, u, num, control);
                                                                        }
                                                                        catch
                                                                        {
                                                                            try
                                                                            {
                                                                                TIPO_EMPRESA poco = (TIPO_EMPRESA)o;
                                                                                Master_TIPOEMPRESA(poco, u, num, control);
                                                                            }
                                                                            catch 
                                                                            {
                                                                                try
                                                                                {
                                                                                    TIPO_PEDIMENTO poco = (TIPO_PEDIMENTO)o;
                                                                                    Master_TIPOPEDIMENTO(poco, u, num, control);
                                                                                }
                                                                                catch
                                                                                {
                                                                                    try
                                                                                    {
                                                                                        TRANSPORTE poco = (TRANSPORTE)o;
                                                                                        Master_TRANSPORTE(poco, u, num, control);
                                                                                    }
                                                                                    catch
                                                                                    {
                                                                                        try
                                                                                        {
                                                                                            UNIDAD poco = (UNIDAD)o;
                                                                                            Master_UNIDAD(poco, u, num, control);
                                                                                        }
                                                                                        catch { }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    } 
                                }
                            }
                        }
                    }
                }  
            }            
        }

        #region MASTER TABLAS

        private static void Master_BANCO(BANCO poco, USUARIO u, int num, string control) {

            using (var entity = new TAE2Entities())
            {                
                var Master = entity.MASTER_INVENTARIOS.OrderBy(p => p.UNID_MASTER_INVENTARIOS).Where(p => p.UNID_BANCO != null && p.UNID_BANCO == poco.UNID_BANCO).ToList();
                var NewMaster = new MASTER_INVENTARIOS();

                NewMaster.UNID_MASTER_INVENTARIOS = UNID.getNewUNID();
                NewMaster.UNID_BANCO = poco.UNID_BANCO;
                NewMaster.UNID_USUARIO_MOD = u.UNID_USUARIO;
                NewMaster.MODIFICACIONES = control;
                NewMaster.FECHA = DateTime.Now;
                NewMaster.LAST_MODIFIED_DATE = UNID.getNewUNID();
                NewMaster.IS_ACTIVE = true;
                NewMaster.IS_MODIFIED = true;
                string aux = "";
                foreach (USUARIO_ROL r in u.USUARIO_ROL)
                    aux += r.ROL.ROL_NAME + ", ";
                if (!String.IsNullOrEmpty(aux))
                {
                    aux = aux.Substring(0, aux.Length - 2);
                    aux += ".";
                }
                NewMaster.ROLES = aux;

                if (Master != null && Master.Count > 0)
                {
                    var CreatorUser = entity.MASTER_INVENTARIOS.OrderByDescending(p => p.UNID_BANCO).Where(p => p.UNID_BANCO == NewMaster.UNID_BANCO).ToList();
                    NewMaster.UNID_USUARIO_CREADOR = CreatorUser[0].UNID_USUARIO_MOD;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();                    
                }
                else
                {
                    NewMaster.UNID_USUARIO_CREADOR = u.UNID_USUARIO;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
            }
        }

        private static void Master_CATEGORIA(CATEGORIA poco, USUARIO u, int num, string control)
        {
            using (var entity = new TAE2Entities())
            {
                var Master = entity.MASTER_INVENTARIOS.OrderBy(p => p.UNID_MASTER_INVENTARIOS).Where(p => p.UNID_CATEGORIA != null && p.UNID_CATEGORIA == poco.UNID_CATEGORIA).ToList();
                var NewMaster = new MASTER_INVENTARIOS();

                NewMaster.UNID_MASTER_INVENTARIOS = UNID.getNewUNID();
                NewMaster.UNID_CATEGORIA = poco.UNID_CATEGORIA;
                NewMaster.UNID_USUARIO_MOD = u.UNID_USUARIO;
                NewMaster.MODIFICACIONES = control;
                NewMaster.FECHA = DateTime.Now;
                NewMaster.LAST_MODIFIED_DATE = UNID.getNewUNID();
                NewMaster.IS_ACTIVE = true;
                NewMaster.IS_MODIFIED = true;
                string aux = "";
                foreach (USUARIO_ROL r in u.USUARIO_ROL)
                    aux += r.ROL.ROL_NAME + ", ";
                if (!String.IsNullOrEmpty(aux))
                {
                    aux = aux.Substring(0, aux.Length - 2);
                    aux += ".";
                }
                NewMaster.ROLES = aux;

                if (Master != null && Master.Count > 0)
                {
                    var CreatorUser = entity.MASTER_INVENTARIOS.OrderByDescending(p => p.UNID_CATEGORIA).Where(p => p.UNID_CATEGORIA == NewMaster.UNID_CATEGORIA).ToList();
                    NewMaster.UNID_USUARIO_CREADOR = CreatorUser[0].UNID_USUARIO_MOD;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
                else
                {
                    NewMaster.UNID_USUARIO_CREADOR = u.UNID_USUARIO;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
            }
        }
        
        private static void Master_CIUDAD(CIUDAD poco, USUARIO u, int num, string control)
        {
            using (var entity = new TAE2Entities())
            {
                var Master = entity.MASTER_INVENTARIOS.OrderBy(p => p.UNID_MASTER_INVENTARIOS).Where(p => p.UNID_CIUDAD != null && p.UNID_CIUDAD == poco.UNID_CIUDAD).ToList();
                var NewMaster = new MASTER_INVENTARIOS();

                NewMaster.UNID_MASTER_INVENTARIOS = UNID.getNewUNID();
                NewMaster.UNID_CIUDAD = poco.UNID_CIUDAD;
                NewMaster.UNID_USUARIO_MOD = u.UNID_USUARIO;
                NewMaster.MODIFICACIONES = control;
                NewMaster.FECHA = DateTime.Now;
                NewMaster.LAST_MODIFIED_DATE = UNID.getNewUNID();
                NewMaster.IS_ACTIVE = true;
                NewMaster.IS_MODIFIED = true;
                string aux = "";
                foreach (USUARIO_ROL r in u.USUARIO_ROL)
                    aux += r.ROL.ROL_NAME + ", ";
                if (!String.IsNullOrEmpty(aux))
                {
                    aux = aux.Substring(0, aux.Length - 2);
                    aux += ".";
                }
                NewMaster.ROLES = aux;

                if (Master != null && Master.Count > 0)
                {
                    var CreatorUser = entity.MASTER_INVENTARIOS.OrderByDescending(p => p.UNID_CIUDAD).Where(p => p.UNID_CIUDAD == NewMaster.UNID_CIUDAD).ToList();
                    NewMaster.UNID_USUARIO_CREADOR = CreatorUser[0].UNID_USUARIO_MOD;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
                else
                {
                    NewMaster.UNID_USUARIO_CREADOR = u.UNID_USUARIO;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
            }
        }

        private static void Master_CLIENTE(CLIENTE poco, USUARIO u, int num, string control)
        {
            using (var entity = new TAE2Entities())
            {
                var Master = entity.MASTER_INVENTARIOS.OrderBy(p => p.UNID_MASTER_INVENTARIOS).Where(p => p.UNID_CLIENTE != null && p.UNID_CLIENTE == poco.UNID_CLIENTE).ToList();
                var NewMaster = new MASTER_INVENTARIOS();

                NewMaster.UNID_MASTER_INVENTARIOS = UNID.getNewUNID();
                NewMaster.UNID_CLIENTE = poco.UNID_CLIENTE;
                NewMaster.UNID_USUARIO_MOD = u.UNID_USUARIO;
                NewMaster.MODIFICACIONES = control;
                NewMaster.FECHA = DateTime.Now;
                NewMaster.LAST_MODIFIED_DATE = UNID.getNewUNID();
                NewMaster.IS_ACTIVE = true;
                NewMaster.IS_MODIFIED = true;
                string aux = "";
                foreach (USUARIO_ROL r in u.USUARIO_ROL)
                    aux += r.ROL.ROL_NAME + ", ";
                if (!String.IsNullOrEmpty(aux))
                {
                    aux = aux.Substring(0, aux.Length - 2);
                    aux += ".";
                }
                NewMaster.ROLES = aux;

                if (Master != null && Master.Count > 0)
                {
                    var CreatorUser = entity.MASTER_INVENTARIOS.OrderByDescending(p => p.UNID_CLIENTE).Where(p => p.UNID_CLIENTE == NewMaster.UNID_CLIENTE).ToList();
                    NewMaster.UNID_USUARIO_CREADOR = CreatorUser[0].UNID_USUARIO_MOD;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
                else
                {
                    NewMaster.UNID_USUARIO_CREADOR = u.UNID_USUARIO;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
            }
        }

        private static void Master_DEPARTAMENTO(DEPARTAMENTO poco, USUARIO u, int num, string control)
        {
            using (var entity = new TAE2Entities())
            {
                var Master = entity.MASTER_INVENTARIOS.OrderBy(p => p.UNID_MASTER_INVENTARIOS).Where(p => p.UNID_DEPARTAMENTO != null && p.UNID_DEPARTAMENTO == poco.UNID_DEPARTAMENTO).ToList();
                var NewMaster = new MASTER_INVENTARIOS();

                NewMaster.UNID_MASTER_INVENTARIOS = UNID.getNewUNID();
                NewMaster.UNID_DEPARTAMENTO = poco.UNID_DEPARTAMENTO;
                NewMaster.UNID_USUARIO_MOD = u.UNID_USUARIO;
                NewMaster.MODIFICACIONES = control;
                NewMaster.FECHA = DateTime.Now;
                NewMaster.LAST_MODIFIED_DATE = UNID.getNewUNID();
                NewMaster.IS_ACTIVE = true;
                NewMaster.IS_MODIFIED = true;
                string aux = "";
                foreach (USUARIO_ROL r in u.USUARIO_ROL)
                    aux += r.ROL.ROL_NAME + ", ";
                if (!String.IsNullOrEmpty(aux))
                {
                    aux = aux.Substring(0, aux.Length - 2);
                    aux += ".";
                }
                NewMaster.ROLES = aux;

                if (Master != null && Master.Count > 0)
                {
                    var CreatorUser = entity.MASTER_INVENTARIOS.OrderByDescending(p => p.UNID_DEPARTAMENTO).Where(p => p.UNID_DEPARTAMENTO == NewMaster.UNID_DEPARTAMENTO).ToList();
                    NewMaster.UNID_USUARIO_CREADOR = CreatorUser[0].UNID_USUARIO_MOD;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
                else
                {
                    NewMaster.UNID_USUARIO_CREADOR = u.UNID_USUARIO;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
            }
        }
        
        private static void Master_EMPRESA(EMPRESA poco, USUARIO u, int num, string control)
        {
            using (var entity = new TAE2Entities())
            {
                var Master = entity.MASTER_INVENTARIOS.OrderBy(p => p.UNID_MASTER_INVENTARIOS).Where(p => p.UNID_EMPRESA != null && p.UNID_EMPRESA == poco.UNID_EMPRESA).ToList();
                var NewMaster = new MASTER_INVENTARIOS();

                NewMaster.UNID_MASTER_INVENTARIOS = UNID.getNewUNID();
                NewMaster.UNID_EMPRESA = poco.UNID_EMPRESA;
                NewMaster.UNID_USUARIO_MOD = u.UNID_USUARIO;
                NewMaster.MODIFICACIONES = control;
                NewMaster.FECHA = DateTime.Now;
                NewMaster.LAST_MODIFIED_DATE = UNID.getNewUNID();
                NewMaster.IS_ACTIVE = true;
                NewMaster.IS_MODIFIED = true;
                string aux = "";
                foreach (USUARIO_ROL r in u.USUARIO_ROL)
                    aux += r.ROL.ROL_NAME + ", ";
                if (!String.IsNullOrEmpty(aux))
                {
                    aux = aux.Substring(0, aux.Length - 2);
                    aux += ".";
                }
                NewMaster.ROLES = aux;

                if (Master != null && Master.Count > 0)
                {
                    var CreatorUser = entity.MASTER_INVENTARIOS.OrderByDescending(p => p.UNID_EMPRESA).Where(p => p.UNID_EMPRESA == NewMaster.UNID_EMPRESA).ToList();
                    NewMaster.UNID_USUARIO_CREADOR = CreatorUser[0].UNID_USUARIO_MOD;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
                else
                {
                    NewMaster.UNID_USUARIO_CREADOR = u.UNID_USUARIO;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
            }
        }

        private static void Master_INFRAESTRUCTURA(INFRAESTRUCTURA poco, USUARIO u, int num, string control)
        {
            using (var entity = new TAE2Entities())
            {
                var Master = entity.MASTER_INVENTARIOS.OrderBy(p => p.UNID_MASTER_INVENTARIOS).Where(p => p.UNID_INFRAESTRUCTURA != null && p.UNID_INFRAESTRUCTURA == poco.UNID_INFRAESTRUCTURA).ToList();
                var NewMaster = new MASTER_INVENTARIOS();

                NewMaster.UNID_MASTER_INVENTARIOS = UNID.getNewUNID();
                NewMaster.UNID_INFRAESTRUCTURA = poco.UNID_INFRAESTRUCTURA;
                NewMaster.UNID_USUARIO_MOD = u.UNID_USUARIO;
                NewMaster.MODIFICACIONES = control;
                NewMaster.FECHA = DateTime.Now;
                NewMaster.LAST_MODIFIED_DATE = UNID.getNewUNID();
                NewMaster.IS_ACTIVE = true;
                NewMaster.IS_MODIFIED = true;
                string aux = "";
                foreach (USUARIO_ROL r in u.USUARIO_ROL)
                    aux += r.ROL.ROL_NAME + ", ";
                if (!String.IsNullOrEmpty(aux))
                {
                    aux = aux.Substring(0, aux.Length - 2);
                    aux += ".";
                }
                NewMaster.ROLES = aux;

                if (Master != null && Master.Count > 0)
                {
                    var CreatorUser = entity.MASTER_INVENTARIOS.OrderByDescending(p => p.UNID_INFRAESTRUCTURA).Where(p => p.UNID_INFRAESTRUCTURA == NewMaster.UNID_INFRAESTRUCTURA).ToList();
                    NewMaster.UNID_USUARIO_CREADOR = CreatorUser[0].UNID_USUARIO_MOD;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
                else
                {
                    NewMaster.UNID_USUARIO_CREADOR = u.UNID_USUARIO;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
            }
        }
        
        private static void Master_ITEMSTATUS(ITEM_STATUS poco, USUARIO u, int num, string control)
        {
            using (var entity = new TAE2Entities())
            {
                var Master = entity.MASTER_INVENTARIOS.OrderBy(p => p.UNID_MASTER_INVENTARIOS).Where(p => p.UNID_ITEM_STATUS != null && p.UNID_ITEM_STATUS == poco.UNID_ITEM_STATUS).ToList();
                var NewMaster = new MASTER_INVENTARIOS();

                NewMaster.UNID_MASTER_INVENTARIOS = UNID.getNewUNID();
                NewMaster.UNID_ITEM_STATUS = poco.UNID_ITEM_STATUS;
                NewMaster.UNID_USUARIO_MOD = u.UNID_USUARIO;
                NewMaster.MODIFICACIONES = control;
                NewMaster.FECHA = DateTime.Now;
                NewMaster.LAST_MODIFIED_DATE = UNID.getNewUNID();
                NewMaster.IS_ACTIVE = true;
                NewMaster.IS_MODIFIED = true;
                string aux = "";
                foreach (USUARIO_ROL r in u.USUARIO_ROL)
                    aux += r.ROL.ROL_NAME + ", ";
                if (!String.IsNullOrEmpty(aux))
                {
                    aux = aux.Substring(0, aux.Length - 2);
                    aux += ".";
                }
                NewMaster.ROLES = aux;

                if (Master != null && Master.Count > 0)
                {
                    var CreatorUser = entity.MASTER_INVENTARIOS.OrderByDescending(p => p.UNID_ITEM_STATUS).Where(p => p.UNID_ITEM_STATUS == NewMaster.UNID_ITEM_STATUS).ToList();
                    NewMaster.UNID_USUARIO_CREADOR = CreatorUser[0].UNID_USUARIO_MOD;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
                else
                {
                    NewMaster.UNID_USUARIO_CREADOR = u.UNID_USUARIO;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
            }
        }

        private static void Master_MEDIOENVIO(MEDIO_ENVIO poco, USUARIO u, int num, string control)
        {
            using (var entity = new TAE2Entities())
            {
                var Master = entity.MASTER_INVENTARIOS.OrderBy(p => p.UNID_MASTER_INVENTARIOS).Where(p => p.UNID_MEDIO_ENVIO != null && p.UNID_MEDIO_ENVIO == poco.UNID_MEDIO_ENVIO).ToList();
                var NewMaster = new MASTER_INVENTARIOS();

                NewMaster.UNID_MASTER_INVENTARIOS = UNID.getNewUNID();
                NewMaster.UNID_MEDIO_ENVIO = poco.UNID_MEDIO_ENVIO;
                NewMaster.UNID_USUARIO_MOD = u.UNID_USUARIO;
                NewMaster.MODIFICACIONES = control;
                NewMaster.FECHA = DateTime.Now;
                NewMaster.LAST_MODIFIED_DATE = UNID.getNewUNID();
                NewMaster.IS_ACTIVE = true;
                NewMaster.IS_MODIFIED = true;
                string aux = "";
                foreach (USUARIO_ROL r in u.USUARIO_ROL)
                    aux += r.ROL.ROL_NAME + ", ";
                if (!String.IsNullOrEmpty(aux))
                {
                    aux = aux.Substring(0, aux.Length - 2);
                    aux += ".";
                }
                NewMaster.ROLES = aux;

                if (Master != null && Master.Count > 0)
                {
                    var CreatorUser = entity.MASTER_INVENTARIOS.OrderByDescending(p => p.UNID_MEDIO_ENVIO).Where(p => p.UNID_MEDIO_ENVIO == NewMaster.UNID_MEDIO_ENVIO).ToList();
                    NewMaster.UNID_USUARIO_CREADOR = CreatorUser[0].UNID_USUARIO_MOD;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
                else
                {
                    NewMaster.UNID_USUARIO_CREADOR = u.UNID_USUARIO;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
            }
        }
        
        private static void Master_MONEDA(MONEDA poco, USUARIO u, int num, string control)
        {
            using (var entity = new TAE2Entities())
            {
                var Master = entity.MASTER_INVENTARIOS.OrderBy(p => p.UNID_MASTER_INVENTARIOS).Where(p => p.UNID_MONEDA != null && p.UNID_MONEDA == poco.UNID_MONEDA).ToList();
                var NewMaster = new MASTER_INVENTARIOS();

                NewMaster.UNID_MASTER_INVENTARIOS = UNID.getNewUNID();
                NewMaster.UNID_MONEDA = poco.UNID_MONEDA;
                NewMaster.UNID_USUARIO_MOD = u.UNID_USUARIO;
                NewMaster.MODIFICACIONES = control;
                NewMaster.FECHA = DateTime.Now;
                NewMaster.LAST_MODIFIED_DATE = UNID.getNewUNID();
                NewMaster.IS_ACTIVE = true;
                NewMaster.IS_MODIFIED = true;
                string aux = "";
                foreach (USUARIO_ROL r in u.USUARIO_ROL)
                    aux += r.ROL.ROL_NAME + ", ";
                if (!String.IsNullOrEmpty(aux))
                {
                    aux = aux.Substring(0, aux.Length - 2);
                    aux += ".";
                }
                NewMaster.ROLES = aux;

                if (Master != null && Master.Count > 0)
                {
                    var CreatorUser = entity.MASTER_INVENTARIOS.OrderByDescending(p => p.UNID_MONEDA).Where(p => p.UNID_MONEDA == NewMaster.UNID_MONEDA).ToList();
                    NewMaster.UNID_USUARIO_CREADOR = CreatorUser[0].UNID_USUARIO_MOD;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
                else
                {
                    NewMaster.UNID_USUARIO_CREADOR = u.UNID_USUARIO;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
            }
        }

        private static void Master_PAIS(PAI poco, USUARIO u, int num, string control)
        {
            using (var entity = new TAE2Entities())
            {
                var Master = entity.MASTER_INVENTARIOS.OrderBy(p => p.UNID_MASTER_INVENTARIOS).Where(p => p.UNID_PAIS != null && p.UNID_PAIS == poco.UNID_PAIS).ToList();
                var NewMaster = new MASTER_INVENTARIOS();

                NewMaster.UNID_MASTER_INVENTARIOS = UNID.getNewUNID();
                NewMaster.UNID_PAIS = poco.UNID_PAIS;
                NewMaster.UNID_USUARIO_MOD = u.UNID_USUARIO;
                NewMaster.MODIFICACIONES = control;
                NewMaster.FECHA = DateTime.Now;
                NewMaster.LAST_MODIFIED_DATE = UNID.getNewUNID();
                NewMaster.IS_ACTIVE = true;
                NewMaster.IS_MODIFIED = true;
                string aux = "";
                foreach (USUARIO_ROL r in u.USUARIO_ROL)
                    aux += r.ROL.ROL_NAME + ", ";
                if (!String.IsNullOrEmpty(aux))
                {
                    aux = aux.Substring(0, aux.Length - 2);
                    aux += ".";
                }
                NewMaster.ROLES = aux;

                if (Master != null && Master.Count > 0)
                {
                    var CreatorUser = entity.MASTER_INVENTARIOS.OrderByDescending(p => p.UNID_PAIS).Where(p => p.UNID_PAIS == NewMaster.UNID_PAIS).ToList();
                    NewMaster.UNID_USUARIO_CREADOR = CreatorUser[0].UNID_USUARIO_MOD;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
                else
                {
                    NewMaster.UNID_USUARIO_CREADOR = u.UNID_USUARIO;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
            }
        }

        private static void Master_PROYECTO(PROYECTO poco, USUARIO u, int num, string control)
        {
            using (var entity = new TAE2Entities())
            {
                var Master = entity.MASTER_INVENTARIOS.OrderBy(p => p.UNID_MASTER_INVENTARIOS).Where(p => p.UNID_PROYECTO != null && p.UNID_PROYECTO == poco.UNID_PROYECTO).ToList();
                var NewMaster = new MASTER_INVENTARIOS();

                NewMaster.UNID_MASTER_INVENTARIOS = UNID.getNewUNID();
                NewMaster.UNID_PROYECTO = poco.UNID_PROYECTO;
                NewMaster.UNID_USUARIO_MOD = u.UNID_USUARIO;
                NewMaster.MODIFICACIONES = control;
                NewMaster.FECHA = DateTime.Now;
                NewMaster.LAST_MODIFIED_DATE = UNID.getNewUNID();
                NewMaster.IS_ACTIVE = true;
                NewMaster.IS_MODIFIED = true;
                string aux = "";
                foreach (USUARIO_ROL r in u.USUARIO_ROL)
                    aux += r.ROL.ROL_NAME + ", ";
                if (!String.IsNullOrEmpty(aux))
                {
                    aux = aux.Substring(0, aux.Length - 2);
                    aux += ".";
                }
                NewMaster.ROLES = aux;

                if (Master != null && Master.Count > 0)
                {
                    var CreatorUser = entity.MASTER_INVENTARIOS.OrderByDescending(p => p.UNID_PROYECTO).Where(p => p.UNID_PROYECTO == NewMaster.UNID_PROYECTO).ToList();
                    NewMaster.UNID_USUARIO_CREADOR = CreatorUser[0].UNID_USUARIO_MOD;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
                else
                {
                    NewMaster.UNID_USUARIO_CREADOR = u.UNID_USUARIO;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
            }
        }

        private static void Master_SERVICIO(SERVICIO poco, USUARIO u, int num, string control)
        {
            using (var entity = new TAE2Entities())
            {
                var Master = entity.MASTER_INVENTARIOS.OrderBy(p => p.UNID_MASTER_INVENTARIOS).Where(p => p.UNID_SERVICIO != null && p.UNID_SERVICIO == poco.UNID_SERVICIO).ToList();
                var NewMaster = new MASTER_INVENTARIOS();

                NewMaster.UNID_MASTER_INVENTARIOS = UNID.getNewUNID();
                NewMaster.UNID_SERVICIO = poco.UNID_SERVICIO;
                NewMaster.UNID_USUARIO_MOD = u.UNID_USUARIO;
                NewMaster.MODIFICACIONES = control;
                NewMaster.FECHA = DateTime.Now;
                NewMaster.LAST_MODIFIED_DATE = UNID.getNewUNID();
                NewMaster.IS_ACTIVE = true;
                NewMaster.IS_MODIFIED = true;
                string aux = "";
                foreach (USUARIO_ROL r in u.USUARIO_ROL)
                    aux += r.ROL.ROL_NAME + ", ";
                if (!String.IsNullOrEmpty(aux))
                {
                    aux = aux.Substring(0, aux.Length - 2);
                    aux += ".";
                }
                NewMaster.ROLES = aux;

                if (Master != null && Master.Count > 0)
                {
                    var CreatorUser = entity.MASTER_INVENTARIOS.OrderByDescending(p => p.UNID_SERVICIO).Where(p => p.UNID_SERVICIO == NewMaster.UNID_SERVICIO).ToList();
                    NewMaster.UNID_USUARIO_CREADOR = CreatorUser[0].UNID_USUARIO_MOD;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
                else
                {
                    NewMaster.UNID_USUARIO_CREADOR = u.UNID_USUARIO;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
            }
        }

        private static void Master_SOLICITANTE(SOLICITANTE poco, USUARIO u, int num, string control)
        {
            using (var entity = new TAE2Entities())
            {
                var Master = entity.MASTER_INVENTARIOS.OrderBy(p => p.UNID_MASTER_INVENTARIOS).Where(p => p.UNID_SOLICITANTE != null && p.UNID_SOLICITANTE == poco.UNID_SOLICITANTE).ToList();
                var NewMaster = new MASTER_INVENTARIOS();

                NewMaster.UNID_MASTER_INVENTARIOS = UNID.getNewUNID();
                NewMaster.UNID_SOLICITANTE = poco.UNID_SOLICITANTE;
                NewMaster.UNID_USUARIO_MOD = u.UNID_USUARIO;
                NewMaster.MODIFICACIONES = control;
                NewMaster.FECHA = DateTime.Now;
                NewMaster.LAST_MODIFIED_DATE = UNID.getNewUNID();
                NewMaster.IS_ACTIVE = true;
                NewMaster.IS_MODIFIED = true;
                string aux = "";
                foreach (USUARIO_ROL r in u.USUARIO_ROL)
                    aux += r.ROL.ROL_NAME + ", ";
                if (!String.IsNullOrEmpty(aux))
                {
                    aux = aux.Substring(0, aux.Length - 2);
                    aux += ".";
                }
                NewMaster.ROLES = aux;

                if (Master != null && Master.Count > 0)
                {
                    var CreatorUser = entity.MASTER_INVENTARIOS.OrderByDescending(p => p.UNID_SOLICITANTE).Where(p => p.UNID_SOLICITANTE == NewMaster.UNID_SOLICITANTE).ToList();
                    NewMaster.UNID_USUARIO_CREADOR = CreatorUser[0].UNID_USUARIO_MOD;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
                else
                {
                    NewMaster.UNID_USUARIO_CREADOR = u.UNID_USUARIO;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
            }
        }

        private static void Master_TERMINOENVIO(TERMINO_ENVIO poco, USUARIO u, int num, string control)
        {
            using (var entity = new TAE2Entities())
            {
                var Master = entity.MASTER_INVENTARIOS.OrderBy(p => p.UNID_MASTER_INVENTARIOS).Where(p => p.UNID_TERMINO_ENVIO != null && p.UNID_TERMINO_ENVIO == poco.UNID_TERMINO_ENVIO).ToList();
                var NewMaster = new MASTER_INVENTARIOS();

                NewMaster.UNID_MASTER_INVENTARIOS = UNID.getNewUNID();
                NewMaster.UNID_TERMINO_ENVIO = poco.UNID_TERMINO_ENVIO;
                NewMaster.UNID_USUARIO_MOD = u.UNID_USUARIO;
                NewMaster.MODIFICACIONES = control;
                NewMaster.FECHA = DateTime.Now;
                NewMaster.LAST_MODIFIED_DATE = UNID.getNewUNID();
                NewMaster.IS_ACTIVE = true;
                NewMaster.IS_MODIFIED = true;
                string aux = "";
                foreach (USUARIO_ROL r in u.USUARIO_ROL)
                    aux += r.ROL.ROL_NAME + ", ";
                if (!String.IsNullOrEmpty(aux))
                {
                    aux = aux.Substring(0, aux.Length - 2);
                    aux += ".";
                }
                NewMaster.ROLES = aux;

                if (Master != null && Master.Count > 0)
                {
                    var CreatorUser = entity.MASTER_INVENTARIOS.OrderByDescending(p => p.UNID_TERMINO_ENVIO).Where(p => p.UNID_TERMINO_ENVIO == NewMaster.UNID_TERMINO_ENVIO).ToList();
                    NewMaster.UNID_USUARIO_CREADOR = CreatorUser[0].UNID_USUARIO_MOD;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
                else
                {
                    NewMaster.UNID_USUARIO_CREADOR = u.UNID_USUARIO;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
            }
        }

        private static void Master_TIPOCOTIZACION(TIPO_COTIZACION poco, USUARIO u, int num, string control)
        {
            using (var entity = new TAE2Entities())
            {
                var Master = entity.MASTER_INVENTARIOS.OrderBy(p => p.UNID_MASTER_INVENTARIOS).Where(p => p.UNID_TIPO_COTIZACION != null && p.UNID_TIPO_COTIZACION == poco.UNID_TIPO_COTIZACION).ToList();
                var NewMaster = new MASTER_INVENTARIOS();

                NewMaster.UNID_MASTER_INVENTARIOS = UNID.getNewUNID();
                NewMaster.UNID_TIPO_COTIZACION = poco.UNID_TIPO_COTIZACION;
                NewMaster.UNID_USUARIO_MOD = u.UNID_USUARIO;
                NewMaster.MODIFICACIONES = control;
                NewMaster.FECHA = DateTime.Now;
                NewMaster.LAST_MODIFIED_DATE = UNID.getNewUNID();
                NewMaster.IS_ACTIVE = true;
                NewMaster.IS_MODIFIED = true;
                string aux = "";
                foreach (USUARIO_ROL r in u.USUARIO_ROL)
                    aux += r.ROL.ROL_NAME + ", ";
                if (!String.IsNullOrEmpty(aux))
                {
                    aux = aux.Substring(0, aux.Length - 2);
                    aux += ".";
                }
                NewMaster.ROLES = aux;

                if (Master != null && Master.Count > 0)
                {
                    var CreatorUser = entity.MASTER_INVENTARIOS.OrderByDescending(p => p.UNID_TIPO_COTIZACION).Where(p => p.UNID_TIPO_COTIZACION == NewMaster.UNID_TIPO_COTIZACION).ToList();
                    NewMaster.UNID_USUARIO_CREADOR = CreatorUser[0].UNID_USUARIO_MOD;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
                else
                {
                    NewMaster.UNID_USUARIO_CREADOR = u.UNID_USUARIO;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
            }
        }

        private static void Master_TIPOEMPRESA(TIPO_EMPRESA poco, USUARIO u, int num, string control)
        {
            using (var entity = new TAE2Entities())
            {
                var Master = entity.MASTER_INVENTARIOS.OrderBy(p => p.UNID_MASTER_INVENTARIOS).Where(p => p.UNID_TIPO_EMPRESA != null && p.UNID_TIPO_EMPRESA == poco.UNID_TIPO_EMPRESA).ToList();
                var NewMaster = new MASTER_INVENTARIOS();

                NewMaster.UNID_MASTER_INVENTARIOS = UNID.getNewUNID();
                NewMaster.UNID_TIPO_EMPRESA = poco.UNID_TIPO_EMPRESA;
                NewMaster.UNID_USUARIO_MOD = u.UNID_USUARIO;
                NewMaster.MODIFICACIONES = control;
                NewMaster.FECHA = DateTime.Now;
                NewMaster.LAST_MODIFIED_DATE = UNID.getNewUNID();
                NewMaster.IS_ACTIVE = true;
                NewMaster.IS_MODIFIED = true;
                string aux = "";
                foreach (USUARIO_ROL r in u.USUARIO_ROL)
                    aux += r.ROL.ROL_NAME + ", ";
                if (!String.IsNullOrEmpty(aux))
                {
                    aux = aux.Substring(0, aux.Length - 2);
                    aux += ".";
                }
                NewMaster.ROLES = aux;

                if (Master != null && Master.Count > 0)
                {
                    var CreatorUser = entity.MASTER_INVENTARIOS.OrderByDescending(p => p.UNID_TIPO_EMPRESA).Where(p => p.UNID_TIPO_EMPRESA == NewMaster.UNID_TIPO_EMPRESA).ToList();
                    NewMaster.UNID_USUARIO_CREADOR = CreatorUser[0].UNID_USUARIO_MOD;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
                else
                {
                    NewMaster.UNID_USUARIO_CREADOR = u.UNID_USUARIO;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
            }
        }

        private static void Master_TIPOPEDIMENTO(TIPO_PEDIMENTO poco, USUARIO u, int num, string control)
        {
            using (var entity = new TAE2Entities())
            {
                var Master = entity.MASTER_INVENTARIOS.OrderBy(p => p.UNID_MASTER_INVENTARIOS).Where(p => p.UNID_TIPO_PEDIMENTO != null && p.UNID_TIPO_PEDIMENTO == poco.UNID_TIPO_PEDIMENTO).ToList();
                var NewMaster = new MASTER_INVENTARIOS();

                NewMaster.UNID_MASTER_INVENTARIOS = UNID.getNewUNID();
                NewMaster.UNID_TIPO_PEDIMENTO = poco.UNID_TIPO_PEDIMENTO;
                NewMaster.UNID_USUARIO_MOD = u.UNID_USUARIO;
                NewMaster.MODIFICACIONES = control;
                NewMaster.FECHA = DateTime.Now;
                NewMaster.LAST_MODIFIED_DATE = UNID.getNewUNID();
                NewMaster.IS_ACTIVE = true;
                NewMaster.IS_MODIFIED = true;
                string aux = "";
                foreach (USUARIO_ROL r in u.USUARIO_ROL)
                    aux += r.ROL.ROL_NAME + ", ";
                if (!String.IsNullOrEmpty(aux))
                {
                    aux = aux.Substring(0, aux.Length - 2);
                    aux += ".";
                }
                NewMaster.ROLES = aux;

                if (Master != null && Master.Count > 0)
                {
                    var CreatorUser = entity.MASTER_INVENTARIOS.OrderByDescending(p => p.UNID_TIPO_PEDIMENTO).Where(p => p.UNID_TIPO_PEDIMENTO == NewMaster.UNID_TIPO_PEDIMENTO).ToList();
                    NewMaster.UNID_USUARIO_CREADOR = CreatorUser[0].UNID_USUARIO_MOD;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
                else
                {
                    NewMaster.UNID_USUARIO_CREADOR = u.UNID_USUARIO;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
            }
        }

        private static void Master_TRANSPORTE(TRANSPORTE poco, USUARIO u, int num, string control)
        {
            using (var entity = new TAE2Entities())
            {
                var Master = entity.MASTER_INVENTARIOS.OrderBy(p => p.UNID_MASTER_INVENTARIOS).Where(p => p.UNID_TRANSPORTE != null && p.UNID_TRANSPORTE == poco.UNID_TRANSPORTE).ToList();
                var NewMaster = new MASTER_INVENTARIOS();

                NewMaster.UNID_MASTER_INVENTARIOS = UNID.getNewUNID();
                NewMaster.UNID_TRANSPORTE = poco.UNID_TRANSPORTE;
                NewMaster.UNID_USUARIO_MOD = u.UNID_USUARIO;
                NewMaster.MODIFICACIONES = control;
                NewMaster.FECHA = DateTime.Now;
                NewMaster.LAST_MODIFIED_DATE = UNID.getNewUNID();
                NewMaster.IS_ACTIVE = true;
                NewMaster.IS_MODIFIED = true;
                string aux = "";
                foreach (USUARIO_ROL r in u.USUARIO_ROL)
                    aux += r.ROL.ROL_NAME + ", ";
                if (!String.IsNullOrEmpty(aux))
                {
                    aux = aux.Substring(0, aux.Length - 2);
                    aux += ".";
                }
                NewMaster.ROLES = aux;

                if (Master != null && Master.Count > 0)
                {
                    var CreatorUser = entity.MASTER_INVENTARIOS.OrderByDescending(p => p.UNID_TRANSPORTE).Where(p => p.UNID_TRANSPORTE == NewMaster.UNID_TRANSPORTE).ToList();
                    NewMaster.UNID_USUARIO_CREADOR = CreatorUser[0].UNID_USUARIO_MOD;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
                else
                {
                    NewMaster.UNID_USUARIO_CREADOR = u.UNID_USUARIO;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
            }
        }

        private static void Master_UNIDAD(UNIDAD poco, USUARIO u, int num, string control)
        {
            using (var entity = new TAE2Entities())
            {
                var Master = entity.MASTER_INVENTARIOS.OrderBy(p => p.UNID_MASTER_INVENTARIOS).Where(p => p.UNID_UNIDAD != null && p.UNID_UNIDAD == poco.UNID_UNIDAD).ToList();
                var NewMaster = new MASTER_INVENTARIOS();

                NewMaster.UNID_MASTER_INVENTARIOS = UNID.getNewUNID();
                NewMaster.UNID_UNIDAD = poco.UNID_UNIDAD;
                NewMaster.UNID_USUARIO_MOD = u.UNID_USUARIO;
                NewMaster.MODIFICACIONES = control;
                NewMaster.FECHA = DateTime.Now;
                NewMaster.LAST_MODIFIED_DATE = UNID.getNewUNID();
                NewMaster.IS_ACTIVE = true;
                NewMaster.IS_MODIFIED = true;
                string aux = "";
                foreach (USUARIO_ROL r in u.USUARIO_ROL)
                    aux += r.ROL.ROL_NAME + ", ";
                if (!String.IsNullOrEmpty(aux))
                {
                    aux = aux.Substring(0, aux.Length - 2);
                    aux += ".";
                }
                NewMaster.ROLES = aux;

                if (Master != null && Master.Count > 0)
                {
                    var CreatorUser = entity.MASTER_INVENTARIOS.OrderByDescending(p => p.UNID_UNIDAD).Where(p => p.UNID_UNIDAD == NewMaster.UNID_UNIDAD).ToList();
                    NewMaster.UNID_USUARIO_CREADOR = CreatorUser[0].UNID_USUARIO_MOD;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
                else
                {
                    NewMaster.UNID_USUARIO_CREADOR = u.UNID_USUARIO;
                    entity.MASTER_INVENTARIOS.AddObject(NewMaster);
                    entity.SaveChanges();
                }
            }
        }

        #endregion
    }
}