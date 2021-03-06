//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace Futbol
{
    using System;
    using System.Collections.Generic;
    
    public partial class Imagen
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Imagen()
        {
            this.Equipo = new HashSet<Equipo>();
            this.Jugador = new HashSet<Jugador>();
            this.Noticia = new HashSet<Noticia>();
            this.Torneo = new HashSet<Torneo>();
        }
    
        public int imagen_id { get; set; }
        public string imagen_title { get; set; }
        [Display(Name = "Imagen")]
        public string imagen_path { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Equipo> Equipo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Jugador> Jugador { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Noticia> Noticia { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Torneo> Torneo { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}
