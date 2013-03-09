using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.Model.InventarioFisico
{
    public class InventariosFisicosModel : ObservableCollection<InventarioFisicoModel>,IModelDataConverter
    {
        private IDataMapper _DataMapper;
        
        public InventariosFisicosModel(IDataMapper dataMapper)
        {
            this.Init();
            this._DataMapper = dataMapper;
            this.Load();
        }

        public void Init()
        {
            this._DataMapper = null;
        }

        public void Load()
        {
            if (this._DataMapper != null)
            {
                InventarioFisicoDataMapper dm = this._DataMapper as InventarioFisicoDataMapper;
                if (dm != null)
                {
                    object elements = dm.getElementsView();
                    this.ConvertBack(elements);
                }
            }
        }

        public object ConvertTo()
        {
            List<VW_GET_INVENTARIO_FISICO> pocos = new List<VW_GET_INVENTARIO_FISICO>();
            if (pocos != null)
            {
                foreach (InventarioFisicoModel inv in this)
                {
                    pocos.Add(
                            new VW_GET_INVENTARIO_FISICO()
                            {
                                UNID_ALMACEN=inv.Almacen.UnidAlmacen,
                                UNID_INVENTARIO_FISICO=inv.unid,
                                IS_FINALIZADO=inv.Status ,
                                FECHA_INICIO=inv.FechaIni ,
                                FECHA_FIN = inv.FechaFin 
                            }
                        );
                }//fin foreach
            }//fin if

            return pocos;
        }

        public object ConvertBack(object data)
        {
            //Convierte de una lista de POCOs a una lista de objeto inventario fisico model
            //List<InventarioFisicoModel> inventarios = new List<InventarioFisicoModel>();
            List<VW_GET_INVENTARIO_FISICO> pocos = data as List<VW_GET_INVENTARIO_FISICO>;
            if (pocos != null)
            {
                foreach (VW_GET_INVENTARIO_FISICO inv in pocos)
                {
                    this.Add(
                            new InventarioFisicoModel()
                            {
                                Almacen = new AlmacenModel(null,null)
                                {
                                     UnidAlmacen=inv.UNID_ALMACEN
                                     //AlmacenName=inv.ALMACEN.ALMACEN_NAME
                                },
                                unid=inv.UNID_INVENTARIO_FISICO,
                                Status=inv.IS_FINALIZADO,
                                FechaIni=(DateTime)inv.FECHA_INICIO,
                                FechaFin = (DateTime)inv.FECHA_FIN
                            }
                        );
                }//fin foreach
            }//fin if

            return this;
        }
    }
}
