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
    public partial class ViewTeacher
    {
        public System.Guid Id { get; set; }
        public string Account { get; set; }
        public string RealName { get; set; }
        public string DisplayName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Nullable<bool> Gender { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string Birthplace { get; set; }
        public string Address { get; set; }
        public string Nationality { get; set; }
        public string IDCard { get; set; }
        public int PriorOrdinal { get; set; }
        public System.Guid DepartmentId { get; set; }
        public Nullable<System.Guid> TopDepartmentId { get; set; }
        public Nullable<State> State { get; set; }
        public string DepartmentName { get; set; }
        public string DeaprtmentDisplayName { get; set; }
        public Nullable<int> Level { get; set; }
        public Nullable<int> MinorOrdinal { get; set; }
        public DepartmentUserType Type { get; set; }
        public bool PerStaff { get; set; }
        public bool Sync { get; set; }
        public Nullable<int> AutoId { get; set; }
    }
}
