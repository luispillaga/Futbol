//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Futbol
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pago
    {
        public int pago_id { get; set; }
        public System.DateTime pago_fecha { get; set; }
        public decimal pago_valor { get; set; }
        public string pago_descripcion { get; set; }
        public Nullable<int> tor_equ_id { get; set; }
    
        public virtual TorneoEquipo TorneoEquipo { get; set; }
    }
}