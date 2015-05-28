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
    public partial class ResourceComment
    {
        public System.Guid Id { get; set; }
        public Nullable<System.Guid> ParentId { get; set; }
        public int Level { get; set; }
        public System.Guid ResourceId { get; set; }
        public System.Guid UserId { get; set; }
        public string Content { get; set; }
        public Nullable<decimal> Start { get; set; }
        public Nullable<decimal> End { get; set; }
        public Nullable<bool> Timed { get; set; }
        public System.DateTime Time { get; set; }
        public State State { get; set; }
    
        public virtual Resource Resource { get; set; }
        public virtual User User { get; set; }
    }
}
