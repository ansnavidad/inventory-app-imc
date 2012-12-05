using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.Recibo;
using InventoryApp.Model.Recibo;
using System.Collections.ObjectModel;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.Recibo
{
    public class ModifyReciboViewModel :  AddReciboViewModel
    {


        public ModifyReciboViewModel(CatalogReciboViewModel catalogReciboViewModel)
            : base(catalogReciboViewModel)
        {
            this.InitModifyReciboViewModel();
        }

        /// <summary>
        /// Funcion de inicializacion
        /// </summary>
        private void InitModifyReciboViewModel()
        {
            if (this._CatalogReciboViewModel.SelectedRecibo != null)
            {
                this.GetReciboData(this._CatalogReciboViewModel.SelectedRecibo.UnidRecibo);
                this.Facturas= this.GetFacturas(this._CatalogReciboViewModel.SelectedRecibo.UnidRecibo);
                this.Movimientos = this.GetMovimientos(this._CatalogReciboViewModel.SelectedRecibo.UnidRecibo);
            }
        }

        private void GetReciboData(long unidRecibo)
        {
            this._UnidRecibo = this._CatalogReciboViewModel.SelectedRecibo.UnidRecibo;
            this.FechaCreacion = this._CatalogReciboViewModel.SelectedRecibo.FechaCreacion;
            try
            {
                this.SelectedEmpresa = this.Empresas.FirstOrDefault(emp => emp.UnidEmpresa == this._CatalogReciboViewModel.SelectedRecibo.Solicitante.Empresa.UNID_EMPRESA);
            }
            catch (Exception)
            {
                ;
            }


            try
            {
                this.SelectedSolicitante = this.Solicitantes.FirstOrDefault(sol => sol.UnidSolicitante == this._CatalogReciboViewModel.SelectedRecibo.Solicitante.UnidSolicitante);
            }
            catch (Exception)
            {
                ;
            }

            this.PO = this._CatalogReciboViewModel.SelectedRecibo.PO;
            this.TroubleTicket = this._CatalogReciboViewModel.SelectedRecibo.TroubleTicket;
        }

        private ObservableCollection<FacturaCompraModel> GetFacturas(long unidRecibo)
        {
            RECIBO recibo = new RECIBO()
            {
                UNID_RECIBO=unidRecibo
            };

            FacturaCompraDataMapper fcDataMapper = new FacturaCompraDataMapper();

            List<FACTURA> facturaList=fcDataMapper.GetFacturaList(recibo);

            ObservableCollection<FacturaCompraModel> facturas = new ObservableCollection<FacturaCompraModel>();

            facturaList.ForEach(f => 
            {
                FacturaCompraModel fcm = new FacturaCompraModel()
                {
                    UnidFactura = f.UNID_FACTURA,
                    FechaFactura = (DateTime)f.FECHA_FACTURA,
                    IsActive = f.IS_ACTIVE,
                    IsChecked = false,
                    Moneda = new MonedaModel(null)
                    {
                        UnidMoneda = f.MONEDA.UNID_MONEDA,
                        MonedaName = f.MONEDA.MONEDA_NAME,
                        MonedaAbr = f.MONEDA.MONEDA_ABR
                    },
                    NumeroFactura = f.FACTURA_NUMERO,
                    FacturaDetalle = new ObservableCollection<FacturaCompraDetalleModel>(),
                    Proveedor = new ProveedorModel(null)
                    {
                        UnidProveedor=f.PROVEEDOR.UNID_PROVEEDOR,
                        ProveedorName=f.PROVEEDOR.PROVEEDOR_NAME
                    },
                    PorIva=f.IVA_POR==null?0d:(double)f.IVA_POR,
                    NumeroPedimento=f.NUMERO_PEDIMENTO
                };

                f.FACTURA_DETALLE.ToList().ForEach(fd => 
                {
                    fcm.FacturaDetalle.Add(new FacturaCompraDetalleModel()
                    {
                        UnidFacturaCompraDetalle = fd.UNID_FACTURA_DETALE,
                        Articulo = new ArticuloModel()
                        {
                            UnidArticulo = fd.ARTICULO.UNID_ARTICULO,
                            ArticuloName = fd.ARTICULO.ARTICULO1,
                            Categoria = fd.ARTICULO.CATEGORIA,
                            Equipo = fd.ARTICULO.EQUIPO,
                            EquipoModel = new EquipoModel(null)
                            {
                                UnidEquipo = fd.ARTICULO.EQUIPO.UNID_EQUIPO,
                                EquipoName = fd.ARTICULO.EQUIPO.EQUIPO_NAME
                            },
                            Marca = fd.ARTICULO.MARCA,
                            Modelo = fd.ARTICULO.MODELO
                        },
                        Cantidad = fd.CANTIDAD,
                        Descripcion = fd.DESCRIPCION,
                        Factura = fcm,
                        ImpuestoUnitario = fcm.PorIva,
                        IsActive = fd.IS_ACTIVE,
                        Numero = fd.NUMERO,
                        CostoUnitario = fd.PRECIO_UNITARIO,
                        Unidad = new UnidadModel(null)
                        {
                            UnidUnidad = fd.UNIDAD.UNID_UNIDAD,
                            UnidadName = fd.UNIDAD.UNIDAD1
                        }
                    });
                });

                facturas.Add(fcm);
            });//factura foreach

            return facturas;
        }

        private ObservableCollection<Model.Recibo.MovimientoModel> GetMovimientos(long unidRecibo)
        {
            ObservableCollection<Model.Recibo.MovimientoModel> recibos = new ObservableCollection<Model.Recibo.MovimientoModel>();

            try
            {
                ReciboDataMapper recDataMapper = new ReciboDataMapper();
                List<MOVIMENTO> listMovtos = recDataMapper.GetListMovimiento(new RECIBO() { UNID_RECIBO=unidRecibo });
                listMovtos.ForEach(m =>
                {
                    Model.Recibo.MovimientoModel mm = new Model.Recibo.MovimientoModel()
                    {
                        UnidMovimiento = m.UNID_MOVIMIENTO,
                        OrigenProveedor = new OrigenProveedorModel()
                        {
                            UnidProveedor = m.PROVEEDOR.UNID_PROVEEDOR,
                            ProveedorName = m.PROVEEDOR.PROVEEDOR_NAME
                        },
                        DestinoAlmacen = new Model.Recibo.AlmacenModel()
                        {
                            UnidAlmacen = m.ALMACEN.UNID_ALMACEN,
                            AlmacenName = m.ALMACEN.ALMACEN_NAME,
                            Recibe = m.ALMACEN.CONTACTO
                        },
                        FechaCaptura = m.FECHA_MOVIMIENTO,
                        Origen = new OrigenProveedorModel()
                        {
                            UnidProveedor = m.PROVEEDOR.UNID_PROVEEDOR,
                            ProveedorName = m.PROVEEDOR.PROVEEDOR_NAME
                        }
                    };

                    ObservableCollection<Model.Recibo.ReciboItemModel> items = new ObservableCollection<ReciboItemModel>();

                    m.MOVIMIENTO_DETALLE.ToList().ForEach(md =>
                    {
                        items.Add(new ReciboItemModel()
                        {
                            UnidMovimientoDetalle = md.UNID_MOVIMIENTO_DETALLE,
                            UnidMovimiento = md.UNID_MOVIMIENTO,
                            UnidItem = md.ITEM.UNID_ITEM,
                            Sku = md.ITEM.SKU,
                            NumeroSerie = md.ITEM.NUMERO_SERIE,
                            CostoUnitario = md.ITEM.COSTO_UNITARIO,
                            Articulo = new ArticuloModel()
                            {
                                UnidArticulo = md.ITEM.ARTICULO.UNID_ARTICULO,
                                ArticuloName = md.ITEM.ARTICULO.ARTICULO1,
                                Categoria = md.ITEM.ARTICULO.CATEGORIA,
                                Equipo = md.ITEM.ARTICULO.EQUIPO,
                                Marca = md.ITEM.ARTICULO.MARCA,
                                Modelo = md.ITEM.ARTICULO.MODELO
                            }
                        });

                        mm.Factura = new FacturaCompraModel()
                        {
                            UnidFactura = md.ITEM.FACTURA_DETALLE.UNID_FACTURA,
                            NumeroFactura=md.ITEM.FACTURA_DETALLE.FACTURA.FACTURA_NUMERO
                        };
                    });

                    mm.Items = items;
                    recibos.Add(mm);
                });
            }
            catch (Exception)
            {
                ;
            }

            return recibos;
        }

        private void SaveFactura()
        {
            LoteModel lot=null;
            FacturaCompraDataMapper fcdmp = new FacturaCompraDataMapper();
            FacturaCompraDetalleDataMapper fcdDm = new FacturaCompraDetalleDataMapper();

            //Guardar
            foreach (FacturaCompraModel item in this.Facturas)
            {
                
                if (item.IsNew)
                {
                    //insertar la nueva factura
                    if (lot == null)
                    {
                        lot = new LoteModel(new LoteDataMapper());
                        lot.UnidLote = UNID.getNewUNID();
                        lot.UnidPOM = UNID.getNewUNID();
                        lot.saveLote();
                    }
                    
                    item.UnidLote = lot.UnidLote;
                    item.UnidFactura = UNID.getNewUNID();
                    item.saveFactura();
                    //factura detalle
                    foreach (FacturaCompraDetalleModel fac in item.FacturaDetalle)
                    {
                        fac.Factura = item;
                        fac.saveFacturaDetalle();
                    }
                }
                else
                {
                    //Actualizar Factura
                    fcdmp.udpateElement(new FACTURA()
                    {
                        UNID_FACTURA=item.UnidFactura,
                        FACTURA_NUMERO=item.NumeroFactura,
                        FECHA_FACTURA=item.FechaFactura,
                        IS_ACTIVE=item.IsActive,
                        IVA_POR=item.PorIva,
                        UNID_MONEDA=item.Moneda.UnidMoneda,
                        UNID_PROVEEDOR=item.Proveedor.UnidProveedor
                    });

                    //Generar Array para insertar/actualizar/eliminar las facturas
                    List<FACTURA_DETALLE> fds = new List<FACTURA_DETALLE>();
                    foreach (FacturaCompraDetalleModel det in item.FacturaDetalle)
                    {
                        fds.Add(det.ConvertToPoco(det));
                    }

                    fcdDm.udpateElements(fds);
                }
            }//end foreach
        }//end func
    }
}
