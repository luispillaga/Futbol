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
    
    public partial class JugadorScore
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JugadorScore()
        {
            this.Alineacion = new HashSet<Alineacion>();
        }
    
        public int jug_score_id { get; set; }
        public int jug_score_num_goles { get; set; }
        public int jug_score_num_amarillas { get; set; }
        public int jug_score_num_rojas { get; set; }
        public int jug_score_num_autogoles { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Alineacion> Alineacion { get; set; }
    }
}
