//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Homory.Model
{
    using System;
    using System.Collections.Generic;
    
    [EntityFramework.Audit.Audit]
    public partial class Catalog
    {
        public Catalog()
        {
            this.CatalogChildren = new HashSet<Catalog>();
            this.Learned = new HashSet<Learned>();
            this.Taught = new HashSet<Taught>();
            this.ResourceCatalog = new HashSet<ResourceCatalog>();
            this.UserCatalog = new HashSet<UserCatalog>();
            this.AssessTable = new HashSet<AssessTable>();
            this.AssessTable1 = new HashSet<AssessTable>();
            this.Group = new HashSet<Group>();
            this.Group1 = new HashSet<Group>();
        }
    
        public System.Guid Id { get; set; }
        public Nullable<System.Guid> ParentId { get; set; }
        public CatalogType Type { get; set; }
        public string Name { get; set; }
        public State State { get; set; }
        public int Ordinal { get; set; }
        public Nullable<System.Guid> TopId { get; set; }
    
        public virtual ICollection<Catalog> CatalogChildren { get; set; }
        public virtual Catalog CatalogParent { get; set; }
        public virtual ICollection<Learned> Learned { get; set; }
        public virtual ICollection<Taught> Taught { get; set; }
        public virtual ICollection<ResourceCatalog> ResourceCatalog { get; set; }
        public virtual ICollection<UserCatalog> UserCatalog { get; set; }
        public virtual ICollection<AssessTable> AssessTable { get; set; }
        public virtual ICollection<AssessTable> AssessTable1 { get; set; }
        public virtual ICollection<Group> Group { get; set; }
        public virtual ICollection<Group> Group1 { get; set; }
    }
}
