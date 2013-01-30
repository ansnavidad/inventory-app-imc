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
using System.Windows.Input;

namespace InventoryApp.ViewModel.Recibo
{
    public class ModifyReciboViewModel :  AddReciboViewModel
    {
        private const int MovimientoRecibo = 16;

        #region Commands
        //ModifyRecibo
        public ICommand ModifyReciboCmd
        {
            get
            {
                if (_ModifyMovimientoCmd == null)
                {
                    _ModifyMovimientoCmd = new RelayCommand(p => this.AttemptModifyReciboCmd(), p => this.CanAttemptAddReciboCmd());
                }
                return _ModifyMovimientoCmd;
            }
        }
        private RelayCommand _ModifyMovimientoCmd;
        public ICommand ModifyReciboCmd2
        {
            get
            {
                if (_ModifyMovimientoCmd2 == null)
                {
                    _ModifyMovimientoCmd2 = new RelayCommand(p => this.AttemptModifyReciboCmd2(), p => this.CanAttemptAddReciboCmd2());
                }
                return _ModifyMovimientoCmd2;
            }
        }
        private RelayCommand _ModifyMovimientoCmd2;

        

        public void AttemptModifyReciboCmd()
        {
            this.SaveFactura();
            this.SaveRecibo();            
            this._CatalogReciboViewModel.updateCollection();
        }

        public bool CanAttemptAddReciboCmd()
        {
            if (Movimientos != null)
            {
                if (Movimientos.Count > 0)
                {
                    
                    bool? aux = Movimientos[0].Finished;

                    if (aux != null)
                    {
                        
                        if ((bool)aux){
                            ContB = false;
                            this.Msj2 = "Esta en modo de lectura ya que la captura del recbio ha finalizado, ningún cambio realizado, se reflejará.";
                        }else{
                            ContB = true;
                            this.Msj2 = "";
                        }
                    }                    
                }                
            }            
            
            if (!ContB)
                return false;

            bool canAttempt = false;

            if ((this.Facturas != null && this.Facturas.Count > 0) && (this.Movimientos != null && this.Movimientos.Count > 0) && this.SelectedSolicitante != null)
            {
                canAttempt = true;
            }

            return canAttempt;
        }       

        public void AttemptModifyReciboCmd2()
        {
            //if (System.Windows.MessageBox.Show("¿Está seguro que desea finalizar el recibo actual? No se podrá editar posteriormente.", "Captura de Recibo", System.Windows.MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.Yes)
            //{
                this.SaveFactura();
                this.SaveRecibo2();
                this._CatalogReciboViewModel.updateCollection();                
            //}
        }

        public bool CanAttemptAddReciboCmd2()
        {
            bool canAttempt = false;

            if (!ContB)
                return false;

            if ((this.Facturas != null && this.Facturas.Count > 0) && (this.Movimientos != null && this.Movimientos.Count > 0) && this.SelectedSolicitante != null)
            {
                canAttempt = true;
            }

            if (canAttempt)
            {

                foreach (Model.Recibo.MovimientoModel mm in this.Movimientos)
                {

                    foreach (ReciboItemModel rr in mm.Items)
                    {

                        if (string.IsNullOrEmpty(rr.Sku) || string.IsNullOrEmpty(rr.NumeroSerie))
                            return false;
                    }
                }
            }

            return canAttempt;
        } 

        #endregion

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
                this._DelFacturas = new List<long>();
                this._DelMovs = new List<long>();
                this.ContB = true;
                this.GetReciboData(this._CatalogReciboViewModel.SelectedRecibo.UnidRecibo);
                this.Facturas= this.GetFacturas(this._CatalogReciboViewModel.SelectedRecibo.UnidRecibo);
                
