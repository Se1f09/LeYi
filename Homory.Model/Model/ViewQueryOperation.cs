//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Homory.Model
{
    using System;
    using System.Collections.Generic;
    
    [EntityFramework.Audit.Audit]
    public partial class ViewQueryOperation
    {
        public System.Guid Id { get; set; }
        public System.DateTime Time { get; set; }
        public OperationType Type { get; set; }
        public Nullable<System.Guid> UserId { get; set; }
        public Nullable<System.Guid> CampusId { get; set; }
        public string Name { get; set; }
        public string 姓名 { get; set; }
    }
}