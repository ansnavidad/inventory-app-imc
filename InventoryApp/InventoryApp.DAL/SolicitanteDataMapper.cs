using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
     public class SolicitanteDataMapper : IDataMapper 
    {
        public object getElements()
        {
            throw new NotImplementedException();
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
            using (var entitie = new TAE2Entities())
            {
                SOLICITANTE Esolicitate = (SOLICITANTE)element;
                SOLICITANTE soliciatante = new SOLICITANTE();
                soliciatante.UNID_SOLICITANTE = soliciatante.UNID_SOLICITANTE;
                soliciatante.SOLICITANTE_NAME = soliciatante.SOLICITANTE_NAME;
                soliciatante.UNID_EMPRESA = soliciatante.UNID_EMPRESA;
                soliciatante.UNID_DEPARTAMENTO = soliciatante.UNID_DEPARTAMENTO;
                soliciatante.EMAIL = soliciatante.EMAIL;
                soliciatante.VALIDADOR = soliciatante.VALIDADOR;
                entitie.SOLICITANTEs.AddObject(soliciatante);
                entitie.SaveChanges();
            }
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}
