using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventoryApp.Model
{
    public class PedimentoModel:ModelBase
    {
        public long UnidPedimentoModel
        {
            get { return _UnidPedimentoModel; }
            set
            {
                if (_UnidPedimentoModel != value)
                {
                    _UnidPedimentoModel = value;
                    OnPropertyChanged(UnidPedimentoModelPropertyName);
                }
            }
        }
        private long _UnidPedimentoModel;
        public const string UnidPedimentoModelPropertyName = "UnidPedimentoModel";

        public string PedimentoNumero
        {
            get { return _PedimentoNumero; }
            set
            {
                if (_PedimentoNumero != value)
                {
                    _PedimentoNumero = value;
                    OnPropertyChanged(PedimentoNumeroPropertyName);
                }
            }
        }
        private string _PedimentoNumero;
        public const string PedimentoNumeroPropertyName = "PedimentoNumero";

        public bool IsActive
        {
            get { return _IsActive; }
            set
            {
                if (_IsActive != value)
                {
                    _IsActive = value;
                    OnPropertyChanged(IsActivePropertyName);
                }
            }
        }
        private bool _IsActive;
        public const string IsActivePropertyName = "IsActive";

        public TipoPedimentoModel TipoPedimento
        {
            get { return _TipoPedimento; }
            set
            {
                if (_TipoPedimento != value)
                {
                    _TipoPedimento = value;
                    OnPropertyChanged(TipoPedimentoPropertyName);
                }
            }
        }
        private TipoPedimentoModel _TipoPedimento;
        public const string TipoPedimentoPropertyName = "TipoPedimento";
    }
}
