//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace APPR_POE.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class GoodDonation
    {
        public GoodDonation()
        {
            AllocateGoods = new HashSet<AllocateGood>();
            PurchaseGoods = new HashSet<PurchaseGood>();
        }
        public int GoodId { get; set; }
        public string Email { get; set; }
        public string DonarName { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> NumItems { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
    
        public virtual UserTable UserTable { get; set; }
    }
}
