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
    
    public partial class location
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public location()
        {
            this.activeproductitems = new HashSet<activeproductitem>();
            this.activeproductitems1 = new HashSet<activeproductitem>();
            this.consumers = new HashSet<consumer>();
            this.resellers = new HashSet<reseller>();
        }
    
        public int Location_ID { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string LatLng { get; set; }
        public string PostalCode { get; set; }
        public Nullable<int> GDP { get; set; }
        public Nullable<decimal> UnemploymentRate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<activeproductitem> activeproductitems { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<activeproductitem> activeproductitems1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<consumer> consumers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<reseller> resellers { get; set; }
    }
}
