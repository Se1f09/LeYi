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
    public partial class Action
    {
        public System.Guid Id { get; set; }
        public ActionType Type { get; set; }
        public System.Guid Id1 { get; set; }
        public System.Guid Id2 { get; set; }
        public Nullable<System.Guid> Id3 { get; set; }
        public Nullable<System.Guid> Id4 { get; set; }
        public string Content1 { get; set; }
        public string Content2 { get; set; }
        public string Content3 { get; set; }
        public string Content4 { get; set; }
        public System.DateTime Time { get; set; }
        public State State { get; set; }
    }
}
