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
    public partial class ResourceRoom
    {
        public ResourceRoom()
        {
            this.ResourceCommentTemp = new HashSet<ResourceCommentTemp>();
        }
    
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public int Ordinal { get; set; }
        public State State { get; set; }
    
        public virtual ICollection<ResourceCommentTemp> ResourceCommentTemp { get; set; }
    }
}