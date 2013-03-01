﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.EntityClient;

namespace InventoryApp.DAL.POCOS
{
    public partial class TAE2Entities : ObjectContext
    {
        public const string ConnectionString = "name=TAE2Entities";
        public const string ContainerName = "TAE2Entities";
    
        #region Constructors
    
        public TAE2Entities()
            : base(ConnectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        public TAE2Entities(string connectionString)
            : base(connectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        public TAE2Entities(EntityConnection connection)
            : base(connection, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = true;
        }
    
        #endregion
    
        #region ObjectSet Properties
    
        public ObjectSet<MASTER_INVENTARIOS> MASTER_INVENTARIOS
        {
            get { return _mASTER_INVENTARIOS  ?? (_mASTER_INVENTARIOS = CreateObjectSet<MASTER_INVENTARIOS>("MASTER_INVENTARIOS")); }
        }
        private ObjectSet<MASTER_INVENTARIOS> _mASTER_INVENTARIOS;
    
        public ObjectSet<MENU> MENUs
        {
            get { return _mENUs  ?? (_mENUs = CreateObjectSet<MENU>("MENUs")); }
        }
        private ObjectSet<MENU> _mENUs;
    
        public ObjectSet<ROL> ROLs
        {
            get { return _rOLs  ?? (_rOLs = CreateObjectSet<ROL>("ROLs")); }
        }
        private ObjectSet<ROL> _rOLs;
    
        public ObjectSet<ROL_MENU> ROL_MENU
        {
            get { return _rOL_MENU  ?? (_rOL_MENU = CreateObjectSet<ROL_MENU>("ROL_MENU")); }
        }
        private ObjectSet<ROL_MENU> _rOL_MENU;
    
        public ObjectSet<USUARIO> USUARIOs
        {
            get { return _uSUARIOs  ?? (_uSUARIOs = CreateObjectSet<USUARIO>("USUARIOs")); }
        }
        private ObjectSet<USUARIO> _uSUARIOs;
    
        public ObjectSet<USUARIO_ROL> USUARIO_ROL
        {
            get { return _uSUARIO_ROL  ?? (_uSUARIO_ROL = CreateObjectSet<USUARIO_ROL>("USUARIO_ROL")); }
        }
        private ObjectSet<USUARIO_ROL> _uSUARIO_ROL;
    
        public ObjectSet<ARTICULO> ARTICULOes
        {
            get { return _aRTICULOes  ?? (_aRTICULOes = CreateObjectSet<ARTICULO>("ARTICULOes")); }
        }
        private ObjectSet<ARTICULO> _aRTICULOes;
    
        public ObjectSet<CATEGORIA> CATEGORIAs
        {
            get { return _cATEGORIAs  ?? (_cATEGORIAs = CreateObjectSet<CATEGORIA>("CATEGORIAs")); }
        }
        private ObjectSet<CATEGORIA> _cATEGORIAs;
    
        public ObjectSet<EQUIPO> EQUIPOes
        {
            get { return _eQUIPOes  ?? (_eQUIPOes = CreateObjectSet<EQUIPO>("EQUIPOes")); }
        }
        private ObjectSet<EQUIPO> _eQUIPOes;
    
        public ObjectSet<MARCA> MARCAs
        {
            get { return _mARCAs  ?? (_mARCAs = CreateObjectSet<MARCA>("MARCAs")); }
        }
        private ObjectSet<MARCA> _mARCAs;
    
        public ObjectSet<MODELO> MODELOes
        {
            get { return _mODELOes  ?? (_mODELOes = CreateObjectSet<MODELO>("MODELOes")); }
        }
        private ObjectSet<MODELO> _mODELOes;
    
        public ObjectSet<TEST> TESTs
        {
            get { return _tESTs  ?? (_tESTs = CreateObjectSet<TEST>("TESTs")); }
        }
        private ObjectSet<TEST> _tESTs;
    
        public ObjectSet<BANCO> BANCOes
        {
            get { return _bANCOes  ?? (_bANCOes = CreateObjectSet<BANCO>("BANCOes")); }
        }
        private ObjectSet<BANCO> _bANCOes;
    
        public ObjectSet<DEPARTAMENTO> DEPARTAMENTOes
        {
            get { return _dEPARTAMENTOes  ?? (_dEPARTAMENTOes = CreateObjectSet<DEPARTAMENTO>("DEPARTAMENTOes")); }
        }
        private ObjectSet<DEPARTAMENTO> _dEPARTAMENTOes;
    
        public ObjectSet<EMPRESA> EMPRESAs
        {
            get { return _eMPRESAs  ?? (_eMPRESAs = CreateObjectSet<EMPRESA>("EMPRESAs")); }
        }
        private ObjectSet<EMPRESA> _eMPRESAs;
    
        public ObjectSet<INFRAESTRUCTURA> INFRAESTRUCTURAs
        {
            get { return _iNFRAESTRUCTURAs  ?? (_iNFRAESTRUCTURAs = CreateObjectSet<INFRAESTRUCTURA>("INFRAESTRUCTURAs")); }
        }
        private ObjectSet<INFRAESTRUCTURA> _iNFRAESTRUCTURAs;
    
        public ObjectSet<MEDIO_ENVIO> MEDIO_ENVIO
        {
            get { return _mEDIO_ENVIO  ?? (_mEDIO_ENVIO = CreateObjectSet<MEDIO_ENVIO>("MEDIO_ENVIO")); }
        }
        private ObjectSet<MEDIO_ENVIO> _mEDIO_ENVIO;
    
        public ObjectSet<MONEDA> MONEDAs
        {
            get { return _mONEDAs  ?? (_mONEDAs = CreateObjectSet<MONEDA>("MONEDAs")); }
        }
        private ObjectSet<MONEDA> _mONEDAs;
    
        public ObjectSet<PROCESS_BATCH> PROCESS_BATCH
        {
            get { return _pROCESS_BATCH  ?? (_pROCESS_BATCH = CreateObjectSet<PROCESS_BATCH>("PROCESS_BATCH")); }
        }
        private ObjectSet<PROCESS_BATCH> _pROCESS_BATCH;
    
        public ObjectSet<PROVEEDOR> PROVEEDORs
        {
            get { return _pROVEEDORs  ?? (_pROVEEDORs = CreateObjectSet<PROVEEDOR>("PROVEEDORs")); }
        }
        private ObjectSet<PROVEEDOR> _pROVEEDORs;
    
        public ObjectSet<PROVEEDOR_CATEGORIA> PROVEEDOR_CATEGORIA
        {
            get { return _pROVEEDOR_CATEGORIA  ?? (_pROVEEDOR_CATEGORIA = CreateObjectSet<PROVEEDOR_CATEGORIA>("PROVEEDOR_CATEGORIA")); }
        }
        private ObjectSet<PROVEEDOR_CATEGORIA> _pROVEEDOR_CATEGORIA;
    
        public ObjectSet<PROVEEDOR_CUENTA> PROVEEDOR_CUENTA
        {
            get { return _pROVEEDOR_CUENTA  ?? (_pROVEEDOR_CUENTA = CreateObjectSet<PROVEEDOR_CUENTA>("PROVEEDOR_CUENTA")); }
        }
        private ObjectSet<PROVEEDOR_CUENTA> _pROVEEDOR_CUENTA;
    
        public ObjectSet<PROYECTO> PROYECTOes
        {
            get { return _pROYECTOes  ?? (_pROYECTOes = CreateObjectSet<PROYECTO>("PROYECTOes")); }
        }
        private ObjectSet<PROYECTO> _pROYECTOes;
    
        public ObjectSet<SERVER_LASTDATA> SERVER_LASTDATA
        {
            get { return _sERVER_LASTDATA  ?? (_sERVER_LASTDATA = CreateObjectSet<SERVER_LASTDATA>("SERVER_LASTDATA")); }
        }
        private ObjectSet<SERVER_LASTDATA> _sERVER_LASTDATA;
    
        public ObjectSet<SOLICITANTE> SOLICITANTEs
        {
            get { return _sOLICITANTEs  ?? (_sOLICITANTEs = CreateObjectSet<SOLICITANTE>("SOLICITANTEs")); }
        }
        private ObjectSet<SOLICITANTE> _sOLICITANTEs;
    
        public ObjectSet<SYNC> SYNCs
        {
            get { return _sYNCs  ?? (_sYNCs = CreateObjectSet<SYNC>("SYNCs")); }
        }
        private ObjectSet<SYNC> _sYNCs;
    
        public ObjectSet<TERMINO_ENVIO> TERMINO_ENVIO
        {
            get { return _tERMINO_ENVIO  ?? (_tERMINO_ENVIO = CreateObjectSet<TERMINO_ENVIO>("TERMINO_ENVIO")); }
        }
        private ObjectSet<TERMINO_ENVIO> _tERMINO_ENVIO;
    
        public ObjectSet<TIPO_COTIZACION> TIPO_COTIZACION
        {
            get { return _tIPO_COTIZACION  ?? (_tIPO_COTIZACION = CreateObjectSet<TIPO_COTIZACION>("TIPO_COTIZACION")); }
        }
        private ObjectSet<TIPO_COTIZACION> _tIPO_COTIZACION;
    
        public ObjectSet<TIPO_EMPRESA> TIPO_EMPRESA
        {
            get { return _tIPO_EMPRESA  ?? (_tIPO_EMPRESA = CreateObjectSet<TIPO_EMPRESA>("TIPO_EMPRESA")); }
        }
        private ObjectSet<TIPO_EMPRESA> _tIPO_EMPRESA;
    
        public ObjectSet<TIPO_PEDIMENTO> TIPO_PEDIMENTO
        {
            get { return _tIPO_PEDIMENTO  ?? (_tIPO_PEDIMENTO = CreateObjectSet<TIPO_PEDIMENTO>("TIPO_PEDIMENTO")); }
        }
        private ObjectSet<TIPO_PEDIMENTO> _tIPO_PEDIMENTO;
    
        public ObjectSet<TRANSPORTE> TRANSPORTEs
        {
            get { return _tRANSPORTEs  ?? (_tRANSPORTEs = CreateObjectSet<TRANSPORTE>("TRANSPORTEs")); }
        }
        private ObjectSet<TRANSPORTE> _tRANSPORTEs;
    
        public ObjectSet<UPDATE> UPDATEs
        {
            get { return _uPDATEs  ?? (_uPDATEs = CreateObjectSet<UPDATE>("UPDATEs")); }
        }
        private ObjectSet<UPDATE> _uPDATEs;
    
        public ObjectSet<UPLOAD_LOG> UPLOAD_LOG
        {
            get { return _uPLOAD_LOG  ?? (_uPLOAD_LOG = CreateObjectSet<UPLOAD_LOG>("UPLOAD_LOG")); }
        }
        private ObjectSet<UPLOAD_LOG> _uPLOAD_LOG;
    
        public ObjectSet<ALMACEN> ALMACENs
        {
            get { return _aLMACENs  ?? (_aLMACENs = CreateObjectSet<ALMACEN>("ALMACENs")); }
        }
        private ObjectSet<ALMACEN> _aLMACENs;
    
        public ObjectSet<ALMACEN_TECNICO> ALMACEN_TECNICO
        {
            get { return _aLMACEN_TECNICO  ?? (_aLMACEN_TECNICO = CreateObjectSet<ALMACEN_TECNICO>("ALMACEN_TECNICO")); }
        }
        private ObjectSet<ALMACEN_TECNICO> _aLMACEN_TECNICO;
    
        public ObjectSet<CLIENTE> CLIENTEs
        {
            get { return _cLIENTEs  ?? (_cLIENTEs = CreateObjectSet<CLIENTE>("CLIENTEs")); }
        }
        private ObjectSet<CLIENTE> _cLIENTEs;
    
        public ObjectSet<ITEM_STATUS> ITEM_STATUS
        {
            get { return _iTEM_STATUS  ?? (_iTEM_STATUS = CreateObjectSet<ITEM_STATUS>("ITEM_STATUS")); }
        }
        private ObjectSet<ITEM_STATUS> _iTEM_STATUS;
    
        public ObjectSet<MAX_MIN> MAX_MIN
        {
            get { return _mAX_MIN  ?? (_mAX_MIN = CreateObjectSet<MAX_MIN>("MAX_MIN")); }
        }
        private ObjectSet<MAX_MIN> _mAX_MIN;
    
        public ObjectSet<PROGRAMADO> PROGRAMADOes
        {
            get { return _pROGRAMADOes  ?? (_pROGRAMADOes = CreateObjectSet<PROGRAMADO>("PROGRAMADOes")); }
        }
        private ObjectSet<PROGRAMADO> _pROGRAMADOes;
    
        public ObjectSet<PROPIEDAD> PROPIEDADs
        {
            get { return _pROPIEDADs  ?? (_pROPIEDADs = CreateObjectSet<PROPIEDAD>("PROPIEDADs")); }
        }
        private ObjectSet<PROPIEDAD> _pROPIEDADs;
    
        public ObjectSet<SERVICIO> SERVICIOs
        {
            get { return _sERVICIOs  ?? (_sERVICIOs = CreateObjectSet<SERVICIO>("SERVICIOs")); }
        }
        private ObjectSet<SERVICIO> _sERVICIOs;
    
        public ObjectSet<SOLICITANTE1> SOLICITANTE1
        {
            get { return _sOLICITANTE1  ?? (_sOLICITANTE1 = CreateObjectSet<SOLICITANTE1>("SOLICITANTE1")); }
        }
        private ObjectSet<SOLICITANTE1> _sOLICITANTE1;
    
        public ObjectSet<TECNICO> TECNICOes
        {
            get { return _tECNICOes  ?? (_tECNICOes = CreateObjectSet<TECNICO>("TECNICOes")); }
        }
        private ObjectSet<TECNICO> _tECNICOes;
    
        public ObjectSet<TIPO_MOVIMIENTO> TIPO_MOVIMIENTO
        {
            get { return _tIPO_MOVIMIENTO  ?? (_tIPO_MOVIMIENTO = CreateObjectSet<TIPO_MOVIMIENTO>("TIPO_MOVIMIENTO")); }
        }
        private ObjectSet<TIPO_MOVIMIENTO> _tIPO_MOVIMIENTO;
    
        public ObjectSet<UNIDAD> UNIDADs
        {
            get { return _uNIDADs  ?? (_uNIDADs = CreateObjectSet<UNIDAD>("UNIDADs")); }
        }
        private ObjectSet<UNIDAD> _uNIDADs;
    
        public ObjectSet<COTIZACION> COTIZACIONs
        {
            get { return _cOTIZACIONs  ?? (_cOTIZACIONs = CreateObjectSet<COTIZACION>("COTIZACIONs")); }
        }
        private ObjectSet<COTIZACION> _cOTIZACIONs;
    
        public ObjectSet<BATCH_LOAD_CARGAMOV> BATCH_LOAD_CARGAMOV
        {
            get { return _bATCH_LOAD_CARGAMOV  ?? (_bATCH_LOAD_CARGAMOV = CreateObjectSet<BATCH_LOAD_CARGAMOV>("BATCH_LOAD_CARGAMOV")); }
        }
        private ObjectSet<BATCH_LOAD_CARGAMOV> _bATCH_LOAD_CARGAMOV;
    
        public ObjectSet<LOG_LOAD_CARGAMOV> LOG_LOAD_CARGAMOV
        {
            get { return _lOG_LOAD_CARGAMOV  ?? (_lOG_LOAD_CARGAMOV = CreateObjectSet<LOG_LOAD_CARGAMOV>("LOG_LOAD_CARGAMOV")); }
        }
        private ObjectSet<LOG_LOAD_CARGAMOV> _lOG_LOAD_CARGAMOV;
    
        public ObjectSet<CIUDAD> CIUDADs
        {
            get { return _cIUDADs  ?? (_cIUDADs = CreateObjectSet<CIUDAD>("CIUDADs")); }
        }
        private ObjectSet<CIUDAD> _cIUDADs;
    
        public ObjectSet<PAI> PAIS
        {
            get { return _pAIS  ?? (_pAIS = CreateObjectSet<PAI>("PAIS")); }
        }
        private ObjectSet<PAI> _pAIS;
    
        public ObjectSet<FACTURA_VENTA> FACTURA_VENTA
        {
            get { return _fACTURA_VENTA  ?? (_fACTURA_VENTA = CreateObjectSet<FACTURA_VENTA>("FACTURA_VENTA")); }
        }
        private ObjectSet<FACTURA_VENTA> _fACTURA_VENTA;
    
        public ObjectSet<ITEM> ITEMs
        {
            get { return _iTEMs  ?? (_iTEMs = CreateObjectSet<ITEM>("ITEMs")); }
        }
        private ObjectSet<ITEM> _iTEMs;
    
        public ObjectSet<MOVIMENTO> MOVIMENTOes
        {
            get { return _mOVIMENTOes  ?? (_mOVIMENTOes = CreateObjectSet<MOVIMENTO>("MOVIMENTOes")); }
        }
        private ObjectSet<MOVIMENTO> _mOVIMENTOes;
    
        public ObjectSet<MOVIMIENTO_DETALLE> MOVIMIENTO_DETALLE
        {
            get { return _mOVIMIENTO_DETALLE  ?? (_mOVIMIENTO_DETALLE = CreateObjectSet<MOVIMIENTO_DETALLE>("MOVIMIENTO_DETALLE")); }
        }
        private ObjectSet<MOVIMIENTO_DETALLE> _mOVIMIENTO_DETALLE;
    
        public ObjectSet<RECIBO> RECIBOes
        {
            get { return _rECIBOes  ?? (_rECIBOes = CreateObjectSet<RECIBO>("RECIBOes")); }
        }
        private ObjectSet<RECIBO> _rECIBOes;
    
        public ObjectSet<RECIBO_MOVIMIENTO> RECIBO_MOVIMIENTO
        {
            get { return _rECIBO_MOVIMIENTO  ?? (_rECIBO_MOVIMIENTO = CreateObjectSet<RECIBO_MOVIMIENTO>("RECIBO_MOVIMIENTO")); }
        }
        private ObjectSet<RECIBO_MOVIMIENTO> _rECIBO_MOVIMIENTO;
    
        public ObjectSet<RECIBO_STATUS> RECIBO_STATUS
        {
            get { return _rECIBO_STATUS  ?? (_rECIBO_STATUS = CreateObjectSet<RECIBO_STATUS>("RECIBO_STATUS")); }
        }
        private ObjectSet<RECIBO_STATUS> _rECIBO_STATUS;
    
        public ObjectSet<ULTIMO_MOVIMIENTO> ULTIMO_MOVIMIENTO
        {
            get { return _uLTIMO_MOVIMIENTO  ?? (_uLTIMO_MOVIMIENTO = CreateObjectSet<ULTIMO_MOVIMIENTO>("ULTIMO_MOVIMIENTO")); }
        }
        private ObjectSet<ULTIMO_MOVIMIENTO> _uLTIMO_MOVIMIENTO;
    
        public ObjectSet<FACTURA> FACTURAs
        {
            get { return _fACTURAs  ?? (_fACTURAs = CreateObjectSet<FACTURA>("FACTURAs")); }
        }
        private ObjectSet<FACTURA> _fACTURAs;
    
        public ObjectSet<FACTURA_DETALLE> FACTURA_DETALLE
        {
            get { return _fACTURA_DETALLE  ?? (_fACTURA_DETALLE = CreateObjectSet<FACTURA_DETALLE>("FACTURA_DETALLE")); }
        }
        private ObjectSet<FACTURA_DETALLE> _fACTURA_DETALLE;
    
        public ObjectSet<LOTE> LOTEs
        {
            get { return _lOTEs  ?? (_lOTEs = CreateObjectSet<LOTE>("LOTEs")); }
        }
        private ObjectSet<LOTE> _lOTEs;
    
        public ObjectSet<PEDIMENTO> PEDIMENTOes
        {
            get { return _pEDIMENTOes  ?? (_pEDIMENTOes = CreateObjectSet<PEDIMENTO>("PEDIMENTOes")); }
        }
        private ObjectSet<PEDIMENTO> _pEDIMENTOes;
    
        public ObjectSet<POM> POMs
        {
            get { return _pOMs  ?? (_pOMs = CreateObjectSet<POM>("POMs")); }
        }
        private ObjectSet<POM> _pOMs;
    
        public ObjectSet<POM_ARTICULO> POM_ARTICULO
        {
            get { return _pOM_ARTICULO  ?? (_pOM_ARTICULO = CreateObjectSet<POM_ARTICULO>("POM_ARTICULO")); }
        }
        private ObjectSet<POM_ARTICULO> _pOM_ARTICULO;

        #endregion

        #region Function Imports
        public ObjectResult<TEST> SP_TAE2_JOB()
        {
            return base.ExecuteFunction<TEST>("SP_TAE2_JOB");
        }

        public void GetJob()
        {

            base.ExecuteFunction("SP_TAE2_JOB");
        }

        #endregion
    }
}
