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
    
    public partial class FaseEquipo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FaseEquipo()
        {
            this.Estadistica = new HashSet<Estadistica>();
        }
    
        public int fase_equ_id { get; set; }
        public Nullable<int> tor_equ_id { get; set; }
        public Nullable<int> fasee_id { get; set; }
        public Nullable<int> grupo_id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Estadistica> Estadistica { get; set; }
        public virtual TorneoEquipo TorneoEquipo { get; set; }
        public virtual Grupo Grupo { get; set; }
        public virtual TorneoEquipo TorneoEquipo1 { get; set; }
    }
}
