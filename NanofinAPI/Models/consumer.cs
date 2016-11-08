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
    
    public partial class consumer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public consumer()
        {
            this.activeproductitems = new HashSet<activeproductitem>();
            this.claims = new HashSet<claim>();
            this.consumerriskvalues = new HashSet<consumerriskvalue>();
        }
    
        public int Consumer_ID { get; set; }
        public int User_ID { get; set; }
        public System.DateTime consumerDateOfBirth { get; set; }
        public string consumerAddress { get; set; }
        public string gender { get; set; }
        public string maritalStatus { get; set; }
        public string topProductCategoriesInterestedIn { get; set; }
        public string homeOwnerType { get; set; }
        public string employmentStatus { get; set; }
        public Nullable<decimal> grossMonthlyIncome { get; set; }
        public Nullable<decimal> nettMonthlyIncome { get; set; }
        public Nullable<decimal> totalMonthlyExpenses { get; set; }
        public Nullable<int> Location_ID { get; set; }
        public Nullable<int> numDependant { get; set; }
        public Nullable<int> numClaims { get; set; }
        public Nullable<int> ageGroup_ID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<activeproductitem> activeproductitems { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<claim> claims { get; set; }
        public virtual user user { get; set; }
        public virtual location location { get; set; }
        public virtual risk_agegroup risk_agegroup { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<consumerriskvalue> consumerriskvalues { get; set; }
    }
}