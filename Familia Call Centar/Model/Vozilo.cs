//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Familia_Call_Centar.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class vozilo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public vozilo()
        {
            this.voznja = new HashSet<voznja>();
        }

        public vozilo(int id, string tip, int nosivost)
        {
            this.voznja = new HashSet<voznja>();
            this.voziloID = id;
            this.tip_vozila = tip;
            this.nosivost = nosivost;
        }


        public int voziloID { get; set; }
        public string tip_vozila { get; set; }
        public int nosivost { get; set; }
        public string url_slike_vozila { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<voznja> voznja { get; set; }
    }
}
