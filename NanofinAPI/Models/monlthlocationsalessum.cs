//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NanofinAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class monlthlocationsalessum
    {
        public Nullable<System.DateTime> purchaseDate { get; set; }
        public string datum { get; set; }
        public int ProductProvider_ID { get; set; }
        public int Location_ID { get; set; }
        public string Province { get; set; }
        public string city { get; set; }
        public string LatLng { get; set; }
        public Nullable<decimal> sales { get; set; }
    }
}
