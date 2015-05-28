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
    public partial class ViewResource
    {
        public System.Guid RC { get; set; }
        public System.Guid Id { get; set; }
        public System.Guid UserId { get; set; }
        public ResourceType Type { get; set; }
        public OpenType OpenType { get; set; }
        public ResourceFileType FileType { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public bool Converted { get; set; }
        public string Thumbnail { get; set; }
        public string SourceName { get; set; }
        public string Source { get; set; }
        public string Preview { get; set; }
        public string Image { get; set; }
        public bool Prize { get; set; }
        public Nullable<ResourcePrizeRange> PrizeRange { get; set; }
        public Nullable<ResourcePrizeLevel> PrizeLevel { get; set; }
        public int Credit { get; set; }
        public State State { get; set; }
        public int Favourite { get; set; }
        public int Comment { get; set; }
        public int Rate { get; set; }
        public decimal Grade { get; set; }
        public int View { get; set; }
        public int Download { get; set; }
        public System.DateTime Time { get; set; }
    }
}
