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
    public partial class ViewQueryTaught
    {
        public System.Guid DepartmentId { get; set; }
        public System.Guid CourseId { get; set; }
        public int State { get; set; }
        public string 学校 { get; set; }
        public string 届 { get; set; }
        public string 班级 { get; set; }
        public string 课程名称 { get; set; }
        public string 教师 { get; set; }
    }
}
