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
    
    public partial class activeproductitemswithdetail
    {
        public int ActiveProductItems_ID { get; set; }
        public int User_ID { get; set; }
        public int Consumer_ID { get; set; }
        public int Product_ID { get; set; }
        public string activeProductItemPolicyNum { get; set; }
        public Nullable<bool> isActive { get; set; }
        public Nullable<bool> Accepted { get; set; }
        public decimal productValue { get; set; }
        public int duration { get; set; }
        public Nullable<System.DateTime> activeProductItemStartDate { get; set; }
        public Nullable<System.DateTime> activeProductItemEndDate { get; set; }
        public int ProductProvider_ID { get; set; }
        public Nullable<int> ProductType_ID { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }
        public string productPolicyDocPath { get; set; }
        public Nullable<bool> isAvailableForPurchase { get; set; }
        public int InsuranceType_ID { get; set; }
        public Nullable<decimal> ipCoverAmount { get; set; }
        public Nullable<int> ipUnitType { get; set; }
        public string UnitTypeDescription { get; set; }
        public Nullable<decimal> ipUnitCost { get; set; }
        public Nullable<System.DateTime> claimTimeframe { get; set; }
        public string claimContactNo { get; set; }
        public Nullable<int> claimtemplate_ID { get; set; }
    }
}
