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
    public partial class Menu
    {
        public System.Guid Id { get; set; }
        public Nullable<System.Guid> ParentId { get; set; }
        public System.Guid ApplicationId { get; set; }
        public string Name { get; set; }
        public string Redirect { get; set; }
        public string Icon { get; set; }
        public State State { get; set; }
        public int Ordinal { get; set; }
        public string RightName { get; set; }
    
        public virtual Application Application { get; set; }
        public virtual Right Right { get; set; }
    }
}
