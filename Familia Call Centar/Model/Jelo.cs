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
    
    public partial class jelo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public jelo()
        {
            this.narudzba_item = new HashSet<narudzba_item>();
        }

        public jelo(string naziv, string opis, string tip, Double cijena)
        {
            this.narudzba_item = new HashSet<narudzba_item>();
            this.naziv = naziv;
            this.opis = opis;
            this.tip_jela = tip;
            this.cijena = cijena;
        }

        public jelo(int id, string naziv, string opis, string tip, Double cijena)
        {
            this.narudzba_item = new HashSet<narudzba_item>();
            this.jeloID = id;
            this.naziv = naziv;
            this.opis = opis;
            this.tip_jela = tip;
            this.cijena = cijena;
        }


        public int jeloID { get; set; }
        public string naziv { get; set; }
        public string opis { get; set; }
        public string tip_jela { get; set; }
        public Nullable<double> cijena { get; set; }
        public string url_slike_jela { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<narudzba_item> narudzba_item { get; set; }
    }
}
