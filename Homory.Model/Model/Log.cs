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
    public partial class Log
    {
        public System.Guid Id { get; set; }
        public LogAction Action { get; set; }
        public string ActionContent { get; set; }
        public System.Guid ObjectA { get; set; }
        public System.Guid ObjectB { get; set; }
        public System.Guid ObjectC { get; set; }
        public bool NotifiedA { get; set; }
        public bool NotifiedB { get; set; }
        public bool NotifiedC { get; set; }
        public decimal Value { get; set; }
        public System.DateTime Time { get; set; }
        public State State { get; set; }
    }
}