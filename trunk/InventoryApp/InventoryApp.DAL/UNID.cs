using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                res += "0";
            if(aux.Length == 2)
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
    }
}
