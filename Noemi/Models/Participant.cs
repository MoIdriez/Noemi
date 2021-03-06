//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Noemi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Participant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Participant()
        {
            this.Trials = new HashSet<Trial>();
        }
    
        public int ID { get; set; }
        public string ProloficID { get; set; }
        public string SessionID { get; set; }
        public string ColourOrientation { get; set; }
        public bool ColourOrderIsRandom { get; set; }
        public int ImageIterations { get; set; }
        public bool ImageOrderIsRandom { get; set; }
        public int ColourMode { get; set; }
        public string Colours4 { get; set; }
        public string Colours9 { get; set; }
        public string Colours36 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Trial> Trials { get; set; }
    }
}
