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
    
    public partial class checkpoint
    {
        public double longitude { get; set; }
        public double latitude { get; set; }
        public System.DateTime vrijeme_isporuke { get; set; }
        public int voznjaID { get; set; }
    
        public virtual voznja voznja { get; set; }
    }
}