                foreach (FacturaCompraModel ff in Facturas)
                    ff.HasNotRecibo = false;

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
                    IsNew = false,
                    Moneda = new MonedaModel(null)
                    {
                        UnidMoneda = f.MONEDA.UNID_MONEDA,
                        MonedaName = f.MONEDA.MONEDA_NAME,
                        MonedaAbr = f.MONEDA.MONEDA_ABR
                    },
                    TC = f.TC,
                    NumeroFactura = f.FACTURA_NUMERO,
                    FacturaDetalle = new ObservableCollection<FacturaCompraDetalleModel>(),
                    Proveedor = new ProveedorModel(null)
                    {
                        UnidProveedor = f.PROVEEDOR.UNID_PROVEEDOR,
                        ProveedorName = f.PROVEEDOR.PROVEEDOR_NAME
                    },
                    PorIva = f.IVA_POR == null ? 0d : (double)f.IVA_POR,
                    NumeroPedimento = f.NUMERO_PEDIMENTO,
                    TipoPedimento = new TipoPedimentoModel(null) 
                    { 
                        UnidTipoPedimento = f.TIPO_PEDIMENTO.UNID_TIPO_PEDIMENTO,
                        TipoPedimentoName = f.TIPO_PEDIMENTO.TIPO_PEDIMENTO_NAME,
                        Clave = f.TIPO_PEDIMENTO.CLAVE,
                        Nota = f.TIPO_PEDIMENTO.NOTA,
                        Regimen = f.TIPO_PEDIMENTO.REGIMEN
                    }
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
                        }, 
                        Finished = m.FINISHED
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
                            Cantidad = md.ITEM.CANTIDAD,
                            ItemStatus = new ItemStatusModel(null) { ItemStatusName = md.ITEM.ITEM_STATUS.ITEM_STATUS_NAME, UnidItemStatus = md.ITEM.ITEM_STATUS.UNID_ITEM_STATUS },
                            Articulo = new ArticuloModel()
                            {
                                UnidArticulo = md.ITEM.ARTICULO.UNID_ARTICULO,
                                ArticuloName = md.ITEM.ARTICULO.ARTICULO1,
                                Categoria = md.ITEM.ARTICULO.CATEGORIA,
                                Equipo = md.ITEM.ARTICULO.EQUIPO,
                                Marca = md.ITEM.ARTICULO.MARCA,
                                Modelo = md.ITEM.ARTICULO.MODELO,
                                EquipoModel = new EquipoModel(null)
                                {

                                    EquipoName = md.ITEM.ARTICULO.EQUIPO.EQUIPO_NAME,
                                    UnidEquipo = md.ITEM.ARTICULO.EQUIPO.UNID_EQUIPO
                                }
                            },
                            FacturaDetalle = new FacturaCompraDetalleModel()
                            {

                                UnidFacturaCompraDetalle = md.ITEM.FACTURA_DETALLE.UNID_FACTURA_DETALE,
                                Factura = new FacturaCompraModel() {

                                    UnidFactura = md.ITEM.FACTURA_DETALLE.UNID_FACTURA,
                                    NumeroFactura = md.ITEM.FACTURA_DETALLE.FACTURA.FACTURA_NUMERO,
                                    NumeroPedimento = md.ITEM.FACTURA_DETALLE.FACTURA.NUMERO_PEDIMENTO,
                                    FechaFactura = (DateTime)md.ITEM.FACTURA_DETALLE.FACTURA.FECHA_FACTURA,
                                    PorIva = (double)md.ITEM.FACTURA_DETALLE.FACTURA.IVA_POR,
                                    TC = md.ITEM.FACTURA_DETALLE.FACTURA.TC,
                                    Moneda = new MonedaModel(null)
                                    {
                                        MonedaAbr = md.ITEM.FACTURA_DETALLE.FACTURA.MONEDA.MONEDA_ABR
                                    }
                                }, 
                                Cantidad = md.ITEM.FACTURA_DETALLE.CANTIDAD, 
                                CostoUnitario = md.ITEM.FACTURA_DETALLE.PRECIO_UNITARIO, 
                                CantidadElegida = md.ITEM.FACTURA_DETALLE.CANTIDAD
                            }
                        });

                        ObservableCollection<FacturaCompraDetalleModel> FacAux = new ObservableCollection<FacturaCompraDetalleModel>();

                        List<long> unids = new List<long>();

                        foreach(FACTURA_DETALLE df in md.ITEM.FACTURA_DETALLE.FACTURA.FACTURA_DETALLE){

                            if (!unids.Contains(df.UNID_FACTURA_DETALE))
                            {
                                FacturaCompraDetalleModel auxx = new FacturaCompraDetalleModel();
                                auxx.Cantidad = df.CANTIDAD;
                                auxx.CostoUnitario = df.PRECIO_UNITARIO;
                                FacAux.Add(auxx);
                                unids.Add(df.UNID_FACTURA_DETALE);
                            }
                        }
                        
                        mm.Factura = new FacturaCompraModel()
                        {
                            UnidFactura = md.ITEM.FACTURA_DETALLE.UNID_FACTURA,
                            NumeroFactura = md.ITEM.FACTURA_DETALLE.FACTURA.FACTURA_NUMERO,

                            NumeroPedimento = md.ITEM.FACTURA_DETALLE.FACTURA.NUMERO_PEDIMENTO,
                            FechaFactura = (DateTime)md.ITEM.FACTURA_DETALLE.FACTURA.FECHA_FACTURA,
                            PorIva = (double)md.ITEM.FACTURA_DETALLE.FACTURA.IVA_POR,
                            TC = md.ITEM.FACTURA_DETALLE.FACTURA.TC,
                            Moneda = new MonedaModel(null)
                            {
                                MonedaAbr = md.ITEM.FACTURA_DETALLE.FACTURA.MONEDA.MONEDA_ABR
                            }, 
                            FacturaDetalle = FacAux
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
                        UNID_PROVEEDOR=item.Proveedor.UnidProveedor,
                        NUMERO_PEDIMENTO=item.NumeroPedimento, 
                        UNID_TIPO_PEDIMENTO = item.TipoPedimento.UnidTipoPedimento,
                        TC = item.TC
                    });

                    //Generar Array para insertar/actualizar/eliminar las factura detalle
                    List<FACTURA_DETALLE> fds = new List<FACTURA_DETALLE>();
                    foreach (FacturaCompraDetalleModel det in item.FacturaDetalle)
                    {
                        fds.Add(det.ConvertToPoco(det));
                        fds[fds.Count - 1].UNID_FACTURA = item.UnidFactura;
                    }

                    if(fds.Count > 0)
                        fcdDm.udpateElements(fds);
                }
                //Borrar facturas
                fcdmp.deleteFacturas(this._DelFacturas);
            }//end foreach
        }//end func

        private void SaveRecibo()
        {
            ReciboDataMapper rdm = new ReciboDataMapper();
            rdm.udpateElement(new RECIBO()
            {
                UNID_SOLICITANTE=this.SelectedSolicitante.UnidSolicitante,
                PO=this.PO,
                UNID_RECIBO=this.UnidRecibo
            });

            rdm.LimpiarRecibo(this._DelMovs, this.UnidRecibo);

            foreach (InventoryApp.Model.Recibo.MovimientoModel mov in this.Movimientos)
            {
                //Agregar movimiento
                DAL.POCOS.MOVIMENTO movimiento = new MOVIMENTO()
                {
                    UNID_MOVIMIENTO = mov.UnidMovimiento
                    ,
                    FECHA_MOVIMIENTO = mov.FechaCaptura
                    ,
                    UNID_ALMACEN_DESTINO = (mov.DestinoAlmacen != null) ? mov.DestinoAlmacen.UnidAlmacen : (long?)null
                    ,
                    UNID_ALMACEN_PROCEDENCIA = (mov.OrigenAlmacen != null) ? mov.OrigenAlmacen.UnidAlmacen : (long?)null
                    ,
                    UNID_CLIENTE_PROCEDENCIA = (mov.OrigenCliente != null) ? mov.OrigenCliente.UnidCliente : (long?)null
                    ,
                    UNID_PROVEEDOR_PROCEDENCIA = (mov.OrigenProveedor != null) ? mov.OrigenProveedor.UnidProveedor : (long?)null
                    ,
                    TT = this.TroubleTicket
                    ,
                    UNID_TIPO_MOVIMIENTO = MovimientoRecibo
                    ,
                    IS_ACTIVE = true
                };
                MovimientoDataMapper movDataMapper = new MovimientoDataMapper();
                movDataMapper.udpateElementRecibo(movimiento);

                foreach (InventoryApp.Model.Recibo.ReciboItemModel item in mov.Items)
                {
                    long? aux = null;
                    if (item.ItemStatus != null)
                        aux = item.ItemStatus.UnidItemStatus;

                    //Agregar el item
                    DAL.POCOS.ITEM pItem = new DAL.POCOS.ITEM()
                    {
                        UNID_ITEM = item.UnidItem
                        ,
                        SKU = item.Sku
                        ,
                        NUMERO_SERIE = item.NumeroSerie
                        ,
                        UNID_ITEM_STATUS = aux
                        ,
                        COSTO_UNITARIO = item.CostoUnitario
                        ,
                        UNID_FACTURA_DETALE = item.FacturaDetalle.UnidFacturaCompraDetalle
                        ,
                        UNID_ARTICULO = item.Articulo.UnidArticulo
                        ,
                        PEDIMENTO_EXPO = item.PedimentoExpo
                        ,
                        PEDIMENTO_IMPO = item.PedimentoImpo
                        ,
                        CANTIDAD = item.Cantidad
                        ,
                        IS_ACTIVE = true
                    };
                    ItemDataMapper itemDataMapper = new ItemDataMapper();
                    itemDataMapper.updateElementRecibo(pItem);

                    //Agregar detalle del movimiento
                    DAL.POCOS.MOVIMIENTO_DETALLE movDetalle = new DAL.POCOS.MOVIMIENTO_DETALLE()
                    {
                        UNID_MOVIMIENTO = mov.UnidMovimiento
                        ,
                        UNID_ITEM = item.UnidItem
                        ,
                        UNID_MOVIMIENTO_DETALLE = item.UnidMovimientoDetalle
                        ,
                        CANTIDAD = item.Cantidad
                        ,
                        UNID_ITEM_STATUS = item.ItemStatus.UnidItemStatus
                        ,
                        IS_ACTIVE = true
                    };
                    MovimientoDetalleDataMapper mdDataMapper = new MovimientoDetalleDataMapper();
                    mdDataMapper.udpateElementRecibo(movDetalle);

                    //Actualizar el último movimiento
                    DAL.POCOS.ULTIMO_MOVIMIENTO ulitmoMovto = new DAL.POCOS.ULTIMO_MOVIMIENTO()
                    {
                        UNID_ITEM = item.UnidItem,
                        UNID_ALMACEN = mov.DestinoAlmacen.UnidAlmacen,
                        UNID_MOVIMIENTO_DETALLE = item.UnidMovimientoDetalle,
                        CANTIDAD = item.Cantidad,
                        UNID_ITEM_STATUS = item.ItemStatus.UnidItemStatus,
                        IS_ACTIVE = true
                    };
                    UltimoMovimientoDataMapper umDataMapper = new UltimoMovimientoDataMapper();
                    umDataMapper.udpateElement(ulitmoMovto);
                }

                //Agregar recibodetalle
                DAL.POCOS.RECIBO_MOVIMIENTO rm = new DAL.POCOS.RECIBO_MOVIMIENTO()
                {
                    UNID_RECIBO = this._UnidRecibo,
                    UNID_RECIBO_MOVIMIENTO = mov.UnidMovimiento,
                    UNID_MOVIMIENTO = mov.UnidMovimiento,
                    UNID_FACTURA = mov.Items.First().FacturaDetalle.Factura.UnidFactura,
                    IS_ACTIVE = true
                };
                ReciboMovimientoDataMapper rmDataMaper = new ReciboMovimientoDataMapper();
                rmDataMaper.udpateElementRecibo(rm);
            }
        }

        private void SaveRecibo2()
        {
            ReciboDataMapper rdm = new ReciboDataMapper();
            rdm.udpateElement(new RECIBO()
            {
                UNID_SOLICITANTE = this.SelectedSolicitante.UnidSolicitante,
                PO = this.PO,
                UNID_RECIBO = this.UnidRecibo
            });

            rdm.LimpiarRecibo(this._DelMovs, this.UnidRecibo);

            foreach (InventoryApp.Model.Recibo.MovimientoModel mov in this.Movimientos)
            {
                //Agregar movimiento
                DAL.POCOS.MOVIMENTO movimiento = new MOVIMENTO()
                {
                    UNID_MOVIMIENTO = mov.UnidMovimiento
                    ,
                    FECHA_MOVIMIENTO = mov.FechaCaptura
                    ,
                    UNID_ALMACEN_DESTINO = (mov.DestinoAlmacen != null) ? mov.DestinoAlmacen.UnidAlmacen : (long?)null
                    ,
                    UNID_ALMACEN_PROCEDENCIA = (mov.OrigenAlmacen != null) ? mov.OrigenAlmacen.UnidAlmacen : (long?)null
                    ,
                    UNID_CLIENTE_PROCEDENCIA = (mov.OrigenCliente != null) ? mov.OrigenCliente.UnidCliente : (long?)null
                    ,
                    UNID_PROVEEDOR_PROCEDENCIA = (mov.OrigenProveedor != null) ? mov.OrigenProveedor.UnidProveedor : (long?)null
                    ,
                    TT = this.TroubleTicket
                    ,
                    UNID_TIPO_MOVIMIENTO = MovimientoRecibo
                    ,
                    IS_ACTIVE = true
                    , 
                    FINISHED = true
                };
                MovimientoDataMapper movDataMapper = new MovimientoDataMapper();
                movDataMapper.udpateElementRecibo(movimiento);

                foreach (InventoryApp.Model.Recibo.ReciboItemModel item in mov.Items)
                {
                    long? aux = null;
                    if (item.ItemStatus != null)
                        aux = item.ItemStatus.UnidItemStatus;

                    //Agregar el item
                    DAL.POCOS.ITEM pItem = new DAL.POCOS.ITEM()
                    {
                        UNID_ITEM = item.UnidItem
                        ,
                        SKU = item.Sku
                        ,
                        NUMERO_SERIE = item.NumeroSerie
                        ,
                        UNID_ITEM_STATUS = aux
                        ,
                        COSTO_UNITARIO = item.CostoUnitario
                        ,
                        UNID_FACTURA_DETALE = item.FacturaDetalle.UnidFacturaCompraDetalle
                        ,
                        UNID_ARTICULO = item.Articulo.UnidArticulo
                        ,
                        PEDIMENTO_EXPO = item.PedimentoExpo
                        ,
                        PEDIMENTO_IMPO = item.PedimentoImpo
                        ,
                        CANTIDAD = item.Cantidad
                        ,
                        IS_ACTIVE = true
                    };
                    ItemDataMapper itemDataMapper = new ItemDataMapper();
                    itemDataMapper.updateElementRecibo(pItem);

                    //Agregar detalle del movimiento
                    DAL.POCOS.MOVIMIENTO_DETALLE movDetalle = new DAL.POCOS.MOVIMIENTO_DETALLE()
                    {
                        UNID_MOVIMIENTO = mov.UnidMovimiento
                        ,
                        UNID_ITEM = item.UnidItem
                        ,
                        UNID_MOVIMIENTO_DETALLE = item.UnidMovimientoDetalle
                        ,
                        CANTIDAD = item.Cantidad
                        ,
                        UNID_ITEM_STATUS = item.ItemStatus.UnidItemStatus
                        ,
                        IS_ACTIVE = true
                    };
                    MovimientoDetalleDataMapper mdDataMapper = new MovimientoDetalleDataMapper();
                    mdDataMapper.udpateElementRecibo(movDetalle);

                    //Actualizar el último movimiento
                    DAL.POCOS.ULTIMO_MOVIMIENTO ulitmoMovto = new DAL.POCOS.ULTIMO_MOVIMIENTO()
                    {
                        UNID_ITEM = item.UnidItem,
                        UNID_ALMACEN = mov.DestinoAlmacen.UnidAlmacen,
                        UNID_MOVIMIENTO_DETALLE = item.UnidMovimientoDetalle,
                        CANTIDAD = item.Cantidad,
                        UNID_ITEM_STATUS = item.ItemStatus.UnidItemStatus,
                        IS_ACTIVE = true
                    };
                    UltimoMovimientoDataMapper umDataMapper = new UltimoMovimientoDataMapper();
                    umDataMapper.udpateElement(ulitmoMovto);
                }

                //Agregar recibodetalle
                DAL.POCOS.RECIBO_MOVIMIENTO rm = new DAL.POCOS.RECIBO_MOVIMIENTO()
                {
                    UNID_RECIBO = this._UnidRecibo,
                    UNID_RECIBO_MOVIMIENTO = mov.UnidMovimiento,
                    UNID_MOVIMIENTO = mov.UnidMovimiento,
                    UNID_FACTURA = mov.Items.First().FacturaDetalle.Factura.UnidFactura,
                    IS_ACTIVE = true
                };
                ReciboMovimientoDataMapper rmDataMaper = new ReciboMovimientoDataMapper();
                rmDataMaper.udpateElementRecibo(rm);
            }
        }
    }
}
