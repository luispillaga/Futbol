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
    
    public partial class FaseEliminatoria
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FaseEliminatoria()
        {
            this.Jornada = new HashSet<Jornada>();
        }
    
        public int fasee_id { get; set; }
        public string fasee_nombre { get; set; }
        public System.DateTime fasee_fecha { get; set; }
        public int fasee_num_equipos { get; set; }
        public int fasee_al_mejor_de { get; set; }
        public string fasee_estado { get; set; }
        public bool fasee_tercer_lugar { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Jornada> Jornada { get; set; }
    }
}
